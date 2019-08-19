﻿using System.Threading.Tasks;

using IO.Ably;
using IO.Ably.CustomSerialisers;
using IO.Ably.Realtime;
using IO.Ably.Types;

namespace IO.Ably.Transport.States.Connection
{
    internal class ConnectionConnectedState : ConnectionStateBase
    {
        private readonly ConnectionInfo _info;
        private bool? _resumed = null;

        public ConnectionConnectedState(IConnectionContext context, ConnectionInfo info, ErrorInfo error = null, ILogger logger = null)
            : base(context, logger)
        {
            _info = info;
            Error = error;
        }

        public override ConnectionState State => ConnectionState.Connected;

        public override bool CanSend => true;

        public override void Close()
        {
            Context.SetState(new ConnectionClosingState(Context, Logger));
        }

        public override async Task<bool> OnMessageReceived(ProtocolMessage message)
        {
            switch (message.Action)
            {
                case ProtocolMessage.MessageAction.Auth:
                    TaskUtils.RunInBackground(Context.RetryAuthentication(updateState: false), e => Logger.Warning(e.Message));
                    return true;
                case ProtocolMessage.MessageAction.Connected:
                    await Context.SetState(new ConnectionConnectedState(Context, new ConnectionInfo(message), message.Error, Logger) { IsUpdate = true });
                    return true;
                case ProtocolMessage.MessageAction.Close:
                    await Context.SetState(new ConnectionClosedState(Context, message.Error, Logger));
                    return true;
                case ProtocolMessage.MessageAction.Disconnected:
                    if (await Context.CanUseFallBackUrl(message.Error))
                    {
                        Context.Connection.Key = null;
                        await Context.SetState(new ConnectionDisconnectedState(Context, message.Error, Logger) { RetryInstantly = true });
                    }

                    if (await Context.RetryBecauseOfTokenError(message.Error))
                    {
                        return true;
                    }

                    await Context.SetState(new ConnectionDisconnectedState(Context, message.Error, Logger));
                    return true;
                case ProtocolMessage.MessageAction.Error:
                    // an error message may signify an error state in the connection or in a channel
                    // Only handle connection errors here.
                    if (message.Channel.IsEmpty())
                    {
                        await Context.SetState(new ConnectionFailedState(Context, message.Error, Logger));
                    }

                    return true;
            }

            return false;
        }

        public override void AbortTimer()
        {
        }

        public override void BeforeTransition()
        {
            if (_info != null)
            {
                if (WasThereAPreviousConnection())
                {
                    _resumed = Context.Connection.Id == _info.ConnectionId;
                }

                Context.Connection.Id = _info.ConnectionId;
                Context.Connection.Key = _info.ConnectionKey;
                Context.Connection.Serial = _info.ConnectionSerial;
                if (_info.ConnectionStateTtl.HasValue)
                {
                    Context.Connection.ConnectionStateTtl = _info.ConnectionStateTtl.Value;
                }

                Context.SetConnectionClientId(_info.ClientId);
            }

            if (_resumed.HasValue && _resumed.Value && Logger.IsDebug)
            {
                Logger.Debug("Connection resumed!");
            }
        }

        private bool WasThereAPreviousConnection()
        {
            return Context.Connection.Key.IsNotEmpty();
        }

        public override Task OnAttachToContext()
        {
            if (Logger.IsDebug)
            {
                Logger.Debug("Processing Connected:OnAttached. Resumed: " + _resumed);
            }

            if (_resumed.HasValue && _resumed == false)
            {
                Context.ClearAckQueueAndFailMessages(null);
                Context.DetachAttachedChannels(Error);
            }

            Context.SendPendingMessages(_resumed.GetValueOrDefault());
            return TaskConstants.BooleanTrue;
        }
    }
}

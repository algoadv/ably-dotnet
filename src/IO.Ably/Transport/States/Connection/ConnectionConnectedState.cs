﻿using System.Threading.Tasks;
using IO.Ably.Types;

namespace IO.Ably.Transport.States.Connection
{
    internal class ConnectionConnectedState : ConnectionState
    {
        public ConnectionConnectedState(IConnectionContext context, ConnectionInfo info) :
            base(context)
        {
            this.Context.Connection.Id = info.ConnectionId;
            this.Context.Connection.Key = info.ConnectionKey;
            this.Context.Connection.Serial = info.ConnectionSerial;
        }

        public override Realtime.ConnectionState State => Realtime.ConnectionState.Connected;

        protected override bool CanQueueMessages => false;

        public override void Connect()
        {
            // do nothing
        }

        public override void Close()
        {
            Context.SetState(new ConnectionClosingState(Context));
        }

        public override Task<bool> OnMessageReceived(ProtocolMessage message)
        {
            switch (message.action)
            {
                case ProtocolMessage.MessageAction.Disconnected:
                {
                    Context.SetState(new ConnectionDisconnectedState(Context, message.error));
                    return TaskConstants.BooleanTrue;
                }
                case ProtocolMessage.MessageAction.Error:
                {
                    Context.SetState(new ConnectionFailedState(Context, message.error));
                        return TaskConstants.BooleanTrue;
                    }
            }
            return TaskConstants.BooleanFalse;
        }

        public override Task OnTransportStateChanged(TransportStateInfo state)
        {
            if (state.State == TransportState.Closed)
            {
                Context.SetState(new ConnectionDisconnectedState(Context, state));
            }
            return TaskConstants.BooleanTrue;
        }

        public override void SendMessage(ProtocolMessage message)
        {
            Context.Transport.Send(message);
        }

        public override void OnAttachedToContext()
        {
            Context.ResetConnectionAttempts();

            if (Context.QueuedMessages != null && Context.QueuedMessages.Count > 0)
            {
                foreach (var message in Context.QueuedMessages)
                {
                    SendMessage(message);
                }
                Context.QueuedMessages.Clear();
            }
        }
    }
}
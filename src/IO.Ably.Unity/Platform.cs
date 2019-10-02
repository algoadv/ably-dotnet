using IO.Ably.Transport;
using System.Net.NetworkInformation;
using IO.Ably.Realtime;

namespace IO.Ably
{
    internal class Platform : IPlatform
    {
        public string PlatformId => "unity";

        public ITransportFactory TransportFactory => null;

        static Platform()
        {
            Connection.NotifyOperatingSystemNetworkState(NetworkState.Online);
            // NetworkChange.NetworkAvailabilityChanged += (sender, eventArgs) =>
                // Connection.NotifyOperatingSystemNetworkState(eventArgs.IsAvailable ? NetworkState.Online : NetworkState.Offline);
        }
    }
}

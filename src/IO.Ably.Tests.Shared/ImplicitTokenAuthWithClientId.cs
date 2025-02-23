using System;
using FluentAssertions;
using Xunit;

namespace IO.Ably.Tests
{
    public class ImplicitTokenAuthWithClientId
    {
        private const string ApiKey = "123.456:789";
        private string _clientId;

        internal AblyRequest CurrentRequest { get; set; }

        public AblyRest Client { get; set; }

        public int ExecutionCount { get; set; }

        public ImplicitTokenAuthWithClientId()
        {
            _clientId = "123";
            Client = new AblyRest(new ClientOptions() { Key = ApiKey, ClientId = _clientId, UseBinaryProtocol = false });
            Client.ExecuteHttpRequest = request =>
            {
                ExecutionCount++;
                if (request.Url.Contains("requestToken"))
                {
                    return string.Format(
                                "{{ \"access_token\": {{ \"id\": \"unique-token-id\", \"expires\": \"{0}\"}}}}",
                                DateTimeOffset.UtcNow.AddDays(1).ToUnixTimeInMilliseconds()).ToAblyResponse();
                }

                return "{}".ToAblyResponse();
            };
        }

        [Fact]
        public void WhenPublishing_WillSendATokenRequestToServer()
        {
            Client.Channels.Get("test").PublishAsync("test", "true");

            ExecutionCount.Should().Be(2);
        }

        [Fact]
        public void BeforeSendingAMessage_CurrentTokenIsNull()
        {
            Client.AblyAuth.CurrentToken.Should().BeNull();
        }

        [Fact]
        public void AfterSendingAMessage_CurrentTokenHasDefaultCapabilityAndTtl()
        {
            Client.Channels.Get("test").PublishAsync("test", "true");

            Client.AblyAuth.CurrentToken.Should().NotBeNull();
        }
    }
}

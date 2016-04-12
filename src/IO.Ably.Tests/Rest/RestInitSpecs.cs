using FluentAssertions;
using IO.Ably.Transport;
using Xunit;

namespace IO.Ably.Tests
{
    public class RestInitSpecs : AblySpecs
    {
        [Fact]
        public void Init_WithKeyAndNoClientId_SetsAuthMethodToBasic()
        {
            var client = new AblyRest(ValidKey);
            Assert.Equal(AuthMethod.Basic, client.AblyAuth.AuthMethod);
        }

        [Fact]
        public void Init_WithKeyAndClientId_SetsAuthMethodToToken()
        {
            var client = new AblyRest(new ClientOptions { Key = ValidKey, ClientId = "123" });
            Assert.Equal(AuthMethod.Token, client.AblyAuth.AuthMethod);
        }

        [Fact]
        public void Init_WithKeyNoClientIdAndAuthTokenId_SetsCurrentTokenWithSuppliedId()
        {
            ClientOptions options = new ClientOptions { Key = ValidKey, ClientId = "123", Token = "222" };
            var client = new AblyRest(options);

            Assert.Equal(options.Token, client.AblyAuth.CurrentToken.Token);
        }

        [Fact]
        public void Init_WithouthKey_SetsAuthMethodToToken()
        {
            var client = new AblyRest(opts =>
            {
                opts.Token = "blah";
                opts.ClientId = "123";
            });

            Assert.Equal(AuthMethod.Token, client.AblyAuth.AuthMethod);
        }

        [Fact]
        public void Init_WithExplicitHost_ShouldInitialiseHttpClientWithCorrectHost()
        {
            var client = new AblyRest(opts =>
            {
                opts.RestHost = "boo.boo.com";
            });
            client.HttpClient.Host.Should().Be("boo.boo.com");
        }

        [Fact]
        public void Init_WithoutSpecifiedHost_ShouldInitialiseHttpClientWithDefaultHost()
        {
            new AblyRest(ValidKey).HttpClient.Host.Should().Be(Defaults.RestHost);
        }

        [Fact]
        public void Init_WithTlsAndSpecificPort_ShouldInitialiseHttpClientWithCorrectPort()
        {
            var client = new AblyRest(opts =>
            {
                opts.Tls = true;
                opts.TlsPort = 111; }
                );
            client.HttpClient.Port.Should().Be(111);
        }

        [Fact]
        public void Init_WithTlsFalseAndSpecificPort_ShouldInitialiseHttpClientWithCorrectPort()
        {
            var client = new AblyRest(opts =>
            {
                opts.Tls = false;
                opts.Port = 111;
            }
                );
            client.HttpClient.Port.Should().Be(111);
        }
    }
}
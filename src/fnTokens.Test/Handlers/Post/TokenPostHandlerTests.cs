using FluentAssertions;
using fnTokens.Data;
using fnTokens.Handlers.Post;
using fnTokens.Settings;
using Moq;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using Xunit;

namespace fnTokens.Test.Handlers.Post
{

    public class TokenPostHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new TokenPostHandler(null,null, null).Should().NotBeNull();
        }

        [Fact]
        public void IsAHandler()
        {
            (new TokenPostHandler(null,null,null) as IHandler).Should().NotBeNull();
        }

        [Fact]
        public void AddsTokenWithTokenId()
        {
            // arrange 
            var tokenId = "aqsdasdsad";
            var requestContext = new Mock<IRequestContext>();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns(tokenId);
            var responseContext = new Mock<IResponseContent>();
            var tokenDataCache = new MockTokenDataCache();
            var handler = new TokenPostHandler(new MockAppSettings(), new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(tokenId, tokenDataCache.Token);
        }

        [Fact]
        public void AddsTokenWithClaims()
        {
            // arrange 
            var claimsJson  = "{hello:1}";
            var requestContext = new Mock<IRequestContext>();
            requestContext.Setup(x => x.GetValue("ClaimsJson")).Returns(claimsJson);
            var responseContext = new Mock<IResponseContent>();
            var tokenDataCache = new MockTokenDataCache();
            var handler = new TokenPostHandler(new MockAppSettings(), new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(claimsJson, tokenDataCache.Values);
        }

        [Fact]
        public void AddsTokenWithExpiresDateTime()
        {
            // arrange 
            var expiresOn = new MockDateTimeProvider().Now.AddMinutes(10);
            var requestContext = new Mock<IRequestContext>();
            requestContext.Setup(x => x.GetValue("expiresInMinutes")).Returns("10");
            var responseContext = new Mock<IResponseContent>();
            var tokenDataCache = new MockTokenDataCache();
            var handler = new TokenPostHandler(new MockAppSettings(), new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(expiresOn, tokenDataCache.ExpiresOn);
        }

        [Fact]
        public void AddsTokenWithDefaultExpiresDateTime()
        {
            // arrange 
            var expiresOn = new MockDateTimeProvider().Now.AddMinutes(60);
            var requestContext = new Mock<IRequestContext>();
            requestContext.Setup(x => x.GetValue("expiresInMinutes")).Returns("NOT A NUMBER");
            var responseContext = new Mock<IResponseContent>();
            var tokenDataCache = new MockTokenDataCache();
            var handler = new TokenPostHandler(new MockAppSettings(), new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(expiresOn, tokenDataCache.ExpiresOn);
        }
    }

    class MockAppSettings : IAppSettings
    {
        public int DefaultExpiryInMins => 60;
    }

    class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return new DateTime(2000, 1, 1); } }
    }
    class MockTokenDataCache : ITokenDataCache
    {
        public DateTime ExpiresOn { get; private set; }
        public string Token { get; private set; }
        public object Values { get; private set; }

        public object Get(string token)
        {
            throw new NotImplementedException();
        }

        public void Set(string token, object values, DateTime expiresOn)
        {
            Token = token;
            Values = values;
            ExpiresOn = expiresOn;
        }
    }
}

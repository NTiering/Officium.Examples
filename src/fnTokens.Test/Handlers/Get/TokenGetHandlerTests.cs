using FluentAssertions;
using fnTokens.Data;
using fnTokens.Handlers.Get;
using fnTokens.Handlers.Post;
using fnTokens.Settings;
using Moq;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using Xunit;

namespace fnTokens.Test.Handlers.Get
{
    public class TokenGetHandlerTests
    {       
        [Fact]
        public void CanBeConstructed()
        {
            new TokenGetHandler(null, null).Should().NotBeNull();
        }

        [Fact]
        public void IsAHandler()
        {
            (new TokenGetHandler(null, null) as IHandler).Should().NotBeNull();
        }

        [Fact]
        public void PresentsTokenToDataCache()
        {
            // arrange 
            var tokenId = "aqsdasdsad";
            var requestContext = new Mock<IRequestContext>();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns(tokenId);
            var responseContext = new Mock<IResponseContent>();
            var tokenDataCache = new MockTokenDataCache();
            var handler = new TokenGetHandler(new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(tokenId, tokenDataCache.Token);
        }

        [Fact]
        public void PresentsResultToResponse()
        {
            // arrange 
            string supplied = "{ message : 2222}";
            string expected = null;
            var requestContext = new Mock<IRequestContext>();
            var responseContext = new Mock<IResponseContent>();
            responseContext.SetupSet(x => x.Result).Callback(x => expected = x.ToString());
            var tokenDataCache = new MockTokenDataCache { Result = supplied };
            var handler = new TokenGetHandler(new MockDateTimeProvider(), tokenDataCache);

            // act 
            handler.HandleRequest(requestContext.Object, responseContext.Object);

            // assert
            Assert.Equal(supplied, expected);
        }
    }

    class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return new DateTime(2000, 1, 1); } }
    }
    class MockTokenDataCache : ITokenDataCache
    {
        public string Token { get; private set; }
        public object Result { get;  set; }

        public object Get(string token)
        {
            Token = token;
            return Result;
        }

        public void Set(string token, object values, DateTime expiresOn)
        {
        }
    }
}

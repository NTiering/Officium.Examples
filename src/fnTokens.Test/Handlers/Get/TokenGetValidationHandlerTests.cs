using FluentAssertions;
using fnTokens.Data;
using fnTokens.Handlers.Get;
using fnTokens.Handlers.Post;
using fnTokens.Settings;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using Xunit;

namespace fnTokens.Test.Handlers.Get
{
    public class TokenGetValidationHandlerTests
    {
        [Fact]
        public void ItCanBeConstructed()
        {
            new TokenGetValidationHandler().Should().NotBeNull();
        }

        [Fact]
        public void ItIsAHandler()
        {
            (new TokenGetValidationHandler() as IHandler).Should().NotBeNull();
        }

        [Fact]
        public void ValidRequestCauseNoErrors()
        {
            // arrange 
            var tokenId = "aqsdasdsad";
            var requestContext = new Mock<IRequestContext>();
            var responseContent = new MockResponseContent();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns(tokenId);   

            // act 
            new TokenGetValidationHandler().HandleRequest(requestContext.Object, responseContent);

            // assert
            Assert.Empty(responseContent.ValidationErrors);
        }

        [Fact]
        public void MissingTokenIdCausesAValidationError()
        {
            // arrange             
            var requestContext = new Mock<IRequestContext>();
            var responseContent = new MockResponseContent();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns(string.Empty);
            var tokenDataCache = new MockTokenDataCache();

            // act 
            new TokenGetValidationHandler().HandleRequest(requestContext.Object, responseContent);

            // assert
            Assert.Single(responseContent.ValidationErrors);
        }
    }

    public class MockResponseContent : IResponseContent
    {
        public Exception Exception { get; set; }
        public object Result { get; set; }
        public int StatusCode { get; set; }

        public List<IValidationError> ValidationErrors { get; set; }

        public MockResponseContent()
        {
            ValidationErrors = new List<IValidationError>();
        }
        public ActionResult GetActionResult()
        {
            throw new NotImplementedException();
        }
    }
}

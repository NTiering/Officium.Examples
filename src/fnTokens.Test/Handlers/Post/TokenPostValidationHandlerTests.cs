using FluentAssertions;
using fnTokens.Data;
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

namespace fnTokens.Test.Handlers.Post
{
    public class TokenPostValidationHandlerTests
    {
        [Fact]
        public void CanBeConstructed()
        {
            new TokenPostValidationHandler().Should().NotBeNull();
        }

        [Fact]
        public void IsAHandler()
        {
            (new TokenPostValidationHandler() as IHandler).Should().NotBeNull();
        }

        [Fact]
        public void ValidRequestsMakeNoValidationErrors()
        {
            // arrange
            var requestContext = new Mock<IRequestContext>();
            var responseContent = new MockResponseContent();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns("aqsdasdsad");
            requestContext.Setup(x => x.GetValue("ClaimsJson")).Returns("{message : 22}");
            requestContext.Setup(x => x.GetValue("expiresInMinutes")).Returns("45");


            // act 
            new TokenPostValidationHandler().HandleRequest(requestContext.Object, responseContent);

            // assert
            Assert.Empty(responseContent.ValidationErrors);
        }

        [Fact]
        public void MissingExpiresInMinutesMakeNoValidationErrors()
        {
            // arrange 
            var requestContext = new Mock<IRequestContext>();
            var responseContent = new MockResponseContent();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns("aqsdasdsad");
            requestContext.Setup(x => x.GetValue("ClaimsJson")).Returns("{message : 22}");

            // act 
            new TokenPostValidationHandler().HandleRequest(requestContext.Object, responseContent);

            // assert
            Assert.Empty(responseContent.ValidationErrors);
        }

        [Fact]
        public void NonIntExpiresInMinutesCausesErrors()
        {
            // arrange 
            var requestContext = new Mock<IRequestContext>();
            var responseContent = new MockResponseContent();
            requestContext.Setup(x => x.GetValue("TokenId")).Returns("aqsdasdsad");
            requestContext.Setup(x => x.GetValue("ClaimsJson")).Returns("{message : 22}");
            requestContext.Setup(x => x.GetValue("expiresInMinutes")).Returns("NOT A NUMBER");

            // act 
            new TokenPostValidationHandler().HandleRequest(requestContext.Object, responseContent);

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
using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpeakerMeet.Tests
{
    public class SpeakerControllerSearchTests
    {
        private readonly SpeakerController _controller;

        public SpeakerControllerSearchTests()
        {
            _controller = new SpeakerController();
        }

        [Fact]
        public void ItReturnsOkObjectResult()
        {
            // Act
            var result = _controller.Search("Jos");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ItReturnsCollectionOfSpeakers()
        {
            // Act
            var result = _controller.Search("Jos") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.IsType<List<Speaker>>(result.Value);
        }

        [Fact]
        public void GivenExactMatchThenOneSpeakerInCollection()
        {
            // Arrange
            // Act
            var result = _controller.Search("Joshua") as OkObjectResult;

            // Assert
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Single(speakers);
        }
    }
}

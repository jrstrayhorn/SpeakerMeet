using Microsoft.AspNetCore.Mvc;
using SpeakerMeet.API.Controllers;
using SpeakerMeet.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpeakerMeet.Tests
{
    public class TestSpeakerService: ISpeakerService
    {

    }

    public class SpeakerControllerSearchTests
    {
        private readonly SpeakerController _controller;

        public SpeakerControllerSearchTests()
        {
            var testSpeakerService = new TestSpeakerService();

            _controller = new SpeakerController(testSpeakerService);
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
            Assert.Equal("Joshua", speakers[0].Name);
        }

        [Theory]
        [InlineData("Joshua")]
        [InlineData("joshua")]
        [InlineData("JoShUa")]
        public void GivenCaseInsensitveMatchThenSpeakerInCollection(string searchString)
        {
            // Arrange
            // Act
            var result = _controller.Search(searchString) as OkObjectResult;

            // Assert
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Single(speakers);
            Assert.Equal("Joshua", speakers[0].Name);
        }

        [Fact]
        public void GivenNoMatchThenEmptyCollection()
        {
            // Arrange
            // Act
            var result = _controller.Search("ZZZ") as OkObjectResult;

            // Assert
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Empty(speakers);
        }

        [Fact]
        public void Given3MatchThenCollectionWith3Speakers()
        {
            // Arrange
            // Act
            var result = _controller.Search("jos") as OkObjectResult;

            // Assert
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Equal(3, speakers.Count);
            Assert.Contains(speakers, s => s.Name == "Josh");
            Assert.Contains(speakers, s => s.Name == "Joshua");
            Assert.Contains(speakers, s => s.Name == "Joseph");

        }

        [Fact]
        public void ItAcceptsInterface()
        {
            // Arrange
            ISpeakerService testSpeakerService = new TestSpeakerService();

            // Act
            var controller = new SpeakerController(testSpeakerService);

            // Assert
            Assert.NotNull(controller);
        }
    }
}

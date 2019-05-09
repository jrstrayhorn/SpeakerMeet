using Microsoft.AspNetCore.Mvc;
using Moq;
using SpeakerMeet.API.Controllers;
using SpeakerMeet.API.Models;
using SpeakerMeet.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpeakerMeet.Tests
{
    public class TestSpeakerService : ISpeakerService
    {
        public IEnumerable<Speaker> Search(string searchString)
        {
            throw new NotImplementedException();
        }
    }

    public class SpeakerControllerSearchTests
    {
        private readonly SpeakerController _controller;
        private static Mock<ISpeakerService> _speakerServiceMock;
        private readonly List<Speaker> _speakers;

        public SpeakerControllerSearchTests()
        {
            _speakers = new List<Speaker> { new Speaker
            {
                Name = "test"
            }};

            // define the mock
            _speakerServiceMock = new Mock<ISpeakerService>();

            // when search is called, return list of speakers containing speaker
            _speakerServiceMock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(() => _speakers);

            _controller = new SpeakerController(_speakerServiceMock.Object);
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
        public void ItAcceptsInterface()
        {
            // Arrange
            ISpeakerService testSpeakerService = new TestSpeakerService();

            // Act
            var controller = new SpeakerController(testSpeakerService);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void ItCallsSearchServiceOnce()
        {
            // Arrange
            // Act
            _controller.Search("jos");

            // Assert
            _speakerServiceMock.Verify(mock => mock.Search(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GivenSearchStringThenSpeakerServiceSearchCalledWithString()
        {
            // Arrange
            var searchString = "jos";

            // Act
            _controller.Search(searchString);

            // Assert
            _speakerServiceMock.Verify(mock => mock.Search(searchString), Times.Once());
        }

        [Fact(Skip="refactoring")]
        public void GivenSpeakerServiceThenResultsReturned()
        {
            // Arrange
            var searchString = "jos";

            // Act
            var result = _controller.Search(searchString) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            var speakers = ((IEnumerable<Speaker>)result.Value).ToList();
            Assert.Equal(_speakers, speakers);
        }
    }
}

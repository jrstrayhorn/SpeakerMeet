using SpeakerMeet.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpeakerMeet.Tests
{
    public class SpeakerServiceTests
    {
        private static ISpeakerService _speakerService;

        public SpeakerServiceTests()
        {
            _speakerService = new SpeakerService();
        }

        [Fact]
        public void ItExists()
        {
            var speakerService = new SpeakerService();
        }

        [Fact]
        public void ItHasSearchMethod()
        {
            _speakerService.Search("test");
        }

        [Fact]
        public void ItImplementsISpeakerService()
        {
            Assert.True(_speakerService is ISpeakerService);
        }

        [Fact]
        public void GivenExactMatchThenOneSpeakerInCollection()
        {
            // Arrange
            // Act
            var result = _speakerService.Search("Joshua");

            // Assert
            var speakers = result.ToList();
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
            var result = _speakerService.Search(searchString);

            // Assert
            var speakers = result.ToList();
            Assert.Single(speakers);
            Assert.Equal("Joshua", speakers[0].Name);
        }

        [Fact]
        public void GivenNoMatchThenEmptyCollection()
        {
            // Arrange
            // Act
            var result = _speakerService.Search("ZZZ");

            // Assert
            var speakers = result.ToList();
            Assert.Empty(speakers);
        }

        [Fact]
        public void Given3MatchThenCollectionWith3Speakers()
        {
            // Arrange
            // Act
            var result = _speakerService.Search("jos");

            // Assert
            var speakers = result.ToList();
            Assert.Equal(3, speakers.Count);
            Assert.Contains(speakers, s => s.Name == "Josh");
            Assert.Contains(speakers, s => s.Name == "Joshua");
            Assert.Contains(speakers, s => s.Name == "Joseph");
        }
    }
}

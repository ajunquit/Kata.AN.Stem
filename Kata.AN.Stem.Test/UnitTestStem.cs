using Kata.AN.Stem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Kata.AN.Stem.Test.MockData;
using Moq;
using Kata.AN.Stem.Services;
using FluentAssertions;

namespace Kata.AN.Stem.Test
{
    public class UnitTestStem
    {
        [Fact]
        public void Test1Async()
        {
            /// Expected
            var expected = StemMockData.GetStemsMock1();

            /// Arrange
            string stem = "apples"; 
            var stemService = new Mock<IStemService>();
            stemService.Setup(x => x.GetWords(stem)).Returns(expected);
            
            var stemController = new StemController(stemService.Object);

            /// Act
            var result = (OkObjectResult) stemController.Get(stem);

            /// Assert
            result.StatusCode.Should().Be(200);
            var actual = result.Value as WordResult;

            Assert.NotNull(actual.Data);

            // verify that the method must execute one time.
            stemService.Verify(x => x.GetWords(stem), Times.Exactly(1));
        }
    }
}
using System;
using System.Linq;
using Xunit;

namespace TransportTycoon.Tests
{
    public class WorldTests
    {
        [Theory]
        //[InlineData("A", 5)]
        //[InlineData("AB", 5)]
        [InlineData("BB", 5)]
        //[InlineData("ABB", 7)]
        //[InlineData("AABABBAB", )]
        //[InlineData("AAAABBBB", )]
        //[InlineData("BBBBAAAA", )]
        //[InlineData("ABBBABAAABBB", )]
        public void ShouldDeliver(string destinations, int durationTimeInHours)
        {
            // Arrange
            var world = new World(destinations.Select(chr => chr.ToString()));

            // Act
            world.Deliver();

            // Assert
            Assert.Equal(durationTimeInHours, world.CurrentTime);
        }
    }
}

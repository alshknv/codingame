using System;
using System.Collections.Generic;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class PlayerTests
    {
        [Fact]
        public void SimpleTest()
        {
            var neighbors = Player.NeighborScan(2, 2, new string[] { "00", "0." });

            neighbors.Should().HaveCount(3);
            neighbors[0].Should().Be("0 0 1 0 0 1");
            neighbors[1].Should().Be("1 0 -1 -1 -1 -1");
            neighbors[2].Should().Be("0 1 -1 -1 -1 -1");
        }

        [Fact]
        public void Example()
        {
            var neighbors = Player.NeighborScan(1, 5, new string[] { "0.0.0" });

            neighbors.Should().HaveCount(3);
            neighbors[0].Should().Be("0 0 2 0 -1 -1");
            neighbors[1].Should().Be("2 0 4 0 -1 -1");
            neighbors[2].Should().Be("4 0 -1 -1 -1 -1");
        }
    }
}

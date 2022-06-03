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
            var blocks = Player.GetCloneBlocks(12, 3, 11, 3, new List<int>[] {
                new List<int>() { 1, 9 },
                new List<int>() { 2, 5, 11 },
                new List<int>() { 4, 10 },
                new List<int>(),
            });
            blocks.Should().HaveCount(2);
            blocks[0].Should().Be((0, 3));
            blocks[1].Should().Be((1, 1));
        }
    }
}

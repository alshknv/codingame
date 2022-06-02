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
        public void Example1()
        {
            var value = Solution.GetAnswers(new string[] {
                "####",
                "##O#",
                "#OO#",
                "####"
            }, new string[] { "1 2", "1 1", "2 2", "3 3" });

            value.Should().HaveCount(4);
            value[0].Should().Be(3);
            value[1].Should().Be(0);
            value[2].Should().Be(3);
            value[3].Should().Be(0);
        }
    }
}

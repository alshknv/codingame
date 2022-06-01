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
        public void SimpleExample1()
        {
            var value = Solution.GetMaxValue(new string[] {
                "0 5 1 2",
                "1 10 3 E",
                "2 20 3 4",
                "3 5 5 E",
                "4 10 E E",
                "5 10 E E"
            });

            value.Should().Be(40);
        }

        [Fact]
        public void Example2()
        {
            var value = Solution.GetMaxValue(new string[] {
                "0 17 1 2",
                "1 15 3 4",
                "2 15 4 5",
                "3 20 6 7",
                "4 12 7 8",
                "5 11 8 9",
                "6 18 10 11",
                "7 19 11 12",
                "8 12 12 13",
                "9 11 13 14",
                "10 13 E E",
                "11 14 E E",
                "12 17 E E",
                "13 19 E E",
                "14 15 E E"
            });

            value.Should().Be(88);
        }
    }
}

using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Loss3()
        {
            Solution.GetMaxLoss(6, "3 2 4 2 1 5").Should().Be(-3);
        }

        [Fact]
        public void Loss4()
        {
            Solution.GetMaxLoss(6, "5 3 4 2 3 1").Should().Be(-4);
        }

        [Fact]
        public void Profit()
        {
            Solution.GetMaxLoss(5, "1 2 4 4 5").Should().Be(0);
        }
    }
}

using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Example1()
        {
            Solution.GetTreeHeight(new[] { "1 2", "1 3", "3 4" }).Should().Be(3);
        }

        [Fact]
        public void Example2()
        {
            Solution.GetTreeHeight(new[] { "1 2", "1 3", "3 4", "2 4", "2 5", "10 11", "10 1", "10 3" }).Should().Be(4);
        }
    }
}

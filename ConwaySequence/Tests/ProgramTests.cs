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
            Solution.ConwaySequence(1, 6).Should().Equal(new int[] { 3, 1, 2, 2, 1, 1 });
        }
    }
}

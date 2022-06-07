using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Impossible()
        {
            var (isPossible, _) = Solution.GetGiftContributions(100, new[] { 20, 20, 40 });
            isPossible.Should().BeFalse();
        }

        [Fact]
        public void Possible1()
        {
            var (isPossible, contributions) = Solution.GetGiftContributions(100, new[] { 40, 40, 40 });
            isPossible.Should().BeTrue();
            contributions.Should().Equal(new[] { 33, 33, 34 });
        }

        [Fact]
        public void Possible2()
        {
            var (isPossible, contributions) = Solution.GetGiftContributions(100, new[] { 100, 1, 60 });
            isPossible.Should().BeTrue();
            contributions.Should().Equal(new[] { 1, 49, 50 });
        }
    }
}

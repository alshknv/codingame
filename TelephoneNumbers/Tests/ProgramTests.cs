using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void OneNumber()
        {
            Solution.TrieCount(new[] { "0467123456" }).Should().Be(10);
        }

        [Fact]
        public void TwoNumbers()
        {
            Solution.TrieCount(new[] { "0123456789", "1123456789" }).Should().Be(20);
        }

        [Fact]
        public void NumberIncludedInAnother()
        {
            Solution.TrieCount(new[] { "0123456789", "0123" }).Should().Be(10);
        }
    }
}

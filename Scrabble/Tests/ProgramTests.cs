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
            Solution.MostValuableWord(new[] { "because", "first", "these", "could", "which" }, "hicquwh").Should().Be("which");
        }

        [Fact]
        public void Example2()
        {
            Solution.MostValuableWord(new[] { "restaurateur", "satire" }, "aretsui").Should().Be("satire");
        }
    }
}

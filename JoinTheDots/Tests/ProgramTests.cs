using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void SquareOf4()
        {
            var input = new string[] {
                "4 4",
                "1..2",
                "5...",
                "....",
                "4..3"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "o--o",
                    "o  |",
                    "|  |",
                    "o--o"
                }
            );
        }

        [Fact]
        public void ZigZag()
        {
            var input = new string[] {
                "7 4",
                "1..2",
                "....",
                "....",
                "3..4",
                "....",
                "....",
                "5..6"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "o--o",
                    "  /",
                    " /",
                    "o--o",
                    "  /",
                    " /",
                    "o--o"
                }
            );
        }

        [Fact]
        public void XMarksTheSpot()
        {
            var input = new string[] {
                "5 5",
                "1...4",
                ".....",
                ".....",
                ".....",
                "3...2"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "o   o",
                    " \\ /",
                    "  X",
                    " / \\",
                    "o---o"
                }
            );
        }
    }
}

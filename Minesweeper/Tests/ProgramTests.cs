using FluentAssertions;
using Xunit;

namespace Codingame;

public class ProgramTests
{
    [Fact]
    public void Example()
    {
        var input = new string[] {
            "6 6",
            "4",
            ".1???1",
            ".11211",
            "11....",
            "?2....",
            "?2....",
            "11...."
        };
        Solution.Solve(input).Should().BeEquivalentTo(new string[] {
            "0 3",
            "0 4",
            "2 0",
            "4 0"
        });
    }

    [Fact]
    public void HiddenMines()
    {
        var input = new string[] {
            "5 5",
            "6",
            ".....",
            "..122",
            "..2??",
            "..3??",
            "..2??"
        };
        Solution.Solve(input).Should().BeEquivalentTo(new string[] {
            "3 2",
            "3 3",
            "3 4",
            "4 2",
            "4 3",
            "4 4"
        });
    }

    [Fact]
    public void OneLine()
    {
        var input = new string[] {
            "3 17",
            "9",
            ".................",
            "11112211232222211",
            "?????????????????"
        };
        Solution.Solve(input).Should().BeEquivalentTo(new string[] {
            "1 2",
            "4 2",
            "5 2",
            "8 2",
            "9 2",
            "10 2",
            "12 2",
            "13 2",
            "15 2"
        });
    }

    [Fact]
    public void TwoBlocks()
    {
        var input = new string[] {
            "5 5",
            "3",
            "??1..",
            "??1..",
            "11222",
            "..1??",
            "..1??"
        };
        Solution.Solve(input).Should().BeEquivalentTo(new string[] {
            "1 1",
            "3 3",
            "4 3"
        });
    }

    [Fact]
    public void FinalTest()
    {
        var input = new string[] {
            "9 9",
            "12",
            "?1....1??",
            "?2....12?",
            "?1.....11",
            "11.......",
            "12221..11",
            "????2112?",
            "????21???",
            "????32???",
            "????2????"
        };
        Solution.Solve(input).Should().BeEquivalentTo(new string[] {
            "0 0",
            "0 2",
            "0 5",
            "2 5",
            "3 5",
            "3 6",
            "3 8",
            "5 8",
            "6 6",
            "7 0",
            "8 1",
            "8 5"
        });
    }
}
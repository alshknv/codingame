using FluentAssertions;
using Xunit;

namespace Codingame;

public class ProgramTests
{
    [Fact]
    public void WarPAT()
    {
        var input = new string[] {
            "7","10D","9S","8D","KH","7D","5H","6S",
            "7","10H","7H","5C","QC","2C","4H","6D"
        };
        Solution.Solve(input).Should().Be("PAT");
    }

    [Fact]
    public void _3Cards()
    {
        var input = new string[] {
            "3",
            "AD",
            "KC",
            "QC",
            "3",
            "KH",
            "QS",
            "JC"
        };
        Solution.Solve(input).Should().Be("1 3");
    }

    [Fact]
    public void OneGameOneBattle()
    {
        var input = new string[] {
            "26",
            "10H","KD","6C","10S","8S","AD","QS","3D","7H","KH","9D","2D","JC","KS","3S","2S","QC","AC","JH","7D","KC","10D","4C","AS","5D","5S",
            "26",
            "2H","9C","8C","4S","5C","AH","JD","QH","7C","5H","4H","6H","6S","QD","9H","10C","4D","JS","6D","3H","8H","3C","7S","9S","8D","2C"
        };
        Solution.Solve(input).Should().Be("1 52");
    }
}
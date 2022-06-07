using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void SimpleAddition()
        {
            var result = Mayan.Calculate(4, 4, new string[] {
                ".oo.o...oo..ooo.oooo....o...oo..ooo.oooo____o...oo..ooo.oooo____o...oo..ooo.oooo",
                "o..o................____________________________________________________________",
                ".oo.........................................____________________________________",
                "................................................................________________"
            }, new string[] {
                "o...",
                "....",
                "....",
                "...."
            }, new string[] {
                "o...",
                "....",
                "....",
                "...."
            },
            "+");
            result.Should().HaveCount(4);
            result[0].Should().Be("oo..");
            result[1].Should().Be("....");
            result[2].Should().Be("....");
            result[3].Should().Be("....");
        }

        [Fact]
        public void Substraction()
        {
            var result = Mayan.Calculate(4, 4, new string[] {
                ".oo.o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo",
                "o..o................____________________________________________________________",
                ".oo.....................................________________________________________",
                "............................................................____________________"
            }, new string[] {
                "o...",
                "____",
                "....",
                "....",
                "ooo.",
                "....",
                "....",
                "...."
            }, new string[] {
                "oo..",
                "____",
                "....",
                "...."
            },
            "-");
            result.Should().HaveCount(8);
            result[0].Should().Be("....");
            result[1].Should().Be("____");
            result[2].Should().Be("....");
            result[3].Should().Be("....");
            result[4].Should().Be("o...");
            result[5].Should().Be("____");
            result[6].Should().Be("____");
            result[7].Should().Be("____");
        }

        [Fact]
        public void Zero()
        {
            var result = Mayan.Calculate(4, 4, new string[] {
                ".oo.o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo",
                "o..o................____________________________________________________________",
                ".oo.....................................________________________________________",
                "............................................................____________________"
            }, new string[] {
                "....",
                "____",
                "____",
                "____",
                "ooo.",
                "____",
                "....",
                "....",
                "oo..",
                "____",
                "____",
                "....",
                "o...",
                "____",
                "____",
                "____"
            }, new string[] {
                ".oo.",
                "o..o",
                ".oo.",
                "...."
            },
            "*");
            result.Should().HaveCount(4);
            result[0].Should().Be(".oo.");
            result[1].Should().Be("o..o");
            result[2].Should().Be(".oo.");
            result[3].Should().Be("....");
        }

        [Fact]
        public void Base20()
        {
            var result = Mayan.Calculate(1, 1, new string[] {
                "0123456789ABCDEFGHIJ"
            },
            new string[] { "1", "0", "0", "G" },
            new string[] { "2", "2" },
            "*");
            result.Should().HaveCount(5);
            result[0].Should().Be("2");
            result[1].Should().Be("2");
            result[2].Should().Be("1");
            result[3].Should().Be("D");
            result[4].Should().Be("C");
        }

        [Fact]
        public void GreatMultiplication()
        {
            var result = Mayan.Calculate(4, 4, new string[] {
                ".oo.o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo....o...oo..ooo.oooo",
                "o..o................____________________________________________________________",
                ".oo.....................................________________________________________",
                "............................................................____________________"
            }, new string[] {
                "o...",
                "....",
                "....",
                "....",
                "....",
                "____",
                "____",
                "....",
                "oo..",
                "____",
                "____",
                "____",
                "....",
                "____",
                "....",
                "...."
            }, new string[] {
                "oooo",
                "....",
                "....",
                "....",
                "ooo.",
                "____",
                "____",
                "____",
                "oo..",
                "____",
                "____",
                "....",
                "....",
                "____",
                "____",
                "....",
                "oo..",
                "____",
                "____",
                "...."
            },
            "*");
            result.Should().HaveCount(32);
            result[0].Should().Be("oo..");
            result[1].Should().Be("____");
            result[2].Should().Be("....");
            result[3].Should().Be("....");
            result[4].Should().Be("oo..");
            result[5].Should().Be("____");
            result[6].Should().Be("____");
            result[7].Should().Be("....");
            result[8].Should().Be("ooo.");
            result[9].Should().Be("....");
            result[10].Should().Be("....");
            result[11].Should().Be("....");
            result[12].Should().Be("oo..");
            result[13].Should().Be("____");
            result[14].Should().Be("____");
            result[15].Should().Be("____");
            result[16].Should().Be("oooo");
            result[17].Should().Be("....");
            result[18].Should().Be("....");
            result[19].Should().Be("....");
            result[20].Should().Be("oo..");
            result[21].Should().Be("....");
            result[22].Should().Be("....");
            result[23].Should().Be("....");
            result[24].Should().Be("oo..");
            result[25].Should().Be("____");
            result[26].Should().Be("____");
            result[27].Should().Be("____");
            result[28].Should().Be(".oo.");
            result[29].Should().Be("o..o");
            result[30].Should().Be(".oo.");
            result[31].Should().Be("....");
        }
    }
}

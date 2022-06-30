using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Example()
        {
            var connections = Player.ConnectAll(new string[] {
                "4.2",
                "...",
                "3.1"
            });
            connections.Should().BeEquivalentTo(new string[] {
                "0 0 2 0 2",
                "0 0 0 2 2",
                "0 2 2 2 1"
            });
        }

        [Fact]
        public void Simple()
        {
            var connections = Player.ConnectAll(new string[] {
                "1.3",
                "...",
                "123"
            });
            connections.Should().BeEquivalentTo(new string[] {
                "0 0 2 0 1",
                "2 0 2 2 2",
                "0 2 1 2 1",
                "1 2 2 2 1"
            });
        }

        [Fact]
        public void Intermediate1()
        {
            var connections = Player.ConnectAll(new string[] {
                "4.544",
                ".2...",
                "..5.4",
                "332.."
            });
            connections.Should().BeEquivalentTo(new string[] {
                "0 0 2 0 2",
                "0 0 0 3 2",
                "2 0 3 0 2",
                "2 0 2 2 1",
                "3 0 4 0 2",
                "4 0 4 2 2",
                "1 1 1 3 2",
                "2 2 4 2 2",
                "2 2 2 3 2",
                "0 3 1 3 1"
            });
        }

        [Fact]
        public void Intermediate2()
        {
            var connections = Player.ConnectAll(new string[] {
                "2..2.1.",
                ".3..5.3",
                ".2.1...",
                "2...2..",
                ".1....2"
            });
            connections.Should().BeEquivalentTo(new string[] {
                "0 0 3 0 1",
                "0 0 0 3 1",
                "3 0 5 0 1",
                "1 1 4 1 2",
                "1 1 1 2 1",
                "4 1 6 1 2",
                "4 1 4 3 1",
                "6 1 6 4 1",
                "1 2 3 2 1",
                "0 3 4 3 1",
                "1 4 6 4 1"
            });
        }

        [Fact]
        public void Intermediate3()
        {
            var connections = Player.ConnectAll(new string[] {
                "25.1",
                "47.4",
                "..1.",
                "3344"
            });
            connections.Should().BeEquivalentTo(new string[] {
                "0 0 1 0 2",
                "1 0 3 0 1",
                "1 0 1 1 2",
                "0 1 1 1 2",
                "0 1 0 3 2",
                "1 1 3 1 2",
                "1 1 1 3 1",
                "3 1 3 3 2",
                "2 2 2 3 1",
                "0 3 1 3 1",
                "1 3 2 3 1",
                "2 3 3 3 2"
            });
        }

        [Fact]
        public void Advanced()
        {
            return;
            var connections = Player.ConnectAll(new string[] {
                "3.4.6.2.",
                ".1......",
                "..2.5..2",
                "1.......",
                "..1.....",
                ".3..52.3",
                ".2.17..4",
                ".4..51.2"
            });
            connections.Should().BeEquivalentTo(new string[] {
                ""
            });
        }

        [Fact]
        public void MultipleSolutions()
        {
            var connections = Player.ConnectAll(new string[] {
                ".12..",
                ".2421",
                "24442",
                "1242.",
                "..21."
            });
            connections.Should().BeEquivalentTo(new string[] {
                "1 0 2 0 1",
                "2 0 2 1 1",
                "1 1 2 1 2",
                "2 1 3 1 1",
                "3 1 3 2 1",
                "4 1 4 2 1",
                "0 2 1 2 1",
                "0 2 0 3 1",
                "1 2 2 2 2",
                "1 2 1 3 1",
                "2 2 3 2 2",
                "3 2 4 2 1",
                "1 3 2 3 1",
                "2 3 3 3 2",
                "2 3 2 4 1",
                "2 4 3 4 1"
            });
        }

        [Fact]
        public void CG()
        {
            var connections = Player.ConnectAll(new string[] {
                "22221",
                "2....",
                "2....",
                "2....",
                "2....",
                "22321",
                ".....",
                ".....",
                "22321",
                "2....",
                "2....",
                "2.131",
                "2..2.",
                "2222."
            });

            connections.Should().BeEquivalentTo(new string[] {
                "0 0 1 0 1",
                "0 0 0 1 1",
                "1 0 2 0 1",
                "2 0 3 0 1",
                "3 0 4 0 1",
                "0 1 0 2 1",
                "0 2 0 3 1",
                "0 3 0 4 1",
                "0 4 0 5 1",
                "0 5 1 5 1",
                "1 5 2 5 1",
                "2 5 3 5 1",
                "2 5 2 8 1",
                "3 5 4 5 1",
                "0 8 1 8 1",
                "0 8 0 9 1",
                "1 8 2 8 1",
                "2 8 3 8 1",
                "3 8 4 8 1",
                "0 9 0 10 1",
                "0 10 0 11 1",
                "0 11 0 12 1",
                "2 11 3 11 1",
                "3 11 4 11 1",
                "3 11 3 12 1",
                "0 12 0 13 1",
                "3 12 3 13 1",
                "0 13 1 13 1",
                "1 13 2 13 1",
                "2 13 3 13 1"
            });
        }
    }
}

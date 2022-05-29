using System;
using System.Collections.Generic;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class PlayerTests
    {
        [Fact]
        public void SimpleExample1()
        {
            var path = Solution.GetPath(new string[] {
                "######",
                "#@E $#",
                "# N  #",
                "#X   #",
                "######",
            });

            path.Should().HaveCount(5);
            path[0].Should().Be("SOUTH");
            path[1].Should().Be("EAST");
            path[2].Should().Be("NORTH");
            path[3].Should().Be("EAST");
            path[4].Should().Be("EAST");
        }

        [Fact]
        public void Example2()
        {
            var path = Solution.GetPath(new string[] {
                "##########",
                "#        #",
                "#  S   W #",
                "#        #",
                "#  $     #",
                "#        #",
                "#@       #",
                "#        #",
                "#E     N #",
                "##########"
            });

            path.Should().HaveCount(20);
            path[0].Should().Be("SOUTH");
            path[1].Should().Be("SOUTH");
            path[2].Should().Be("EAST");
            path[3].Should().Be("EAST");
            path[4].Should().Be("EAST");
            path[5].Should().Be("EAST");
            path[6].Should().Be("EAST");
            path[7].Should().Be("EAST");
            path[8].Should().Be("NORTH");
            path[9].Should().Be("NORTH");
            path[10].Should().Be("NORTH");
            path[11].Should().Be("NORTH");
            path[12].Should().Be("NORTH");
            path[13].Should().Be("NORTH");
            path[14].Should().Be("WEST");
            path[15].Should().Be("WEST");
            path[16].Should().Be("WEST");
            path[17].Should().Be("WEST");
            path[18].Should().Be("SOUTH");
            path[19].Should().Be("SOUTH");
        }

        [Fact]
        public void Teleport()
        {
            var path = Solution.GetPath(new string[] {
                "##########",
                "#    T   #",
                "#        #",
                "#        #",
                "#        #",
                "#@       #",
                "#        #",
                "#        #",
                "#    T  $#",
                "##########"
            });

            path.Should().HaveCount(17);
            path[0].Should().Be("SOUTH");
            path[1].Should().Be("SOUTH");
            path[2].Should().Be("SOUTH");
            path[3].Should().Be("EAST");
            path[4].Should().Be("EAST");
            path[5].Should().Be("EAST");
            path[6].Should().Be("EAST");
            path[7].Should().Be("EAST");
            path[8].Should().Be("EAST");
            path[9].Should().Be("EAST");
            path[10].Should().Be("SOUTH");
            path[11].Should().Be("SOUTH");
            path[12].Should().Be("SOUTH");
            path[13].Should().Be("SOUTH");
            path[14].Should().Be("SOUTH");
            path[15].Should().Be("SOUTH");
            path[16].Should().Be("SOUTH");
        }
    }
}

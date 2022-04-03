using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_Step1()
        {
            var graph = new int[4][] {
                new int[] {1,2},
                new int[] {0,3},
                new int[] {0,3},
                new int[] {1,2}
            };
            var exits = new int[] { 3 };
            var move = Player.MakeMove(graph, exits, 0);
            Assert.Equal((1, 3), move);
        }

        [Fact]
        public void Test1_Step2()
        {
            var graph = new int[4][] {
                new int[] {1,2},
                new int[] {0,-1},
                new int[] {0,3},
                new int[] {-1,2}
            };
            var exits = new int[] { 3 };
            var move = Player.MakeMove(graph, exits, 2);
            Assert.Equal((2, 3), move);
        }
    }
}

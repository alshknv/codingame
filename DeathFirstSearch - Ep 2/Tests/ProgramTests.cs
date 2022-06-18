using System;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
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

        [Fact]
        public void FindPath_1()
        {
            var links = new (int, int)[14] {
                ( 0, 1 ),
                ( 0, 2 ),
                ( 0, 3 ),
                ( 1, 2 ),
                ( 2, 3 ),
                ( 1, 4 ),
                ( 2, 4 ),
                ( 2, 5 ),
                ( 3, 5 ),
                ( 4, 5 ),
                ( 4, 6 ),
                ( 4, 7 ),
                ( 5, 8 ),
                ( 5, 9 )
            };
            var graph = Player.BuildGraph(10, links);
            var exits = new int[] { 6, 7, 8, 9 };
            var path = Player.FindPath(7, 0, graph, exits, 1);
            Assert.Equal(new (int, bool)[] { (0, false), (1, false), (4, true), (7, false) }, path);
        }

        [Fact]
        public void LinkedDoubleGateways()
        {
            var links = new (int, int)[14] {
                ( 0, 1 ),
                ( 0, 2 ),
                ( 0, 3 ),
                ( 1, 2 ),
                ( 2, 3 ),
                ( 1, 4 ),
                ( 2, 4 ),
                ( 2, 5 ),
                ( 3, 5 ),
                ( 4, 5 ),
                ( 4, 6 ),
                ( 4, 7 ),
                ( 5, 8 ),
                ( 5, 9 )
            };
            var graph = Player.BuildGraph(10, links);
            var exits = new int[] { 6, 7, 8, 9 };
            var move = Player.MakeMove(graph, exits, 0);
            Assert.Equal((4, 6), move);
        }

        [Fact]
        public void RobustDoubleGateways_Step1()
        {
            var links = new (int, int)[13] {
                ( 0, 1 ),
                ( 0, 2 ),
                ( 0, 5 ),
                ( 1, 3 ),
                ( 1, 5 ),
                ( 2, 4 ),
                ( 2, 5 ),
                ( 3, 6 ),
                ( 3, 5 ),
                ( 4, 7 ),
                ( 4, 5 ),
                ( 5, 6 ),
                ( 5, 7 )
            };
            var graph = Player.BuildGraph(8, links);
            var exits = new int[] { 6, 7 };
            var move = Player.MakeMove(graph, exits, 0);
            Assert.Equal((5, 6), move);
        }

        [Fact]
        public void RobustDoubleGateways_Step2()
        {
            var links = new (int, int)[13] {
                ( 0, 1 ),
                ( 0, 2 ),
                ( 0, 5 ),
                ( 1, 3 ),
                ( 1, 5 ),
                ( 2, 4 ),
                ( 2, 5 ),
                ( 3, 6 ),
                ( 3, 5 ),
                ( 4, 7 ),
                ( 4, 5 ),
                ( 5, 6 ),
                ( 5, 7 )
            };
            var graph = Player.BuildGraph(8, links);
            graph[5][5] = -1;
            graph[6][1] = -1;
            var exits = new int[] { 6, 7 };
            var move = Player.MakeMove(graph, exits, 5);
            Assert.Equal((5, 7), move);
        }
    }
}

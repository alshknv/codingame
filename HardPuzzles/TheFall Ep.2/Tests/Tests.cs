using System.Collections.Generic;
using System;
using Xunit;
using Codingame;

namespace Codingame
{
    public class UnitTest1
    {
        [Fact]
        public void GetRotations()
        {
            var grid = new int[2, 4] {
                {4,11,11,2},{3,10,4,3}
            };

            var (rotations, path, rotatedGrid) = Player.FindRotations(2, 4, grid, (1, 0), "TOP", 1);
            Assert.Collection(rotations,
                _ => Assert.Equal((0, 1, "RIGHT"), _),
                _ => Assert.Equal((1, 2, "LEFT"), _)
            );
        }

        [Fact]
        public void GetRotations2()
        {
            var grid = new int[4, 4] {
                {0,12,6,10},{0,2,0,0},{3,4,0,0},{0,13,3,6}
            };

            var (rotations, path, rotatedGrid) = Player.FindRotations(4, 4, grid, (2, 0), "TOP", 3);
            Assert.Collection(rotations,
                _ => Assert.Equal((2, 1, "LEFT"), _),
                _ => Assert.Equal((3, 3, "LEFT"), _)
            );
        }

        [Fact]
        public void NoRotations()
        {
            var grid = new int[3, 3] {
                {0,0,0},{3,3,3 },{0,0,0 }
            };

            var (rotations, _, _) = Player.FindRotations(3, 3, grid, (2, 0), "TOP", 3);
            Assert.Empty(rotations);
        }

        [Fact]
        public void BrokenSewer()
        {
            var grid1 = new int[8, 4] {
                {0,0,0,0}, { -3, 12, 0, -12}, {0,3,0,3}, {0,3,0,2}, {0,2,0,2}, {0,3,0,3},{0,12,2,13},{0,0,0,0}
            };
            var (rotations1, _, _) = Player.FindRotations(8, 4, grid1, (1, 0), "TOP", 1);
            Assert.Equal(rotations1[0], (1, 1, "LEFT"));
            var grid2 = new int[8, 4] {
                {0,0,0,0}, { -3, 11, 0, -12}, {0,3,0,3}, {0,3,0,2}, {0,2,0,2}, {0,3,0,3},{0,12,2,13},{0,0,0,0}
            };
            var (rotations2, _, _) = Player.FindRotations(8, 4, grid2, (1, 1), "TOP", 1);
            Assert.Equal(rotations2[0], (2, 1, "LEFT"));
        }

        [Fact]
        public void BrokenMausoleum()
        {
            var grid = new int[,] {
                {-3,11,0,0,0,0,0,0,0,0}, {12,5,11,0,0,0,0,12,11,0}, {8,13,2,0,11,3,11,6,4,-3}, {6,0,2,0,2,0,3,3,2,12}, {3,0,3,0,3,0,3,2,3,7},
                {2,0,3,12,1,6,10,3,2,7}, {7,3,8,8,5,8,11,3,2,13}, {2,0,2,3,2,0,2,6,11,13}, {7,3,-9,1,10,0,3,3,12,4}, {0,0,2,3,0,0,2,3,13,5},
                {0,0,3,2,0,0,3,2,13,4}, {0,0,13,7,11,0,2,3,13,10}, {0,0,0,0,13,2,8,12,0,0}
            };

            var (rotations, path, rotatedGrid) = Player.FindRotations(13, 10, grid, (0, 0), "TOP", 2);
            Assert.Collection(rotations,
                _ => Assert.Equal((4, 2, "LEFT"), _),
                _ => Assert.Equal((5, 2, "LEFT"), _),
                _ => Assert.Equal((6, 2, "LEFT"), _),
                _ => Assert.Equal((6, 2, "LEFT"), _),
                _ => Assert.Equal((6, 4, "LEFT"), _),
                _ => Assert.Equal((6, 5, "LEFT"), _),
                _ => Assert.Equal((8, 6, "LEFT"), _),
                _ => Assert.Equal((10, 6, "LEFT"), _),
                _ => Assert.Equal((12, 7, "LEFT"), _),
                _ => Assert.Equal((12, 7, "LEFT"), _),
                _ => Assert.Equal((11, 7, "LEFT"), _),
                _ => Assert.Equal((9, 7, "LEFT"), _),
                _ => Assert.Equal((8, 7, "LEFT"), _),
                _ => Assert.Equal((6, 7, "LEFT"), _),
                _ => Assert.Equal((5, 7, "LEFT"), _),
                _ => Assert.Equal((3, 7, "LEFT"), _),
                _ => Assert.Equal((2, 8, "LEFT"), _)
            );
        }

        [Fact]
        public void CrossRockPath()
        {
            var grid = new int[,] {
                {0,0,0,0,0,0,0},{0,7,-7,9,-7,7,-7},{0,-2,-2,-2,-2,-2,-2},{0,2,3,3,3,3,2},{0,-2,2,2,2,2,2},{0,2,2,2,2,2,2},
                {0,-2,2,2,2,2,2},{0,2,2,2,2,2,2},{-3,10,2,2,2,2,2},{0,0,-2,-2,-2,-2,-2}
            };
            var (_, indyPath, _) = Player.FindRotations(10, 7, grid, (1, 2), "TOP", 1);
            var rockPath = Player.FindPath(10, 7, grid, (5, 6), "RIGHT", 1);
            Assert.Equal(indyPath[3], rockPath[3]);
        }
    }
}

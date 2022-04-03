using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FindPath1()
        {
            var data = new string[] {
                "##############################",
                "#............................#",
                "#.##########################.#",
                "#.....T.........#...#.#......#",
                "#.......................#.#..#",
                "#.#######################.#..#",
                "#.....##......##......#....###",
                "#...####..##..##..##..#..#...#",
                "#.........##......##.....#.C.#",
                "##############################"
            };

            var player = new Player(data.Length, data[0].Length, 0);
            var rick = (3, 6);
            player.UpdateMap(data);
            var (IsRelaible, Steps) = player.FindPath(rick, (8, 27));
            Assert.True(IsRelaible);
            Assert.Collection(Steps, _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("UP", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("DOWN", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _));
        }

        [Fact]
        public void FindPath2()
        {
            var data = new string[] {
                "##############################",
                "#............................#",
                "#.##########################.#",
                "#.....T.........#..???????????",
                "#..................???????????",
                "#.#################???????????",
                "#.....##......????????????????",
                "#...####..##..????????????????",
                "#.........##..????????????????",
                "##############????????????????"
            };

            var player = new Player(data.Length, data[0].Length, 0);
            var rick = (3, 6);
            player.UpdateMap(data);
            var (IsRelaible, Steps) = player.FindPath(rick, (8, 27));
            Assert.False(IsRelaible);
            Assert.Collection(Steps, _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _),
                _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _), _ => Assert.Equal("DOWN", _),
                _ => Assert.Equal("DOWN", _), _ => Assert.Equal("RIGHT", _));
        }

        [Fact]
        public void Explore1()
        {
            var data = new string[] {
                "??????????????????????????????",
                "??????????????????????????????",
                "???#####??????????????????????",
                "???#####??????????????????????",
                "???##T..??????????????????????",
                "???#####??????????????????????",
                "???#####??????????????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????"
            };

            var player = new Player(data.Length, data[0].Length, 30);
            var rick = (4, 5);
            player.UpdateMap(data);
            var move = player.MakeMove(rick, data);
            Assert.Equal("RIGHT", move);
        }

        [Fact]
        public void Explore2()
        {
            var data = new string[] {
                "##############################",
                "#T...........................#",
                "##...........................#",
                "#............................#",
                "#............................#",
                "#............................#",
                "#............................#",
                "#............................#",
                "#...........................C#",
                "##############################"
            };

            var player = new Player(data.Length, data[0].Length, 90);
            var rick = (1, 1);
            player.UpdateMap(data);
            var move = player.MakeMove(rick, data);
            //Assert.Equal("RIGHT", move);
        }

        [Fact]
        public void NoPath()
        {
            var data = new string[] {
                "??????????????????????????????",
                "??????????????????????????????",
                "???############???????????????",
                "???############???????????????",
                "???##......T.C#???????????????",
                "???############???????????????",
                "???############???????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????"
            };

            var player = new Player(data.Length, data[0].Length, 30);
            var rick = (4, 11);
            player.UpdateMap(data);
            var path = player.FindPath(rick, (8, 27));
            Assert.Empty(path.Steps);
        }

        [Fact]
        public void PathToWall()
        {
            var data = new string[] {
                "??????????????????????????????",
                "????????????????#######???????",
                "????????????????.......???????",
                "????????????????#######???????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????",
                "??????????????????????????????"
            };

            var player = new Player(data.Length, data[0].Length, 30);
            var rick = (2, 20);
            player.UpdateMap(data);
            var path = player.FindPath(rick, (3, 18));
            Assert.Empty(path.Steps);
        }
    }
}

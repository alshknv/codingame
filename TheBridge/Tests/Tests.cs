using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Sacrifice()
        {
            var data = new string[] {
                "...0......",
                "...00.....",
                "...0..0...",
                "...0......"
            };

            var bridge = Player.InitBridge(data);
            var bikes = new (int L, int C, int S, bool Active)[] {
                (1,0,0,true),
                (2,0,0,true)
            };
            var strategy = Player.CalculateStrategy(bridge, 1, bikes, 1);
            Assert.Collection(strategy,
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("UP", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _));
        }

        [Fact]
        public void JumpsOfIncreasingLength()
        {
            var data = new string[] {
                "..........000......0000..............000000.............",
                "..........000......0000..............000000.............",
                "..........000......0000..............000000.............",
                "..........000......0000..............000000............."
            };
            var bridge = Player.InitBridge(data);
            var bikes = new (int L, int C, int S, bool Active)[] {
                (0,0,0,true),
                (1,0,0,true),
                (2,0,0,true),
                (3,0,0,true)
            };
            var strategy = Player.CalculateStrategy(bridge, 1, bikes, 4);
            Assert.Collection(strategy,
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _));
        }

        [Fact]
        public void JumpsOfDecreasingLength()
        {
            var data = new string[] {
                "..............00000......0000.....00......",
                "..............00000......0000.....00......",
                "..............00000......0000.....00......",
                "..............00000......0000.....00......"
            };
            var bridge = Player.InitBridge(data);
            var bikes = new (int L, int C, int S, bool Active)[] {
                (0,0,0,true),
                (1,0,0,true),
                (2,0,0,true),
                (3,0,0,true)
            };
            var strategy = Player.CalculateStrategy(bridge, 8, bikes, 4);
            Assert.Collection(strategy,
                _ => Assert.Equal("SLOW", _),
                _ => Assert.Equal("SLOW", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SLOW", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SLOW", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _));
        }

        [Fact]
        public void BigJumpHoleColumns()
        {
            var data = new string[] {
                ".........0000000............................0.0.0.0.0.0.0.0.....",
                ".................0..........................0.0.0.0.0.0.0.0.....",
                ".........0000000............................0.0.0.0.0.0.0.0.....",
                "............................................0.0.0.0.0.0.0.0....."
            };
            var bridge = Player.InitBridge(data);
            var bikes = new (int L, int C, int S, bool Active)[] {
                (0,0,0,true),
                (1,0,0,true),
                (2,0,0,true)
            };
            var strategy = Player.CalculateStrategy(bridge, 7, bikes, 2);
            Assert.Collection(strategy,
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("SPEED", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("JUMP", _),
                _ => Assert.Equal("SPEED", _));
        }
    }
}

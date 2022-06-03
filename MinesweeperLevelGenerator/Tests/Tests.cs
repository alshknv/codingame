using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SimpleLevel()
        {
            var result = Problem.Solve(6, 6, 7, 3, 3, 31);
            Assert.Collection(result,
                l1 => Assert.Equal("1#2##1", l1),
                l2 => Assert.Equal("222221", l2),
                l3 => Assert.Equal("#1..11", l3),
                l4 => Assert.Equal("331.1#", l4),
                l5 => Assert.Equal("##1.11", l5),
                l6 => Assert.Equal("221...", l6));
        }

        [Fact]
        public void CornerSelection()
        {
            var result = Problem.Solve(6, 6, 6, 0, 0, 3);
            Assert.Collection(result,
                l1 => Assert.Equal("...111", l1),
                l2 => Assert.Equal("...1#1", l2),
                l3 => Assert.Equal("11.122", l3),
                l4 => Assert.Equal("#1112#", l4),
                l5 => Assert.Equal("122#32", l5),
                l6 => Assert.Equal(".1#3#1", l6));
        }

        [Fact]
        public void EdgeSelection()
        {
            var result = Problem.Solve(6, 6, 6, 2, 0, 31);
            Assert.Collection(result,
                l1 => Assert.Equal("...1#1", l1),
                l2 => Assert.Equal(".11211", l2),
                l3 => Assert.Equal(".1#111", l3),
                l4 => Assert.Equal("13432#", l4),
                l5 => Assert.Equal("1###21", l5),
                l6 => Assert.Equal("12321.", l6));
        }

        [Fact]
        public void BeginnerLevel()
        {
            var result = Problem.Solve(9, 9, 10, 4, 4, 1);
            Assert.Collection(result,
                l1 => Assert.Equal("11..11211", l1),
                l2 => Assert.Equal("#1..1#2#1", l2),
                l3 => Assert.Equal("11..11211", l3),
                l4 => Assert.Equal("111....11", l4),
                l5 => Assert.Equal("1#1....1#", l5),
                l6 => Assert.Equal("111..1121", l6),
                l7 => Assert.Equal(".111.1#21", l7),
                l8 => Assert.Equal(".1#21223#", l8),
                l9 => Assert.Equal(".12#11#21", l9));
        }

        [Fact]
        public void IntermediateLevel()
        {
            var result = Problem.Solve(16, 16, 40, 8, 8, 17);
            Assert.Collection(result,
                l1 => Assert.Equal(".1#1.1#1....1#1.", l1),
                l2 => Assert.Equal(".1111221..11211.", l2),
                l3 => Assert.Equal("....1#11222#2221", l3),
                l4 => Assert.Equal("....1112##213##1", l4),
                l5 => Assert.Equal("......13#31.2#31", l5),
                l6 => Assert.Equal("1221..1#21..111.", l6),
                l7 => Assert.Equal("2##1..111111111.", l7),
                l8 => Assert.Equal("3#41.....1#12#2.", l8),
                l9 => Assert.Equal("2#311....1123#31", l9),
                l10 => Assert.Equal("223#31.....2#5#1", l10),
                l11 => Assert.Equal("#23##2111..2##32", l11),
                l12 => Assert.Equal("12#5#33#311134#1", l12),
                l13 => Assert.Equal(".12#22##3#211#21", l13),
                l14 => Assert.Equal(".122112222#2332.", l14),
                l15 => Assert.Equal(".1#1.....112##1.", l15),
                l16 => Assert.Equal(".111.......1221.", l16));
        }

        [Fact]
        public void ExpertLevel()
        {
            var result = Problem.Solve(30, 16, 99, 15, 8, 17);
            Assert.Collection(result,
                l1 => Assert.Equal("112#1....1#2#22##1..112#1.....", l1),
                l2 => Assert.Equal("1#322....11223#321.13#311.....", l2),
                l3 => Assert.Equal("222#211....13#3211.2##21221122", l3),
                l4 => Assert.Equal("#3213#311..1##43#213#311##11##", l4),
                l5 => Assert.Equal("##213#4#2..123##22#211.1221133", l5),
                l6 => Assert.Equal("233#213#311..2331222..111.112#", l6),
                l7 => Assert.Equal(".1#222322#1..1#1.1#21.1#212#43", l7),
                l8 => Assert.Equal(".1222##233422221.23#1.112#34##", l8),
                l9 => Assert.Equal("..2#544#2###2#1.13#3211.224###", l9),
                l10 => Assert.Equal("..2###322343211.1##22#311#3#64", l10),
                l11 => Assert.Equal(".12455#213#2....12223##32233##", l11),
                l12 => Assert.Equal(".1#2##22#4#2...12333#4##22#34#", l12),
                l13 => Assert.Equal("133322112#21...1####3335#32#21", l13),
                l14 => Assert.Equal("1##1..12321....1234#32#3#33231", l14),
                l15 => Assert.Equal("1221..1##21.......12#2122#3#3#", l15),
                l16 => Assert.Equal("......123#1........111..113#31", l16));
        }
    }
}

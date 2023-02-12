using System;
using Xunit;
using Codingame;
using FluentAssertions;
namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Easy()
        {
            var input = new string[] {
                "56..1..2. aabbccdde",
                "..72..68. afghhiide",
                "..2.87.15 jfggklmme",
                "......3.9 jjgnklopp",
                ".7....2.. qqgnooorr",
                "9.634.8.. stuuvwwxx",
                "2.9..8... stuuvvwyz",
                "..41.2... sAuuByyyz",
                ".8.4...3. CADDBEEFF",
                "a=12 b=17 c=4 d=14 e=15 f=13 g=19 h=7 i=10 j=16 k=10 l=13 m=10 n=15 o=15 p=13 q=11 r=11 s=18 t=3 u=28 v=15 w=20 x=8 y=22 z=12 A=11 B=13 C=6 D=9 E=10 F=5"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "568913427",
                    "197254683",
                    "342687915",
                    "851726349",
                    "473891256",
                    "926345871",
                    "219538764",
                    "734162598",
                    "685479132"
                }
            );
        }

        [Fact]
        public void Medium()
        {
            var input = new string[] {
                ".1..65..7 abcccdeee",
                "6.7....15 fbbgddhie",
                ".54..1..3 fjggdkhil",
                "......... fjmgkknil",
                "16..3.5.. ooppqqnrr",
                "..8..2... sotuvqnnw",
                "3......7. xxtuyyzzA",
                "...3..... BCDDEFzAA",
                ".2.1..349 BBDGGGzzz",
                "a=9 b=16 c=17 d=21 e=18 f=12 g=18 h=15 i=12 j=12 k=15 l=5 m=9 n=19 o=10 p=6 q=12 r=17 s=5 t=13 u=15 v=1 w=4 x=7 y=11 z=33 A=12 B=17 C=9 D=10 E=7 F=4 G=14"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "913865427",
                    "687243915",
                    "254791683",
                    "479586132",
                    "162437598",
                    "538912764",
                    "345629871",
                    "891374256",
                    "726158349"
                }
            );
        }

        [Fact]
        public void Hard()
        {
            var input = new string[] {
                "........1 abbcdddef",
                "....5.... agccchfff",
                "...3..... ijjjhhkkl",
                ".......9. ijmmnnokp",
                "..6...... qrrsttuvv",
                "....9.7.. qwxsyuuzA",
                ".....3... qwxxyBBCA",
                "......... qDxEEBBCC",
                "..8..5... DDFFFBGHH",
                "a=14 b=9 c=17 d=22 e=3 f=20 g=3 h=9 i=6 j=21 k=19 l=7 m=9 n=11 o=4 p=6 q=16 r=15 s=11 t=11 u=10 v=9 w=9 x=20 y=16 z=5 A=5 B=22 C=19 D=17 E=11 F=15 G=3 H=11"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "672489531",
                    "831752649",
                    "549316827",
                    "157238496",
                    "396547218",
                    "284691753",
                    "415873962",
                    "763924185",
                    "928165374"
                }
            );
        }

        [Fact]
        public void Expert()
        {
            var input = new string[] {
                "......... abbcccddd",
                "......... aabefghii",
                "......... jkkefghil",
                "......... jmnnnooil",
                "......... mmmppqqrl",
                "......... sttpuuqrr",
                "......... svwwwxxxy",
                "......... zvvABBCCy",
                "......... zDDAAEEFF",
                "a=7 b=23 c=13 d=13 e=5 f=10 g=17 h=11 i=28 j=12 k=8 l=16 m=20 n=9 o=11 p=22 q=14 r=8 s=15 t=13 u=4 v=17 w=11 x=16 y=8 z=4 A=19 B=10 C=7 D=13 E=15 F=6"
            };
            Solution.Solve(input).Should().BeEquivalentTo(
                new string[] {
                    "496175238",
                    "218369547",
                    "753248691",
                    "531627489",
                    "827594316",
                    "649813752",
                    "962451873",
                    "374982165",
                    "185736924"
                }
            );
        }
    }
}
using System;
using System.Collections.Generic;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Simple1()
        {
            var value = Solution.GetMinimumCableLength(new string[] {
                "0 0", "1 1","2 2"
            });

            value.Should().Be(4);
        }

        [Fact]
        public void Simple2()
        {
            var value = Solution.GetMinimumCableLength(new string[] {
                "1 2","0 0","2 2"
            });

            value.Should().Be(4);
        }

        [Fact]
        public void LargeNumbers()
        {
            var value = Solution.GetMinimumCableLength(new string[] {
                "-28189131 593661218",
                "102460950 1038903636",
                "938059973 -816049599",
                "-334087877 -290840615",
                "842560881 -116496866",
                "-416604701 690825290",
                "19715507 470868309",
                "846505116 -694479954"
            });
            value.Should().Be(6066790161);
        }

        [Fact]
        public void ComplexCase()
        {
            var value = Solution.GetMinimumCableLength(new string[] {
                "-162526110 -252675912",
                "-4895917 -240420085",
                "141008358 -106615672",
                "206758372 -63665546",
                "88473194 -37289256",
                "202531345 73399429",
                "-135195154 157092065",
                "171101176 161166515",
                "-266264470 191334680",
                "-205060869 233111863",
                "-137959173 262220087"
            });
            value.Should().Be(2178614523);
        }
    }
}

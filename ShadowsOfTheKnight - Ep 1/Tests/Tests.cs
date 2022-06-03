using System;
using Xunit;
using Codingame;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var newReg = Player.NewRegion((0, 0, 3, 7), (2, 3), "DR");
            var moveTo = Player.RegionCenter(newReg);
            Assert.Equal((3, 6), moveTo);
        }
    }
}

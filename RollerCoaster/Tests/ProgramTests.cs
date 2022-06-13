using System;
using System.IO;
using Xunit;
using Codingame;
using FluentAssertions;

namespace Tests
{
    public class ProgramTests
    {
        [Fact]
        public void Example1()
        {
            Solution.TotalEarnings(3, 3, new[] { 3, 1, 1, 2 }).Should().Be(7);
        }

        [Fact]
        public void Example2()
        {
            Solution.TotalEarnings(5, 3, new[] { 2, 3, 5, 4 }).Should().Be(14);
        }

        [Fact]
        public void Example3()
        {
            Solution.TotalEarnings(10, 100, new[] { 1 }).Should().Be(100);
        }

        [Fact]
        public void Cycle()
        {
            Solution.TotalEarnings(5, 3, new[] { 2, 3, 5, 3 }).Should().Be(15);
        }

        [Fact]
        public void LargeDataset()
        {
            var dataset = File.ReadAllLines("../../../../Tests/large-dataset.txt");
            var inputs = dataset[0].Split(' ');
            var L = int.Parse(inputs[0]);
            var C = int.Parse(inputs[1]);
            var N = int.Parse(inputs[2]);
            var groups = new int[N];
            for (int i = 0; i < N; i++)
            {
                groups[i] = int.Parse(dataset[1 + i]);
            }
            Solution.TotalEarnings(L, C, groups).Should().Be(89744892565569);
        }

        [Fact]
        public void AllAtLeastOnce()
        {
            Solution.TotalEarnings(10000, 10, new[] { 100, 200, 300, 400, 500 }).Should().Be(15000);
        }
    }
}

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
namespace Codingame
{
    public static class Problem
    {
        private static uint rnd;
        private static string NeighboringMines(bool[,] grid, int r, int c, int rows, int cols)
        {
            var count = 0;
            for (int i = -1; i <= 1; i++)
            {
                if (r + i < 0 || r + i >= rows) continue;
                for (int j = -1; j <= 1; j++)
                {
                    if ((i == 0 && j == 0) || c + j < 0 || c + j >= cols) continue;
                    if (grid[r + i, c + j]) count++;
                }
            }
            return count == 0 ? "." : count.ToString();
        }

        private static string[] ConstructLevel(bool[,] grid, int rows, int cols)
        {
            string[] level = new string[rows];
            for (int row = 0; row < rows; row++)
            {
                var line = new string[cols];
                for (int col = 0; col < cols; col++)
                {
                    if (grid[row, col])
                        line[col] = "#";
                    else
                        line[col] = NeighboringMines(grid, row, col, rows, cols);
                }
                level[row] = string.Concat(line);
            }
            return level;
        }

        private static uint Lcp()
        {
            rnd = ((214013 * rnd) + 2531011) / 65536 % uint.MaxValue;
            return rnd;
        }

        private static bool[,] MineGrid(int rows, int cols, int n, int r, int c, int seed)
        {
            var grid = new bool[rows, cols];
            var minesPlaced = 0;
            rnd = (uint)seed;
            while (minesPlaced < n)
            {
                int x, y;
                bool placedOk;
                do
                {
                    placedOk = true;
                    x = (int)(Lcp() % cols);
                    y = (int)(Lcp() % rows);
                    if (grid[y, x] || (Math.Abs(r - y) < 2 && Math.Abs(c - x) < 2))
                        placedOk = false;
                } while (!placedOk);
                grid[y, x] = true;
                minesPlaced++;
            }
            return grid;
        }

        public static string[] Solve(int width, int height, int n, int x, int y, int seed)
        {
            var mineGrid = MineGrid(height, width, n, y, x, seed);
            return ConstructLevel(mineGrid, height, width);
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int width = int.Parse(inputs[0]);
            int height = int.Parse(inputs[1]);
            int mines = int.Parse(inputs[2]);
            int x = int.Parse(inputs[3]);
            int y = int.Parse(inputs[4]);
            int seed = int.Parse(inputs[5]);

            foreach (var line in Solve(width, height, mines, x, y, seed))
            {
                Console.WriteLine(line);
            }
        }
    }
}
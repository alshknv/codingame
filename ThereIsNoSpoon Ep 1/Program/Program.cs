using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public static class Player
    {
        public static string[] NeighborScan(int height, int width, string[] lines)
        {
            bool[][] field = new bool[height][];
            for (int i = 0; i < height; i++)
            {
                Console.Error.WriteLine(lines[i]);
                field[i] = lines[i].Select(x => x == '0').ToArray();
            }

            List<string> result = new List<string>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[y][x])
                    {
                        string neighborRight = null;
                        string neighborBottom = null;
                        for (int k = x + 1; k < width; k++)
                        {
                            if (field[y][k])
                            {
                                neighborRight = $"{k} {y}";
                                break;
                            }
                        }
                        if (neighborRight == null) neighborRight = "-1 -1";
                        for (int k = y + 1; k < height; k++)
                        {
                            if (field[k][x])
                            {
                                neighborBottom = $"{x} {k}";
                                break;
                            }
                        }
                        if (neighborBottom == null) neighborBottom = "-1 -1";
                        result.Add($"{x} {y} {neighborRight} {neighborBottom}");
                    }
                }
            }
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
            int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis

            string[] lines = new string[height];
            for (int i = 0; i < height; i++)
            {
                lines[i] = Console.ReadLine(); // width characters, each either 0 or .
            }

            var scanResult = NeighborScan(height, width, lines);
            for (int i = 0; i < scanResult.Length; i++)
            {
                Console.WriteLine(scanResult[i]);
            }
        }
    }
}
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public static class Solution
    {
        public static int[] GetAnswers(string[] mapData, string[] queries)
        {
            // init
            var H = mapData.Length;
            var W = mapData[0].Length;
            var map = new bool[W][];
            var surface = new int[W][];
            var check = new bool[W][];
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H; y++)
                {
                    map[x] = new bool[H];
                    surface[x] = new int[H];
                    check[x] = new bool[H];
                }
            }

            // fill map data
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W; x++)
                {
                    map[x][y] = mapData[y][x] == 'O';
                    surface[x][y] = -1;
                }
            }

            // process queries
            var answers = new int[queries.Length];
            for (int i = 0; i < queries.Length; i++)
            {
                var query = queries[i].Split(' ');
                var x = int.Parse(query[0]);
                var y = int.Parse(query[1]);
                // if we know lake size already return it
                if (surface[x][y] >= 0)
                {
                    answers[i] = surface[x][y];
                    continue;
                }

                // reinit check array with false values
                check = check.Select(x => x.Select(y => false).ToArray()).ToArray();

                var lakeCells = new List<(int x, int y)>();
                var queue = new Queue<(int x, int y)>();
                queue.Enqueue((x, y));
                while (queue.Count > 0)
                {
                    var cell = queue.Dequeue();
                    if (check[cell.x][cell.y]) continue; // if cell was checked already - skip it
                    check[cell.x][cell.y] = true; // mark checked
                    if (map[cell.x][cell.y]) //if it is lake store in the list and add all ajacent cells to queue
                    {
                        lakeCells.Add((cell.x, cell.y));
                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                if (Math.Abs(dx) == Math.Abs(dy) || cell.x + dx >= W || cell.x + dx < 0 ||
                                    cell.y + dy >= H || cell.y + dy < 0)
                                {
                                    continue;
                                }
                                queue.Enqueue((cell.x + dx, cell.y + dy));
                            }
                        }
                    }
                }
                if (lakeCells.Count > 0)
                {
                    foreach (var cell in lakeCells)
                    {
                        surface[cell.x][cell.y] = lakeCells.Count;
                    }
                }
                else
                {
                    surface[x][y] = 0;
                }
                answers[i] = lakeCells.Count;
            }
            return answers;
        }

        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());
            string[] mapData = new string[H];
            for (int i = 0; i < H; i++)
            {
                mapData[i] = Console.ReadLine();
            }
            int N = int.Parse(Console.ReadLine());
            var queries = new string[N];
            for (int i = 0; i < N; i++)
            {
                queries[i] = Console.ReadLine();
            }

            var answers = GetAnswers(mapData, queries);
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine(answers[i]);
            }
        }
    }
}
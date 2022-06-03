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
        public class QItem
        {
            public int Height;
            public int Row;
            public int Col;
            public QItem(int row, int col, int height)
            {
                Height = height;
                Col = col;
                Row = row;
            }
        }

        // custom priority queue since .NET doesn't have such data structure before version 6.0
        public class PriorityQ
        {
            private readonly List<QItem> queue = new List<QItem>();

            private void SiftUp(int index)
            {
                var nextI = index;
                do
                {
                    index = nextI;
                    var p = (index - 1) / 2;
                    if (p >= 0 && (queue[index].Height <= queue[p].Height))
                    {
                        nextI = p;
                        (queue[index], queue[nextI]) = (queue[nextI], queue[index]);
                    }
                } while (nextI != index);
            }

            private void SiftDown(int index)
            {
                var nextI = index;
                do
                {
                    index = nextI;
                    var c1 = 2 * index + 1;
                    var c2 = 2 * index + 2;

                    if ((c1 < queue.Count && queue[index].Height > queue[c1].Height) || (c2 < queue.Count && queue[index].Height > queue[c2].Height))
                    {
                        if (c2 >= queue.Count)
                            nextI = c1;
                        else
                            nextI = queue[c1].Height <= queue[c2].Height ? c1 : c2;
                        if (index != nextI)
                            (queue[nextI], queue[index]) = (queue[index], queue[nextI]);
                    }
                } while (nextI != index);
            }

            public int Count
            {
                get
                {
                    return queue.Count;
                }
            }

            public void Enqueue(QItem item)
            {
                queue.Add(item);
                SiftUp(queue.Count - 1);
            }

            public QItem Dequeue()
            {
                var result = queue[0];
                queue[0] = queue[queue.Count - 1];
                queue.RemoveAt(queue.Count - 1);
                SiftDown(0);
                return result;
            }
        }

        private static bool BorderReachable(int[,] pool, int row, int col)
        {
            var n = (int)Math.Sqrt(pool.Length);
            var cells = new bool[n, n];

            // breadth-first search if there's border cell which height is at most height of current cell
            var cellStack = new Queue<(int, int)>();
            cellStack.Enqueue((row, col));
            while (cellStack.Count > 0)
            {
                var cell = cellStack.Dequeue();
                cells[cell.Item1, cell.Item2] = true;

                // if it is a border cell, return true;
                if (cell.Item1 == 0 || cell.Item2 == 0 || cell.Item1 == n - 1 || cell.Item2 == n - 1)
                    return true;

                // add non-diagonal neighbors to queue
                var d = new int[4] { -1, 0, 1, 0 };
                for (int i = 0; i < d.Length; i++)
                {
                    // enqueue neighbors with height at most height of current cell
                    if (!cells[cell.Item1 + d[i], cell.Item2 + d[3 - i]] && pool[cell.Item1 + d[i], cell.Item2 + d[3 - i]] <= pool[cell.Item1, cell.Item2])
                        cellStack.Enqueue((cell.Item1 + d[i], cell.Item2 + d[3 - i]));
                }
            }
            return false;
        }

        public static int Solve(string[] data)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // initialize priority queue with cell heights
            var n = data.Length;
            var pool = new int[n, n];
            var cellQueue = new PriorityQ();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    pool[i, j] = letters.IndexOf(data[i][j]);
                    if (!(i == 0 || i == n - 1 || j == 0 || j == n - 1))
                        cellQueue.Enqueue(new QItem(i, j, pool[i, j]));
                }
            }

            // at each step add 1 to a cell with lowest height if there's no border reachable
            var volume = 0;
            while (cellQueue.Count > 0)
            {
                var lowestCell = cellQueue.Dequeue();
                if (!BorderReachable(pool, lowestCell.Row, lowestCell.Col))
                {
                    volume++;
                    pool[lowestCell.Row, lowestCell.Col]++;
                    lowestCell.Height++;
                    cellQueue.Enqueue(lowestCell);
                }
            }
            return volume;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var data = new string[N];
            for (int i = 0; i < N; i++)
                data[i] = Console.ReadLine();
            Console.WriteLine(Solve(data));
        }
    }
}
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public class Edge
    {
        public (int R, int C) Target;
        public double Weight;
        public Edge((int, int) target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }

    public class Vertex
    {
        public string Value;
        public int Row;
        public int Col;
        public List<Edge> Edges;
        public Vertex(int row, int col, string value)
        {
            Row = row;
            Col = col;
            Value = value;
            Edges = new List<Edge>();
        }
    }

    public class QItem
    {
        public double Value;
        public (int R, int C) Position;
        public QItem((int, int) pos, double value)
        {
            Value = value;
            Position = pos;
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
                if (p >= 0 && (queue[index].Value <= queue[p].Value))
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

                if ((c1 < queue.Count && queue[index].Value > queue[c1].Value) || (c2 < queue.Count && queue[index].Value > queue[c2].Value))
                {
                    if (c2 >= queue.Count)
                        nextI = c1;
                    else
                        nextI = queue[c1].Value <= queue[c2].Value ? c1 : c2;
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

    public class Player
    {
        private readonly Vertex[][] map;
        private (int, int)? target;
        private (int, int)? start;
        private readonly int stepsBeforeAlarm;
        private bool greedyStrategy;
        private bool targetReached;
        private (int R, int C)? currentExploreTarget;

        public Player(int r, int c, int a)
        {
            stepsBeforeAlarm = a;
            map = new Vertex[r][];

            for (int i = 0; i < r; i++)
            {
                map[i] = new Vertex[c];
                for (int j = 0; j < c; j++)
                {
                    map[i][j] = new Vertex(i, j, "?");
                    for (int dr = -1; dr <= 1; dr++)
                    {
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            if (dr == dc || dr + dc == 0) continue;
                            if (i + dr < 0 || i + dr >= r || j + dc < 0 || j + dc >= c) continue;
                            map[i][j].Edges.Add(new Edge((i + dr, j + dc), 1));
                        }
                    }
                }
            }
        }

        public void UpdateMap(string[] data)
        {
            for (int r = 0; r < data.Length; r++)
            {
                for (int c = 0; c < data[r].Length; c++)
                {
                    if (data[r][c] == '?') continue;
                    map[r][c].Value = data[r][c].ToString();

                    if (map[r][c].Value == "T" && start == null)
                        start = (r, c);
                    if (map[r][c].Value == "C" && target == null)
                        target = (r, c);

                    if (map[r][c].Value == "#")
                    {
                        map[r][c].Edges.Clear();
                    }
                    for (int dr = -1; dr <= 1; dr++)
                    {
                        for (int dc = -1; dc <= 1; dc++)
                        {
                            if (dr == dc || dr + dc == 0) continue;
                            if (r + dr < 0 || r + dr >= data.Length || c + dc < 0 || c + dc >= data[r].Length) continue;
                            if (map[r][c].Value == "#")
                            {
                                map[r + dr][c + dc].Edges.RemoveAll(e => e.Target == (r, c));
                            }
                            if (map[r + dr][c + dc].Value == "#")
                            {
                                map[r][c].Edges.RemoveAll(e => e.Target == (r + dr, c + dc));
                            }
                        }
                    }
                }
            }
        }

        private double Distance((int R, int C) s, (int R, int C) t)
        {
            return Math.Sqrt(Math.Pow(t.R - s.R, 2) + Math.Pow(t.C - s.C, 2));
        }

        private void SetPotentialWeights((int R, int C) target)
        {
            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[r].Length; c++)
                {
                    for (int i = 0; i < map[r][c]?.Edges.Count; i++)
                    {
                        map[r][c].Edges[i].Weight = 1 - Distance((r, c), target) + Distance(map[r][c].Edges[i].Target, target);
                    }
                }
            }
        }

        private void ResetWeights()
        {
            for (int r = 0; r < map.Length; r++)
            {
                for (int c = 0; c < map[r].Length; c++)
                {
                    for (int i = 0; i < map[r][c]?.Edges.Count; i++)
                    {
                        map[r][c].Edges[i].Weight = 1;
                    }
                }
            }
        }

        public (bool IsRelaible, string[] Steps) FindPath((int R, int C) from, (int R, int C) to, bool onlyReliable = false)
        {
            //A-star shortest path search
            //init search
            SetPotentialWeights(to);
            var pqueue = new PriorityQ();
            var dist = new double[map.Length][];
            var prev = new (int R, int C)?[map.Length][];
            for (int r = 0; r < map.Length; r++)
            {
                dist[r] = new double[map[r].Length];
                prev[r] = new (int R, int C)?[map[r].Length];
                for (int c = 0; c < dist[r].Length; c++)
                {
                    dist[r][c] = (r == from.R && c == from.C ? 0 : double.MaxValue);
                    prev[r][c] = null;
                    pqueue.Enqueue(new QItem((r, c), dist[r][c]));
                }
            }

            //do search
            while (pqueue.Count > 0)
            {
                var u = pqueue.Dequeue();
                var m = map[u.Position.R][u.Position.C];
                if (u.Position == to)
                {
                    continue;
                }
                foreach (var e in m.Edges)
                {
                    // if we need a reliable path exclude unexplored cells
                    if (onlyReliable && m.Value == "?") continue;
                    if (dist[e.Target.R][e.Target.C] > dist[m.Row][m.Col] + e.Weight)
                    {
                        dist[e.Target.R][e.Target.C] = dist[m.Row][m.Col] + e.Weight;
                        prev[e.Target.R][e.Target.C] = (m.Row, m.Col);
                        pqueue.Enqueue(new QItem((e.Target.R, e.Target.C), dist[e.Target.R][e.Target.C]));
                    }
                }
            }
            ResetWeights();

            // reconstruct the path after search
            var path = new List<string>();
            var isRelaible = true;
            (int R, int C)? step = to;
            do
            {
                if (step == null) continue;
                var prevStep = prev[step?.R ?? default][step?.C ?? default];

                if (prevStep != null)
                {
                    if (map[prevStep?.R ?? default][prevStep?.C ?? default].Value == "?") isRelaible = false;
                    if (prevStep?.R < step?.R) path.Add("DOWN");
                    else if (prevStep?.R > step?.R) path.Add("UP");
                    else if (prevStep?.C < step?.C) path.Add("RIGHT");
                    else path.Add("LEFT");
                }
                step = prevStep;
            } while (step != null);
            path.Reverse();
            return (isRelaible, path.ToArray());
        }

        private void DisplayMap()
        {
            for (int i = 0; i < map.Length; i++)
            {
                var line = "";
                for (int j = 0; j < map[0].Length; j++)
                {
                    line += map[i][j].Value;
                }
                Console.Error.WriteLine(line);
            }
        }

        public string MakeMove((int R, int C) rick, string[] data)
        {
            if (start == null) start = (rick.R, rick.C);
            UpdateMap(data);

            if (currentExploreTarget == (rick.R, rick.C))
            {
                currentExploreTarget = null;
                // if everything is explored, switch to greedy strategy
                if (map.All(r => r.All(c => c.Value != "?")))
                    greedyStrategy = true;
            }

            // DisplayMap();

            if (rick == target)
                targetReached = true;

            // try to switch to greedy strategy if possible
            if (target != null && !greedyStrategy)
            {
                var path2Target = FindPath(rick, ((int, int))target, true);
                var path2Start = FindPath(((int, int))target, ((int, int))start, true);
                if (path2Target.IsRelaible && path2Start.IsRelaible && path2Target.Steps.Length > 0 &&
                    path2Start.Steps.Length > 0 && path2Start.Steps.Length <= stepsBeforeAlarm)
                {
                    greedyStrategy = true;
                }
            }

            if (greedyStrategy)
            {
                // to the target & back
                if (!targetReached)
                    return FindPath(rick, ((int, int))target, true).Steps[0];
                else
                    return FindPath(rick, ((int, int))start, true).Steps[0];
            }
            else
            {
                // explore, unreliable paths are possible
                var path2ExploreTarget = currentExploreTarget != null ?
                    FindPath(rick, ((int, int))currentExploreTarget).Steps :
                    new string[0];
                if (currentExploreTarget == null || map[currentExploreTarget?.R ?? default][currentExploreTarget?.C ?? default].Value != "?")
                {
                    // take the closest unexplored point that is actually reachable
                    var unexplored = map.SelectMany(r => r.Where(c => c.Value == "?").Select(c => (c.Row, c.Col)))
                        .Select(x => (D: Distance(rick, x), P: x)).ToArray();

                    Array.Sort(unexplored, (u1, u2) => u1.D.CompareTo(u2.D));

                    for (int i = 0; i < unexplored.Length; i++)
                    {
                        currentExploreTarget = unexplored[i].P;
                        Console.Error.WriteLine($"set exp target to ({currentExploreTarget?.R},{currentExploreTarget?.C})");
                        path2ExploreTarget = FindPath(rick, ((int, int))currentExploreTarget).Steps;
                        if (path2ExploreTarget.Length > 0) break; // if path is possible, continue with chosen exploration point
                    }
                }
                return path2ExploreTarget[0];
            }
        }
    }

    public static class Solver
    {
        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            int R = int.Parse(inputs[0]); // number of rows.
            int C = int.Parse(inputs[1]); // number of columns.
            int A = int.Parse(inputs[2]); // number of rounds between the time the alarm countdown is activated and the time the alarm goes off.

            var player = new Player(R, C, A);
            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int KR = int.Parse(inputs[0]); // row where Rick is located.
                int KC = int.Parse(inputs[1]); // column where Rick is located.


                var scannerData = new string[R];
                for (int i = 0; i < R; i++)
                {
                    scannerData[i] = Console.ReadLine(); // C of the characters in '#.TC?' (i.e. one line of the ASCII maze).
                }

                var move = player.MakeMove((KR, KC), scannerData);
                Console.WriteLine(move); // Rick's next move (UP DOWN LEFT or RIGHT).
            }
        }
    }
}

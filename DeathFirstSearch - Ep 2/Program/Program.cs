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
        public static int[][] BuildGraph(int n, (int n1, int n2)[] links)
        {
            var graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach (var (n1, n2) in links)
            {
                graph[n1].Add(n2);
                graph[n2].Add(n1);
            }
            return graph.Select(x => x.ToArray()).ToArray();
        }

        public static (int, bool)[] FindPath(int target, int start, int[][] graph, int[] exits, int exitidx)
        {
            // find path between given target and start postion, bfs
            var path = new List<(int node, bool ex)>();
            var dist = new int[graph.Length];
            var prev = new int[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                dist[i] = int.MaxValue;
                prev[i] = -1;
            }
            dist[target] = 0;
            var queue = new Queue<int>();
            queue.Enqueue(target);
            var minDist = int.MaxValue;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == start)
                {
                    if (dist[node] < minDist)
                    {
                        minDist = dist[node];
                        var pn = node;
                        path.Clear();
                        do
                        {
                            path.Add((pn, graph[pn].Intersect(exits).Any()));
                            pn = prev[pn];
                        } while (pn >= 0);
                    }
                    continue;
                }
                foreach (var link in graph[node])
                {
                    if (link < 0) continue;
                    if (exits.Contains(link)) continue;
                    if (dist[link] > dist[node] + 1)
                    {
                        dist[link] = dist[node] + 1;
                        prev[link] = node;
                        queue.Enqueue(link);
                    }
                }
            }
            if (path.Count > 0 && path[^1].node != exits[exitidx])
            {
                path.Add((exits[exitidx], false));
            }
            return path.ToArray();
        }

        public static (int link, int) MakeMove(int[][] graph, int[] exits, int agent)
        {
            var paths = new List<(int node, bool ex)[]>();
            for (int e = 0; e < exits.Length; e++) // for each exit
            {
                for (int i = 0; i < graph[exits[e]].Length; i++) // for each link to exit
                {
                    if (graph[exits[e]][i] < 0) continue;
                    // find path from agent to this particular exit through this particular link
                    var path = FindPath(graph[exits[e]][i], agent, graph, exits, e);
                    paths.Add(path);
                }
            }
            // group all path found by second-to-last node (just before the exit)
            var groups = paths.GroupBy(p => p[^2].node).ToArray();

            // consider links next to agent (length of path == 2) first, if any
            (int node, bool ex)[][] links = groups
                .SelectMany(g => g.ToArray())
                .Where(p => p.Length == 2 && p[0].node == agent)
                .ToArray();
            if (links.Length == 0)
            {
                //else - consider 'forks' (more than one exit from the node)
                links = groups.Where(g => g.Count() > 1)
                    .SelectMany(g => g.ToArray())
                    .ToArray();
            }
            if (links.Length == 0)
            {
                //else consider all links
                links = groups.SelectMany(g => g.ToArray()).ToArray();
            }

            // choose closest link to agent to sever
            var links2Sever = links.Select(p =>
                    {
                        var a = p.ToArray();
                        return (a[^2].node, exit: a[^1].node, dist: a.Count(x => !x.ex));
                    }).OrderBy(a => a.dist).ToArray();
            return (links2Sever[0].node, links2Sever[0].exit);
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            var links = new (int, int)[L];
            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                links[i] = (N1, N2);
            }

            var graph = BuildGraph(N, links);
            var exits = new int[E];
            for (int i = 0; i < E; i++)
            {
                exits[i] = int.Parse(Console.ReadLine());
                // the index of a gateway node
            }

            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Bobnet agent is positioned this turn
                var move = MakeMove(graph, exits, SI);
                Console.WriteLine($"{move.link} {move.Item2}");
                //sever link
                for (int i = 0; i < graph[move.link].Length; i++)
                    if (graph[move.link][i] == move.Item2) graph[move.link][i] = -1;
                for (int i = 0; i < graph[move.Item2].Length; i++)
                    if (graph[move.Item2][i] == move.link) graph[move.Item2][i] = -1;
            }
        }
    }
}
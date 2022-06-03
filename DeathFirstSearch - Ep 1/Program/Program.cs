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
    public static class Player
    {
        private static int[][] BuildGraph(int n, (int, int)[] links)
        {
            var graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                graph[i] = new List<int>();
            }
            foreach (var link in links)
            {
                graph[link.Item1].Add(link.Item2);
                graph[link.Item2].Add(link.Item1);
            }
            return graph.Select(x => x.ToArray()).ToArray();
        }

        private static (int, int) BFS(int exit, int agent, int[][] graph)
        {
            // find shortest path between given exit and agent postion
            var dist = new int[graph.Length];
            var prev = new int[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                dist[i] = int.MaxValue;
            }
            dist[exit] = 0;
            var queue = new Queue<int>();
            queue.Enqueue(exit);
            var minDist = int.MaxValue;
            var minLink = -1;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == agent)
                {
                    if (dist[node] < minDist)
                    {
                        minDist = dist[node];
                        var pn = node;
                        while (prev[pn] != exit) pn = prev[pn];
                        minLink = pn;
                    }
                    continue;
                }
                foreach (var link in graph[node])
                {
                    if (link < 0) continue;
                    if (dist[link] > dist[node] + 1)
                    {
                        dist[link] = dist[node] + 1;
                        prev[link] = node;
                        queue.Enqueue(link);
                    }
                }
            }
            return (minDist, minLink);
        }

        public static (int, int) MakeMove(int[][] graph, int[] exits, int agent)
        {
            // among all exits find link which is a leg of shortest way to agent
            var minDist = int.MaxValue;
            var linkToSever = (-1, -1);
            for (int e = 0; e < exits.Length; e++)
            {
                var bfsRes = BFS(exits[e], agent, graph);
                if (bfsRes.Item1 < minDist)
                {
                    minDist = bfsRes.Item1;
                    linkToSever = (bfsRes.Item2, exits[e]);
                }
            }
            return linkToSever;
        }

        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
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
                Console.WriteLine($"{move.Item1} {move.Item2}");
                //sever link
                for (int i = 0; i < graph[move.Item1].Length; i++)
                    if (graph[move.Item1][i] == move.Item2) graph[move.Item1][i] = -1;
                for (int i = 0; i < graph[move.Item2].Length; i++)
                    if (graph[move.Item2][i] == move.Item1) graph[move.Item2][i] = -1;
            }
        }
    }
}
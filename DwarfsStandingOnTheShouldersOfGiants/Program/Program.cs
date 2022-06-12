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
        public static int GetTreeHeight(string[] data)
        {
            var edges = new (int x, int y)[data.Length];
            var maxNode = -1;
            for (int i = 0; i < data.Length; i++)
            {
                string[] inputs = data[i].Split(' ');
                edges[i] = (int.Parse(inputs[0]), int.Parse(inputs[1]));
                var node = Math.Max(edges[i].x, edges[i].y);
                if (node > maxNode)
                {
                    maxNode = node;
                }
            }

            var graph = new (List<int> influencedBy, List<int> influences)[maxNode + 1];
            for (int i = 0; i < edges.Length; i++)
            {
                (graph[edges[i].x].influences ??= new List<int>()).Add(edges[i].y);
                (graph[edges[i].y].influencedBy ??= new List<int>()).Add(edges[i].x);
            }

            var maxDepth = 0;
            for (int i = 0; i < graph.Length; i++)
            {
                if (graph[i].influencedBy != null || graph[i].influences == null) continue;
                var stack = new Stack<(int i, int depth)>();
                stack.Push((i, 0));
                while (stack.Count > 0)
                {
                    var node = stack.Pop();
                    if (graph[node.i].influences == null)
                    {
                        if (node.depth > maxDepth) maxDepth = node.depth;
                    }
                    else
                    {
                        foreach (var inf in graph[node.i].influences)
                        {
                            stack.Push((inf, node.depth + 1));
                        }
                    }
                }
            }
            return maxDepth + 1;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); // the number of relationships of influence
            var data = new string[n];
            for (int i = 0; i < n; i++)
            {
                data[i] = Console.ReadLine(); // a relationship of influence between two people (id1, id2);
            }

            // The number of people involved in the longest succession of influences
            Console.WriteLine(GetTreeHeight(data));
        }
    }
}
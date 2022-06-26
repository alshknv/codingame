using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public class Link
    {
        public Node Source;
        public Node Target;
        public int Value;
        public int OriginalValue;
    }

    public class Node
    {
        public (int x, int y) Position;
        public int Value;
        public List<Link> Links = new List<Link>();
    }

    public static class Player
    {
        private static void AddLink(this List<Link> links, Node source, Node target)
        {
            var value = Math.Min(2, Math.Min(target.Value, source.Value));
            var link = new Link()
            {
                Source = source,
                Target = target,
                Value = value,
                OriginalValue = value
            };
            if (links.Any(l => l.Target == target && l.Source == source) ||
                links.Any(l => l.Target == source && l.Source == target))
            {
                return;
            }
            link.Source.Links.Add(link);
            link.Target.Links.Add(link);
            links.Add(link);
        }

        private static (Link[] links, Node[] nodes) Init(string[] data)
        {
            var map = new int?[data.Length][];
            var links = new List<Link>();
            var nodes = new List<Node>();
            for (int y = 0; y < data.Length; y++)
            {
                map[y] = new int?[data[0].Length];
                for (int x = 0; x < data[y].Length; x++)
                {
                    if (char.IsDigit(data[y][x]))
                    {
                        var node = new Node()
                        {
                            Position = (x, y),
                            Value = int.Parse(data[y][x].ToString())
                        };
                        nodes.Add(node);
                        map[y][x] = nodes.Count - 1;
                    }
                }
            }

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] != null)
                    {
                        for (int i = x - 1; i >= 0; i--)
                            if (map[y][i] != null) { links.AddLink(nodes[(int)map[y][x]], nodes[(int)map[y][i]]); break; }
                        for (int i = x + 1; i < data[0].Length; i++)
                            if (map[y][i] != null) { links.AddLink(nodes[(int)map[y][x]], nodes[(int)map[y][i]]); break; }
                        for (int j = y - 1; j >= 0; j--)
                            if (map[j][x] != null) { links.AddLink(nodes[(int)map[y][x]], nodes[(int)map[j][x]]); break; }
                        for (int j = y + 1; j < data.Length; j++)
                            if (map[j][x] != null) { links.AddLink(nodes[(int)map[y][x]], nodes[(int)map[j][x]]); break; }
                    }
                }
            }
            return (links.ToArray(), nodes.ToArray());
        }

        private static int Orientation(Link l, (int x, int y) r)
        {
            int val = ((l.Target.Position.y - l.Source.Position.y) * (r.x - l.Target.Position.x)) -
                      ((l.Target.Position.x - l.Source.Position.x) * (r.y - l.Target.Position.y));

            if (val == 0) return 0;
            return (val > 0) ? 1 : 2;
        }

        private static bool ShareSourceOrTarget(Link l1, Link l2)
        {
            return l1.Source.Position == l2.Source.Position || l1.Source.Position == l2.Source.Position
                || l1.Target.Position == l2.Source.Position || l1.Target.Position == l2.Target.Position;
        }

        private static bool DoIntersect(Link l1, Link l2)
        {
            int o1 = Orientation(l1, l2.Source.Position);
            int o2 = Orientation(l1, l2.Target.Position);
            int o3 = Orientation(l2, l1.Source.Position);
            int o4 = Orientation(l2, l1.Target.Position);

            // if links do intersect, but not if they share source or target
            return o1 != o2 && o3 != o4 && (!ShareSourceOrTarget(l1, l2))
            && (!ShareSourceOrTarget(l1, l2)) && (!ShareSourceOrTarget(l2, l1))
            && (!ShareSourceOrTarget(l2, l1));
        }

        private static bool RecursiveConnect(Link[] links, Node[] nodes, int idx)
        {
            if (nodes.All(n => n.Value == 0)) return true;
            if (idx >= links.Length) return false;
            if (nodes.Any(n => n.Links.Sum(l => l.Value) < n.Value)) return false;
            var presentLinks = links.Where(l => l.Value < l.OriginalValue);
            if (presentLinks.Any(l => presentLinks.Any(l2 => DoIntersect(l, l2)))) return false;

            for (var d = links[idx].Value; d >= 0; d--)
            {
                if (links[idx].Value < d || links[idx].Source.Value < d || links[idx].Target.Value < d)
                    continue;
                links[idx].Value -= d;
                links[idx].Source.Value -= d;
                links[idx].Target.Value -= d;
                if (RecursiveConnect(links, nodes, idx + 1)) return true;
                links[idx].Value += d;
                links[idx].Source.Value += d;
                links[idx].Target.Value += d;
            }
            return false;
        }

        public static string[] ConnectAll(string[] data)
        {
            var (links, nodes) = Init(data);
            var connections = new List<string>();
            Node[] completeNodes;
            do
            {
                completeNodes = nodes.Where(n => n.Value > 0 && n.Links.Sum(l => l.Value) == n.Value).ToArray();
                foreach (var n in completeNodes)
                {
                    foreach (var l in n.Links)
                    {
                        if (l.Value == 0) continue;
                        l.Source.Value -= l.Value;
                        l.Target.Value -= l.Value;
                        l.Value = 0;
                    }
                }
            } while (completeNodes.Length > 0);

            if (RecursiveConnect(links, nodes, 0))
            {
                return links
                .Select((l) => l.OriginalValue == l.Value ? "" : $"{l.Source.Position.x} {l.Source.Position.y} {l.Target.Position.x} {l.Target.Position.y} {l.OriginalValue - l.Value}")
                .Where(l => !string.IsNullOrEmpty(l))
                .ToArray();
            }
            return new string[0];

        }

        static void Main(string[] args)
        {
            Console.ReadLine();
            int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
            var data = new string[height];
            for (int i = 0; i < height; i++)
            {
                data[i] = Console.ReadLine(); // width characters, each either a number or a '.'
            }

            foreach (var line in data)
                Console.Error.WriteLine(line);

            // Two coordinates and one integer: a node, one of its neighbors, the number of links connecting them.
            var connections = ConnectAll(data);
            for (int i = 0; i < connections.Length; i++)
            {
                Console.WriteLine(connections[i]);
            }
        }
    }
}
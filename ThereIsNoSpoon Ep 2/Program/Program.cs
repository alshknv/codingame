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
        public int OriginalValue;
        public int VisitMask { get; set; }
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
                        var value = int.Parse(data[y][x].ToString());
                        var node = new Node()
                        {
                            Position = (x, y),
                            Value = value,
                            OriginalValue = value
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
            return l1.Source.Position == l2.Source.Position || l1.Source.Position == l2.Target.Position
                || l1.Target.Position == l2.Source.Position || l1.Target.Position == l2.Target.Position;
        }

        private static bool DoIntersect(Link l1, Link l2)
        {
            int o1 = Orientation(l1, l2.Source.Position);
            int o2 = Orientation(l1, l2.Target.Position);
            int o3 = Orientation(l2, l1.Source.Position);
            int o4 = Orientation(l2, l1.Target.Position);

            // if links do intersect, but not if they share source or target
            return o1 != o2 && o3 != o4 && !ShareSourceOrTarget(l1, l2);
        }

        private static bool LinksIntersect(Link[] links)
        {
            var presentLinks = links.Where(l => l.Value < l.OriginalValue);
            return presentLinks.Any(l => presentLinks.Any(l2 => DoIntersect(l, l2)));
        }

        private static List<string> RemoveForcedConnections(Node[] nodes)
        {
            var connections = new List<string>();
            Node[] fcNodes = null;
            do
            {
                fcNodes = nodes.Where(n => n.Value > 0 && n.Links.Sum(l => l.Value) == n.Value).ToArray();
                for (int n = 0; n < fcNodes.Length; n++)
                {
                    if (fcNodes[n].Value == 0) continue;
                    foreach (var l in fcNodes[n].Links)
                    {
                        if (l.Value > 0)
                        {
                            connections.Add($"{l.Source.Position.x} {l.Source.Position.y} {l.Target.Position.x} {l.Target.Position.y} {l.OriginalValue}");
                            //l.Source.OriginalValue -= l.OriginalValue;
                            l.Source.Value -= l.OriginalValue;
                            //l.Target.OriginalValue -= l.OriginalValue;
                            l.Target.Value -= l.OriginalValue;
                            l.Value = l.OriginalValue;
                            //l.Value = 0;
                            //n.Value -= l.OriginalValue;
                            //l.Value = 0;
                        }
                    }
                    //fcNodes[n].Links = fcNodes[n].Links.Where(l => l.OriginalValue > 0).ToList();
                }
            }
            while (fcNodes.Length > 0);
            return connections;
        }

        private static bool RecursiveConnect(Node[] nodes, Link[] links, Node[] connectedNodes)
        {
            if (connectedNodes.Length == nodes.Length && nodes.All(n => n.Value == 0))
                return true;
            if (nodes.Any(n => n.Links.Sum(l => l.OriginalValue - l.Value) > n.OriginalValue)) return false;
            if (LinksIntersect(links)) return false;
            var outgoingLinks = connectedNodes.SelectMany(n => n.Links.Where(l => l.Value > 0)).ToArray();

            for (var l = 0; l < outgoingLinks.Length; l++)
            {
                for (var d = outgoingLinks[l].Value; d > 0; d--)
                {
                    if (outgoingLinks[l].Source.Value < d || outgoingLinks[l].Target.Value < d)
                        continue;
                    outgoingLinks[l].Value -= d;
                    outgoingLinks[l].Source.Value -= d;
                    outgoingLinks[l].Target.Value -= d;
                    var cNodes = new List<Node>(connectedNodes)
                    {
                        connectedNodes.Contains(outgoingLinks[l].Source) ? outgoingLinks[l].Target : outgoingLinks[l].Source
                    };
                    if (RecursiveConnect(nodes, links, cNodes.ToArray())) return true;
                    outgoingLinks[l].Value += d;
                    outgoingLinks[l].Source.Value += d;
                    outgoingLinks[l].Target.Value += d;
                }
            }
            return false;
        }

        public static string[] ConnectAll(string[] data)
        {
            var (links, nodes) = Init(data);
            var connections = RemoveForcedConnections(nodes);
            //nodes = nodes.Where(n => n.OriginalValue > 0).ToArray();
            //links = links.Where(l => l.OriginalValue > 0).ToArray();
            /*foreach (var node in nodes)
            {
                node.Links = node.Links.Where(l => l.OriginalValue > 0).ToList();
                foreach (var link in node.Links)
                {
                    var linkValue = Math.Min(2, Math.Min(link.Target.Value, link.Source.Value));
                    link.Value = linkValue;
                    link.OriginalValue = linkValue;
                }
            }*/
            if (RecursiveConnect(nodes, links, nodes.Take(1).ToArray()))
            {
                connections.AddRange(
                links
                .Select((l) => l.OriginalValue == l.Value ? "" : $"{l.Source.Position.x} {l.Source.Position.y} {l.Target.Position.x} {l.Target.Position.y} {l.OriginalValue - l.Value}")
                .Where(l => !string.IsNullOrEmpty(l))
                );
            }
            return connections.ToArray();
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
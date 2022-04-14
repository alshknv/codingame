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
        private static (Dictionary<string, (int low, int high)> split, int move) Split((int low, int high) section, int pos)
        {
            var split = new Dictionary<string, (int low, int high)>();
            var len = section.high - section.low + 1;
            if (len == 2)
            {
                split.Add(section.low == pos ? "COLDER" : "WARMER", (section.low, section.low));
                split.Add(section.low == pos ? "WARMER" : "COLDER", (section.high, section.high));
                return (split, section.low == pos ? section.high : section.low);
            }
            var mid = section.low + (len / 2);
            if (mid + (mid - pos) < section.low) mid++;
            if (mid + (mid - pos) > section.high) mid--;
            split.Add(pos <= mid ? "COLDER" : "WARMER", (section.low, mid == pos ? mid : mid - 1));
            if (mid != pos)
                split.Add("SAME", (mid, mid));
            split.Add(pos <= mid ? "WARMER" : "COLDER", (mid + 1, section.high));
            if (pos == mid) return (split, mid + 1);
            return (split, mid + (mid - pos));
        }

        private static bool GetRangeIndex((int low, int high)[] ranges, int?[] target, int[] pos, out int rangeIndex)
        {
            for (int i = 0; i < ranges.Length; i++)
            {
                if (target[i].HasValue || (ranges[i].low == ranges[i].high) || pos[i] < ranges[i].low || pos[i] > ranges[i].high) continue;
                rangeIndex = i;
                return true;
            }
            rangeIndex = target[1].HasValue ? 0 : 1;
            return false;
        }

        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]); // width of the building.
            int H = int.Parse(inputs[1]); // height of the building.
            int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
            inputs = Console.ReadLine().Split(' ');
            int X0 = int.Parse(inputs[0]);
            int Y0 = int.Parse(inputs[1]);

            Console.ReadLine();

            var ranges = new (int low, int high)[2] {
                (0, W-1), (0, H-1)
            };
            var target = new int?[] { null, null };
            var pos = new int[] { X0, Y0 };

            while (true)
            {
                Console.Error.WriteLine("Turn begin");
                if (target[0].HasValue && target[1].HasValue)
                {
                    Console.Error.WriteLine($"Target found jump directly to the point {target[0].Value}, {target[1].Value}");
                    Console.WriteLine($"{target[0].Value} {target[1].Value}");
                    break;
                }
                if (GetRangeIndex(ranges, target, pos, out int i))
                {
                    Console.Error.WriteLine($"Range index is {i}");
                    var (split, move) = Split(ranges[i], pos[i]);
                    pos[i] = move;
                    Console.WriteLine($"{pos[0]} {pos[1]}");
                    var dir = Console.ReadLine();
                    ranges[i] = split[dir];
                    Console.Error.WriteLine($"Range {i} changed to: {ranges[i]}");
                    if (ranges[i].low == ranges[i].high)
                    {
                        target[i] = ranges[i].low;
                        Console.Error.WriteLine($"Target {i} is: {target[i]}");
                    }
                }
                else
                {
                    pos[0] = Math.Abs(pos[0] - ranges[0].high) < Math.Abs(pos[0] - ranges[0].low) ? ranges[0].high : ranges[0].low;
                    pos[1] = Math.Abs(pos[1] - ranges[1].high) < Math.Abs(pos[1] - ranges[1].low) ? ranges[1].high : ranges[1].low;
                    Console.Error.WriteLine($"Outside both ranges go to {pos[0]}, {pos[1]}");
                    Console.WriteLine($"{pos[0]} {pos[1]}");
                    Console.ReadLine();
                }
                Console.Error.WriteLine("Turn end");
                Console.Error.WriteLine();
            }
        }
    }
}
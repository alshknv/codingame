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
        private static int[] EncodeLine(int[] line)
        {
            var result = new List<int>();
            int? current = null;
            var counter = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == current)
                {
                    counter++;
                }
                else
                {
                    if (current.HasValue)
                        result.Insert(result.Count - 1, counter);
                    result.Add(line[i]);
                    current = line[i];
                    counter = 1;
                }
            }
            result.Insert(result.Count - 1, counter);
            return result.ToArray();
        }

        public static int[] ConwaySequence(int x, int line)
        {
            int[] sequence = new int[] { x };
            while (line > 1)
            {
                sequence = EncodeLine(sequence);
                line--;
            }
            return sequence;
        }

        static void Main(string[] args)
        {
            int R = int.Parse(Console.ReadLine());
            int L = int.Parse(Console.ReadLine());

            var sequence = ConwaySequence(R, L);
            Console.WriteLine(string.Join(" ", sequence));
        }
    }
}
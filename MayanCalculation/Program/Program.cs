using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public static class Mayan
    {
        private static (Dictionary<string, int> mayan2int, Dictionary<int, List<string>> int2mayan) GetNumerals(int l, string[] numData)
        {
            var int2mayan = new Dictionary<int, List<string>>();
            for (int i = 0; i < 20; i++)
            {
                int2mayan.Add(i, new List<string>());
            }
            for (int i = 0; i < numData.Length; i++)
            {
                var j = 0;
                while (j < numData[i].Length)
                {
                    int2mayan[j / l].Add(numData[i][j..(j + l)]);
                    j += l;
                }
            }
            var mayan2int = int2mayan.Select(x => KeyValuePair.Create(string.Concat(x.Value), x.Key)).ToDictionary(x => x.Key, x => x.Value);
            return (mayan2int, int2mayan);
        }

        private static long GetBase10Number(int h, string[] mayanNumber, Dictionary<string, int> numerals)
        {
            long base10Number = 0;
            for (int i = 0; i < mayanNumber.Length / h; i++)
            {
                string stringNumeral = "";
                for (int j = 0; j < h; j++)
                    stringNumeral += mayanNumber[(i * h) + j];
                base10Number += numerals[stringNumeral] * (long)Math.Pow(20, (mayanNumber.Length / h) - i - 1);
            }
            return base10Number;
        }

        private static string[] GetMayanNumber(long base10Number, Dictionary<int, List<string>> int2mayan)
        {
            List<int> mayanDigits = new List<int>();
            List<string> mayanNumber = new List<string>();
            if (base10Number == 0)
            {
                mayanNumber.AddRange(int2mayan[0]);
                return mayanNumber.ToArray();
            }
            while (base10Number > 0)
            {
                mayanDigits.Add((int)(base10Number % 20));
                base10Number /= 20;
            }
            for (int i = mayanDigits.Count - 1; i >= 0; i--)
            {
                mayanNumber.AddRange(int2mayan[mayanDigits[i]]);
            }
            return mayanNumber.ToArray();
        }

        public static string[] Calculate(int l, int h, string[] numData, string[] num1, string[] num2, string operation)
        {
            var (mayan2int, int2mayan) = GetNumerals(l, numData);
            var n1 = GetBase10Number(h, num1, mayan2int);
            var n2 = GetBase10Number(h, num2, mayan2int);
            long result = operation switch
            {
                "+" => n1 + n2,
                "-" => n1 - n2,
                "/" => n1 / n2,
                "*" => n1 * n2,
                _ => throw new Exception("Unknown operation")
            };
            return GetMayanNumber(result, int2mayan);
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int H = int.Parse(inputs[1]);
            var numData = new string[H];
            for (int i = 0; i < H; i++)
            {
                numData[i] = Console.ReadLine();
            }
            int S1 = int.Parse(Console.ReadLine());
            var num1 = new string[S1];
            for (int i = 0; i < S1; i++)
            {
                num1[i] = Console.ReadLine();
            }
            int S2 = int.Parse(Console.ReadLine());
            var num2 = new string[S2];
            for (int i = 0; i < S2; i++)
            {
                num2[i] = Console.ReadLine();
            }
            string operation = Console.ReadLine();

            var result = Calculate(L, H, numData, num1, num2, operation);
            for (int i = 0; i < result.Length; i++)
                Console.WriteLine(result[i]);
        }
    }
}
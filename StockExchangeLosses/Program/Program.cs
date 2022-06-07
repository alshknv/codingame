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
        public static int GetMaxLoss(int n, string data)
        {
            string[] inputs = data.Split(' ');
            int top = int.MinValue;
            int maxLoss = 0;
            for (int i = 0; i < n; i++)
            {
                int v = int.Parse(inputs[i]);
                if (v > top)
                    top = v;
                if (v < top && top - v > maxLoss)
                    maxLoss = top - v;
            }
            return -maxLoss;
        }

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var loss = GetMaxLoss(n, Console.ReadLine());
            Console.WriteLine(loss);
        }
    }
}
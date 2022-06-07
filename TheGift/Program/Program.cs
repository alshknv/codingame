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
        public static (bool, int[]) GetGiftContributions(int cost, int[] budgets)
        {
            var contributions = new int[budgets.Length];
            var budgetLeft = true;
            while (budgetLeft && cost > 0)
            {
                budgetLeft = false;
                for (int i = 0; i < budgets.Length; i++)
                {
                    if (budgets[i] > 0)
                    {
                        budgetLeft = true;
                        budgets[i]--;
                        contributions[i]++;
                        cost--;
                        if (cost == 0)
                            break;
                    }
                }
            }
            Array.Sort(contributions, (c1, c2) => c1.CompareTo(c2));
            return (cost == 0, contributions);
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int C = int.Parse(Console.ReadLine());

            var budgets = new int[N];
            for (int i = 0; i < N; i++)
            {
                budgets[i] = int.Parse(Console.ReadLine());
            }

            var (isPossible, contributions) = GetGiftContributions(C, budgets);
            if (!isPossible)
            {
                Console.WriteLine("IMPOSSIBLE");
            }
            else
            {
                for (int i = 0; i < contributions.Length; i++)
                {
                    Console.WriteLine(contributions[i]);
                }
            }
        }
    }
}
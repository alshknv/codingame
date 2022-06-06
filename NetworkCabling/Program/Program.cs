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
        /// <summary>
        /// Simple numeric derivative sign detection. 
        /// Returns 0 if it is a point where derivative changes sign
        /// </summary>
        /// <param name="c"></param>
        /// <param name="y"></param>
        private static int DerivativeSign(this long[] c, double y)
        {
            const double delta = 0.01;
            var sign1 = (c.CalcModularFunc(y + delta) - c.CalcModularFunc(y)) / delta > 0;
            var sign2 = (c.CalcModularFunc(y) - c.CalcModularFunc(y - delta)) / delta > 0;
            if (sign1 != sign2) return 0;
            return sign1 ? 1 : -1;
        }

        /// <summary>
        /// Calculate function |x-c[0]|+|x-c[1]|+...+|x-c[n]|
        /// </summary>
        /// <param name="c"></param>
        /// <param name="x"></param>
        private static double CalcModularFunc(this long[] c, double x)
        {
            double result = 0;
            for (int i = 0; i < c.Length; i++)
            {
                result += Math.Abs(x - c[i]);
            }
            return result;
        }

        public static long GetMinimumCableLength(string[] houseData)
        {
            var N = houseData.Length;

            var minX = long.MaxValue;
            var maxX = long.MinValue;
            var minY = long.MaxValue;
            var maxY = long.MinValue;
            var housesY = new long[N];

            for (int i = 0; i < N; i++)
            {
                string[] inputs = houseData[i].Split(' ');
                long X = long.Parse(inputs[0]);
                long Y = long.Parse(inputs[1]);
                housesY[i] = Y;
                if (X < minX) minX = X;
                if (X > maxX) maxX = X;
                if (Y < minY) minY = Y;
                if (Y > maxY) maxY = Y;
            }

            // binary search for a minimum - a point where derivative changes its sign from -1 to 1
            while (minY < maxY)
            {
                var mid = (minY + maxY) / 2;
                var sign = housesY.DerivativeSign(mid);
                if (sign == 0)
                {
                    minY = mid;
                    maxY = mid;
                    break;
                }
                else if (sign < 0)
                {
                    minY = mid + 1;
                }
                else
                {
                    maxY = mid - 1;
                }
            }

            // minimum is found calculate the answer
            var y = Math.Max(minY, maxY);
            var minCable = maxX - minX;
            for (int i = 0; i < N; i++)
            {
                minCable += Math.Abs(housesY[i] - y);
            }
            return minCable;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var houseData = new string[N];
            for (int i = 0; i < N; i++)
            {
                houseData[i] = Console.ReadLine();
            }
            Console.WriteLine(GetMinimumCableLength(houseData));
        }
    }
}
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
        private static int Mid(int a, int b)
        {
            return (int)Math.Round((double)(a + b) / 2);
        }

        public static (int X0, int Y0, int X1, int Y1) NewRegion((int X0, int Y0, int X1, int Y1) region, (int X, int Y) bat, string signal)
        {
            return signal switch
            {
                "U" => (bat.X, region.Y0, bat.X, bat.Y - 1),
                "UR" => (bat.X + 1, region.Y0, region.X1, bat.Y - 1),
                "R" => (bat.X + 1, bat.Y, region.X1, bat.Y),
                "DR" => (bat.X + 1, bat.Y + 1, region.X1, region.Y1),
                "D" => (bat.X, bat.Y + 1, bat.X, region.Y1),
                "DL" => (region.X0, bat.Y + 1, bat.X - 1, region.Y1),
                "L" => (region.X0, bat.Y, bat.X - 1, bat.Y),
                "UL" => (region.X0, region.Y0, bat.X - 1, bat.Y - 1),
                _ => (-1, -1, -1, -1)
            };
        }

        public static (int X, int Y) RegionCenter((int X0, int Y0, int X1, int Y1) region)
        {
            return (Mid(region.X1, region.X0), Mid(region.Y1, region.Y0));
        }

        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]); // width of the building.
            int H = int.Parse(inputs[1]); // height of the building.
            int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);

            var bat = (X, Y);
            var reg = (0, 0, W - 1, H - 1);
            // game loop
            while (true)
            {
                string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
                reg = NewRegion(reg, bat, bombDir);
                bat = RegionCenter(reg);
                // the location of the next window Batman should jump to.
                Console.WriteLine($"{bat.X} {bat.Y}");
            }
        }
    }
}
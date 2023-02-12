using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame;

public static class Solution
{
    const string Symbols = "123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    static bool In(this char character, params char[] characterList)
    {
        return characterList.Contains(character);
    }

    public static string[] Solve(string[] inputs)
    {
        string[] hw = inputs[0].Split(' ');
        int H = int.Parse(hw[0]);
        int W = int.Parse(hw[1]);
        var field = new char[H][];
        var points = new (int X, int Y)?[Symbols.Length];
        for (int i = 0; i < H; i++)
        {
            field[i] = inputs[i + 1].ToCharArray();
            for (int j = 0; j < W; j++)
            {
                var index = Symbols.IndexOf(field[i][j]);
                if (index >= 0)
                {
                    points[index] = (j, i);
                }
            }
        }

        for (var p = 0; p < points.Length - 1 && points[p + 1].HasValue; p++)
        {
            var dx = points[p + 1].Value.X - points[p].Value.X;
            if (dx > 0) dx = 1;
            if (dx < 0) dx = -1;
            var dy = points[p + 1].Value.Y - points[p].Value.Y;
            if (dy > 0) dy = 1;
            if (dy < 0) dy = -1;

            var lineChar = dy == 0 ? '-' : (dx == 0 ? '|' : (dx * dy < 0 ? '/' : '\\'));
            var y = points[p].Value.Y;
            var x = points[p].Value.X;
            field[y][x] = 'o';
            field[points[p + 1].Value.Y][points[p + 1].Value.X] = 'o';
            do
            {
                if (!field[y][x].In('o', '*') && field[y][x] != lineChar)
                {
                    if ((lineChar == '|' && field[y][x] == '-') || (lineChar == '-' && field[y][x] == '|'))
                    {
                        field[y][x] = '+';
                    }
                    else if ((lineChar == '/' && field[y][x] == '\\') || (lineChar == '\\' && field[y][x] == '/'))
                    {
                        field[y][x] = 'X';
                    }
                    else if ((lineChar == '+' && field[y][x].In('\\', '/')) || (lineChar == 'X' && field[y][x].In('|', '-'))
                        || (lineChar.In('/', '\\') && field[y][x].In('-', '|')) || (lineChar.In('|', '-') && field[y][x].In('\\', '/')))
                    {
                        field[y][x] = '*';
                    }
                    else
                    {
                        field[y][x] = lineChar;
                    }
                }
                x += dx;
                y += dy;
            } while (x != points[p + 1].Value.X || y != points[p + 1].Value.Y);
        }
        return field.Select(x => string.Concat(x.Select(_ => _ == '.' ? ' ' : _)).TrimEnd()).ToArray();
    }

    static void Main(string[] args)
    {
        var hwLine = Console.ReadLine();
        string[] hw = hwLine.Split(' ');
        var h = int.Parse(hw[0]);

        var inputs = new string[h + 1];
        inputs[0] = hwLine;
        for (int i = 1; i <= h; i++)
        {
            inputs[i] = Console.ReadLine();
        }
        foreach (var line in Solve(inputs))
        {
            Console.WriteLine(line);
        }
    }
}
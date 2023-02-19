using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public static class Solution
{
    public static string[] Solve(string[] input)
    {
        string[] hwLine = input[0].Split(' ');
        int h = int.Parse(hwLine[0]);
        int w = int.Parse(hwLine[1]);
        int totalNumberOfMines = int.Parse(input[1]);

        var field = new (int x, int y, int value, bool unknown, bool mine)[h * w];

        for (int i = 0; i < h; i++)
        {
            var line = input[2 + i];
            for (int j = 0; j < w; j++)
            {
                var idx = (i * w) + j;
                field[idx].x = j;
                field[idx].y = i;
                if (line[j] == '?')
                {
                    field[idx].unknown = true;
                }
                else
                {
                    field[idx].value = line[j] == '.' ? 0 : int.Parse(line[j].ToString());
                }
            }
        }

        do
        {
            var borderCells = field.Where(f => f.value > 0 && !f.unknown);
            // try all borderCells
            foreach (var bcell in borderCells)
            {
                var unknownCells = field.Where(n =>
                    Math.Abs(n.x - bcell.x) <= 1 &&
                    Math.Abs(n.y - bcell.y) <= 1 &&
                    n.unknown
                ).ToArray();
                // if number of unknown cells nearby is equal to border's cell value
                if (unknownCells.Length == bcell.value)
                {
                    for (int i = 0; i < unknownCells.Length; i++)
                    {
                        // mark every such unknown cell as mine
                        var uIdx = (unknownCells[i].y * w) + unknownCells[i].x;
                        field[uIdx].unknown = false;
                        field[uIdx].mine = true;
                        var reducedCells = field.Where(n =>
                            Math.Abs(n.x - unknownCells[i].x) <= 1 &&
                            Math.Abs(n.y - unknownCells[i].y) <= 1 &&
                            !n.unknown && n.value > 0
                        );
                        // for each border cell nearby reduce the value
                        foreach (var rcell in reducedCells)
                        {
                            if (rcell.value > 0)
                            {
                                var nIdx = (rcell.y * w) + rcell.x;
                                field[nIdx].value--;
                                if (field[nIdx].value == 0)
                                {
                                    //if value on the border becomes zero, mark any adjacent unknown cell as safe
                                    var safeCells = field.Where(n =>
                                        Math.Abs(n.x - field[nIdx].x) <= 1 &&
                                        Math.Abs(n.y - field[nIdx].y) <= 1 &&
                                        n.unknown
                                    ).ToArray();

                                    foreach (var scell in safeCells)
                                    {
                                        var sIdx = (scell.y * w) + scell.x;
                                        field[sIdx].unknown = false;
                                        field[sIdx].value = 0;
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
            }
            //in case we have some hidden mines - 
            //if a number of mines left is equal to number of unknown cells left
            // mark them all as mines
            if (totalNumberOfMines - field.Count(f => f.mine) == field.Count(f => f.unknown))
            {
                var mineCells = field.Where(f => f.unknown).ToArray();
                foreach (var mcell in mineCells)
                {
                    var sIdx = (mcell.y * w) + mcell.x;
                    field[sIdx].unknown = false;
                    field[sIdx].mine = true;
                }
            }
        } while (field.Count(f => f.mine) < totalNumberOfMines);

        return field.Where(f => f.mine)
            .OrderBy(f => f.x)
            .ThenBy(f => f.y)
            .Select(f => $"{f.x} {f.y}")
            .ToArray();
    }

    static void Main(string[] args)
    {
        var firstLine = Console.ReadLine();
        var h = int.Parse(firstLine.Split(' ').First());
        var input = new string[h + 2];
        input[0] = firstLine;
        for (int i = 1; i < h + 2; i++)
        {
            input[i] = Console.ReadLine();
        }

        foreach (var line in Solve(input))
        {
            Console.WriteLine(line);
        }
    }
}
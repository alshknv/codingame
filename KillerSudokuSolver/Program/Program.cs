using System;
using System.Linq;
using System.Collections.Generic;

namespace Codingame;

public static class Solution
{
    const string cageSymbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWZYZ";
    // precalculated indices of cells in rows, columns, squares so we don't need to compute them
    static readonly byte[][] squares = new byte[][] {
        new byte[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 }, new byte[] { 3, 4, 5, 12, 13, 14, 21, 22, 23 }, new byte[] { 6, 7, 8, 15, 16, 17, 24, 25, 26 },
        new byte[] { 27, 28, 29, 36, 37, 38, 45, 46, 47 }, new byte[] { 30, 31, 32, 39, 40, 41, 48, 49, 50 }, new byte[] { 33, 34, 35, 42, 43, 44, 51, 52, 53 },
        new byte[] { 54, 55, 56, 63, 64, 65, 72, 73, 74 }, new byte[] { 57, 58, 59, 66, 67, 68, 75, 76, 77 }, new byte[] { 60, 61, 62, 69, 70, 71, 78, 79, 80 } };
    static readonly byte[][] rows = new byte[][] {
        new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[] { 9, 10, 11, 12, 13, 14, 15, 16, 17 }, new byte[] { 18, 19, 20, 21, 22, 23, 24, 25, 26 },
        new byte[] { 27, 28, 29, 30, 31, 32, 33, 34, 35 }, new byte[] { 36, 37, 38, 39, 40, 41, 42, 43, 44 }, new byte[] { 45, 46, 47, 48, 49, 50, 51, 52, 53 },
        new byte[] { 54, 55, 56, 57, 58, 59, 60, 61, 62 }, new byte[] { 63, 64, 65, 66, 67, 68, 69, 70, 71 }, new byte[] { 72, 73, 74, 75, 76, 77, 78, 79, 80 } };
    static readonly byte[][] columns = new byte[][] {
        new byte[] { 0, 9, 18, 27, 36, 45, 54, 63, 72 }, new byte[] { 1, 10, 19, 28, 37, 46, 55, 64, 73 }, new byte[] { 2, 11, 20, 29, 38, 47, 56, 65, 74 },
        new byte[] { 3, 12, 21, 30, 39, 48, 57, 66, 75 }, new byte[] { 4, 13, 22, 31, 40, 49, 58, 67, 76 }, new byte[] { 5, 14, 23, 32, 41, 50, 59, 68, 77 },
        new byte[] { 6, 15, 24, 33, 42, 51, 60, 69, 78 }, new byte[] { 7, 16, 25, 34, 43, 52, 61, 70, 79 }, new byte[] { 8, 17, 26, 35, 44, 53, 62, 71, 80 } };

    /// <summary>
    /// Just a Sum() extension for byte type
    /// </summary>
    /// <param name="bytes"></param>
    public static byte Sum(this IEnumerable<byte> bytes)
    {
        byte sum = 0;
        foreach (var b in bytes)
        {
            sum += b;
        }
        return sum;
    }


    /// <summary>
    /// Checks a group of cells whether it contains different digits.
    /// if there're cells not filled yet, hasZeros would be true
    /// </summary>
    /// <param name="cells"></param>
    /// <param name="grid"></param>
    static (bool fail, bool hasZeros) CheckGroup(byte[] cells, byte[] grid)
    {

        var digits = new bool[10];
        var hasZeros = false;
        for (byte i = 0; i < cells.Length; i++)
        {
            if (grid[cells[i]] == 0)
            {
                hasZeros = true;
                continue;
            }
            if (digits[grid[cells[i]]]) return (true, hasZeros);
            digits[grid[cells[i]]] = true;
        }
        return (false, hasZeros);
    }

    /// <summary>
    /// Checks rows, columns and squares to which each changed cells belongs
    /// avoiding double checks
    /// </summary>
    /// <param name="fail"></param>
    /// <param name="cells"></param>
    /// <param name="grid"></param>
    static (bool fail, bool hasZeros) Check(byte[] cells, byte[] grid)
    {
        var hasZeros = false;
        var checkedRows = new bool[9];
        var checkedCols = new bool[9];
        var checkedSquares = new bool[9];
        for (byte i = 0; i < cells.Length; i++)
        {
            var row = (byte)(cells[i] / 9);
            var col = (byte)(cells[i] % 9);
            var sq = (byte)((col % 3) + ((row / 3) * 3));

            if (!checkedRows[row])
            {
                var (fail, zeros) = CheckGroup(rows[row], grid);
                if (fail) return (fail, zeros);
                checkedRows[row] = true;
                hasZeros |= zeros;
            }

            if (!checkedCols[col])
            {
                var (fail, zeros) = CheckGroup(columns[col], grid);
                if (fail) return (fail, zeros);
                checkedCols[col] = true;
                hasZeros |= zeros;
            }

            if (checkedSquares[sq])
            {
                var (fail, zeros) = CheckGroup(squares[sq], grid);
                if (fail) return (fail, zeros);
                checkedSquares[sq] = true;
                hasZeros |= zeros;
            }
        }
        return (false, hasZeros);
    }

    /// <summary>
    /// Precalculates all possible combinations for killer cages
    /// </summary>
    /// <param name="cage"></param>
    /// <param name="value"></param>
    /// <param name="grid"></param>
    /// <param name="fix"></param>
    static byte[][] PrepareCageSolutions(byte[] cage, byte value, byte[] grid, bool[] fix)
    {
        var solutions = new List<byte[]>();
        var combination = cage.Select(x => fix[x] ? grid[x] : (byte)0).ToArray();
        if (cage.All(x => fix[x]))
        {
            // if all cells are fixed, it the only solution
            solutions.Add(combination);
            return Array.Empty<byte[]>();
        }
        do
        {
            // try all combinations starting from last cell
            var i = combination.Length - 1;
            while (fix[cage[i]])
            {
                i--;
            }

            combination[i]++;
            if (combination.All(x => x > 0) && combination.Sum() == value && combination.GroupBy(x => x).Count() == combination.Length)
            {
                // if sum is equal to cage value add it to solutions
                solutions.Add(combination.ToArray());
            }

            // if 9 is reached, reset values of non-fixed cells which contain 9 to 0 and increment next non-fixed cell
            // if there's such a cell
            if (combination[i] == 9 && combination.Where((v, idx) => idx < i && !fix[cage[idx]] && v < 9).Any())
            {
                combination[i--] = 0;
                while (fix[cage[i]] || combination[i] == 9)
                {
                    if (!fix[cage[i]] && combination[i] == 9)
                    {
                        combination[i] = 0;
                    }
                    i--;
                }
                combination[i]++;
            }
        }
        while (combination.Where((_, idx) => !fix[cage[idx]]).Any(x => x != 9));
        return solutions.ToArray();
    }

    /// <summary>
    /// Prints the solution of sudoku
    /// </summary>
    /// <param name="grid"></param>
    static IEnumerable<string> PrintOut(byte[] grid)
    {
        foreach (var byteLine in grid.Chunk(9))
        {
            yield return string.Concat(byteLine.Select(x => x == 0 ? "." : x.ToString()));
        }
    }

    /// <summary>
    /// Applies cetain solution to the given cage
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="fix"></param>
    /// <param name="cageCells"></param>
    /// <param name="solution"></param>
    static bool ApplySolution(byte[] grid, bool[] fix, byte[] cageCells, byte[] solution)
    {
        if (solution.Length == 0) return false;
        for (byte i = 0; i < cageCells.Length; i++)
        {
            if (!fix[cageCells[i]])
            {
                grid[cageCells[i]] = solution[i];
            }
        }

        return true;
    }

    /// <summary>
    /// Resets applied solution for backtracking
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="fix"></param>
    /// <param name="cageCells"></param>
    static void ResetSolution(byte[] grid, bool[] fix, byte[] cageCells)
    {
        for (byte i = 0; i < cageCells.Length; i++)
        {
            if (!fix[cageCells[i]])
            {
                grid[cageCells[i]] = 0;
            }
        }
    }

    /// <summary>
    /// Solve public method
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static IEnumerable<string> Solve(string[] input)
    {
        // prepare data & do precalculations
        var grid = new byte[9 * 9];
        var fix = new bool[9 * 9];
        var cageValue = new byte[9 * 9];
        var cageCells = new List<byte>[9 * 9];

        // fill grid and cages from input data, mark fixed cells
        for (byte i = 0; i < 9; i++)
        {
            string[] inputs = input[i].Split(' ');
            string gridLine = inputs[0];
            string gridCages = inputs[1];
            for (byte j = 0; j < 9; j++)
            {
                if (gridLine[j] != '.')
                {
                    grid[(9 * i) + j] = byte.Parse(gridLine[j].ToString());
                    fix[(9 * i) + j] = true;
                }
                (cageCells[(byte)cageSymbols.IndexOf(gridCages[j])] ??= new()).Add((byte)((9 * i) + j));
            }
        }

        // array containing list of cells for each cage
        var cages = cageCells.Select(x => x?.ToArray()).ToArray();

        // values of cages
        foreach (var cageValueString in input.Last().Split(' '))
        {
            var splitValue = cageValueString.Split('=');
            cageValue[(byte)cageSymbols.IndexOf(splitValue[0][0])] = byte.Parse(splitValue[1]);
        }

        // precalculate solutions for each cage
        var solutions = new byte[9 * 9][][];
        for (byte i = 0; i < cages.Length; i++)
        {
            if (cages[i] == null) break;
            solutions[i] = PrepareCageSolutions(cages[i], cageValue[i], grid, fix);
        }

        // order cages - the ones with the least number of solutions go first
        var cageOrder = solutions.Where(x => x != null).Select((x, i) => (solution: x, index: i, cells: cages[i].Length)).OrderBy(x => x.solution.Length).ThenByDescending(x => x.cells)
            .Select(x => x.index).ToArray();

        var solutionIndex = new byte[9 * 9];
        byte cageIndex = 0;

        // try applying solutions to cages one-by one and backtrack if check fails
        while (cageIndex < cageOrder.Length)
        {
            while (solutionIndex[cageOrder[cageIndex]] < solutions[cageOrder[cageIndex]].Length && ApplySolution(grid, fix, cages[cageOrder[cageIndex]], solutions[cageOrder[cageIndex]][solutionIndex[cageOrder[cageIndex]]]))
            {
                var (fail, zeros) = Check(cages[cageOrder[cageIndex]], grid);
                if (!fail && zeros)
                {
                    cageIndex++;
                }
                else if (!fail && !zeros)
                {
                    if (cageIndex == cageOrder.Length - 1)
                    {
                        return PrintOut(grid);
                    }
                    else
                    {
                        cageIndex++;
                    }
                }
                else
                {
                    solutionIndex[cageOrder[cageIndex]]++;
                }
            }
            // backtrack
            while (solutionIndex[cageOrder[cageIndex]] == solutions[cageOrder[cageIndex]].Length)
            {
                ResetSolution(grid, fix, cages[cageOrder[cageIndex]]);
                solutionIndex[cageOrder[cageIndex]] = 0;
                solutionIndex[cageOrder[--cageIndex]]++;
            }
        }
        return Array.Empty<string>();
    }

    static void Main(string[] args)
    {
        var result = Solve(Enumerable.Repeat(0, 10).Select(_ => Console.ReadLine()).ToArray());
        foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }
}
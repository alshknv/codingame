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
public static class Player
{
    static void Main(string[] args)
    {
        var inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // number of columns.
        int H = int.Parse(inputs[1]); // number of rows.

        var grid = new int[W, H];
        for (int i = 0; i < H; i++)
        {
            var line = Console.ReadLine().Split(' '); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            for (var j = 0; j < line.Length; j++)
            {
                grid[j, i] = int.Parse(line[j]);
            }
        }
        int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        var rooms = new Dictionary<int, Dictionary<string, string>>() {
            {1, new Dictionary<string, string>() { {"TOP", "DOWN"},{ "RIGHT", "DOWN" },{ "LEFT", "DOWN" } }},
            {2, new Dictionary<string, string>() { { "RIGHT", "LEFT" },{ "LEFT", "RIGHT" } }},
            {3, new Dictionary<string, string>() { {"TOP", "DOWN"} }},
            {4, new Dictionary<string, string>() { {"TOP", "LEFT"},{ "RIGHT", "DOWN" } }},
            {5, new Dictionary<string, string>() { {"TOP", "RIGHT"},{ "LEFT", "DOWN" } }},
            {6, new Dictionary<string, string>() { { "RIGHT", "LEFT" },{ "LEFT", "RIGHT" } }},
            {7, new Dictionary<string, string>() { {"TOP", "DOWN"},{ "RIGHT", "DOWN" } }},
            {8, new Dictionary<string, string>() { {"LEFT", "DOWN"},{ "RIGHT", "DOWN" } }},
            {9, new Dictionary<string, string>() { {"LEFT", "DOWN"},{ "TOP", "DOWN" } }},
            {10,new Dictionary<string, string>() { {"TOP", "LEFT"} }},
            {11,new Dictionary<string, string>() { {"TOP", "RIGHT"} }},
            {12,new Dictionary<string, string>() { {"RIGHT", "DOWN"} }},
            {13,new Dictionary<string, string>() { {"LEFT", "DOWN"} }}
        };

        var direction = "TOP"; // starts fallting from the top
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int XI = int.Parse(inputs[0]);
            int YI = int.Parse(inputs[1]);
            string POS = inputs[2];

            switch (rooms[grid[XI, YI]][direction])
            {
                case "LEFT":
                    XI--;
                    direction = "RIGHT";
                    break;
                case "RIGHT":
                    XI++;
                    direction = "LEFT";
                    break;
                case "DOWN":
                    YI++;
                    direction = "TOP";
                    break;
            }

            Console.WriteLine($"{XI} {YI}");
        }
    }
}
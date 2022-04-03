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
        private readonly static string[] commands = new string[] { "SPEED", "WAIT", "UP", "DOWN", "JUMP", "SLOW" };

        private static void Move(bool[][] bridge, (int L, int C, int Speed, bool Active)[] bikes, int laneDelta, int speedDelta, bool jump)
        {
            for (int i = 0; i < bikes.Length; i++)
            {
                // lost bikes do not move
                if (!bikes[i].Active) continue;

                // adjust speed, do not make it 0, it is pointless if you really want to cross the bridge
                bikes[i].Speed += speedDelta;
                if (bikes[i].Speed < 1) bikes[i].Speed = 1;
                //check for holes
                var pointsPassed = new List<bool>();
                if (laneDelta == 0)
                {
                    // points to check if bike does not change lane
                    if (!jump) pointsPassed.AddRange(bridge[bikes[i].L].Skip(bikes[i].C + 1).Take(bikes[i].Speed));
                    else if (bikes[i].C + bikes[i].Speed < bridge[0].Length) pointsPassed.Add(bridge[bikes[i].L][bikes[i].C + bikes[i].Speed]);
                }
                else
                {
                    // points when bike changes the lane
                    pointsPassed.AddRange(bridge[bikes[i].L + laneDelta].Skip(bikes[i].C + 1).Take(bikes[i].Speed));
                    pointsPassed.AddRange(bridge[bikes[i].L].Skip(bikes[i].C + 1).Take(bikes[i].Speed - 1));
                }
                // if any of points checked is a hole bike is lost, otherwise it changes its position
                if (pointsPassed.Any(x => !x))
                {
                    bikes[i].Active = false;
                }
                else
                {
                    bikes[i].C += bikes[i].Speed;
                    bikes[i].L += laneDelta;
                }
            }
        }

        private static (bool success, string[] strategy) NextStep(bool[][] bridge, (int L, int C, int Speed, bool Active)[] bikes, int minBikes, List<string> steps)
        {
            var lastStep = steps[^1];
            //move bike acording to last step's command
            switch (lastStep)
            {
                case "SPEED":
                    Move(bridge, bikes, 0, 1, false);
                    break;
                case "SLOW":
                    Move(bridge, bikes, 0, -1, false);
                    break;
                case "JUMP":
                    Move(bridge, bikes, 0, 0, true);
                    break;
                case "UP":
                    Move(bridge, bikes, bikes.All(b => b.L > 0) ? -1 : 0, 0, false);
                    break;
                case "DOWN":
                    Move(bridge, bikes, bikes.All(b => b.L < bridge.Length - 1) ? 1 : 0, 0, false);
                    break;
                case "WAIT":
                    Move(bridge, bikes, 0, 0, false);
                    break;
            }
            // if too many bikes are lost, return false, if bikes have crossed the bridge return true, it's a victory
            //otherwise recursively try all possible commands
            if (bikes.Count(b => b.Active) < minBikes || steps.Count > 50) return (false, steps.ToArray());
            if (bikes.Where(b => b.Active).All(b => b.C >= bridge[0].Length)) return (true, steps.ToArray());
            foreach (var command in commands)
            {
                var stepList = new List<string>(steps)
                {
                    command
                };
                var result = NextStep((bool[][])bridge.Clone(), ((int L, int C, int Speed, bool Active)[])bikes.Clone(), minBikes, stepList);
                if (result.success) return result;
            }
            return (false, steps.ToArray());
        }

        public static bool[][] InitBridge(string[] bridgeData)
        {
            var bridge = new bool[bridgeData.Length][];

            for (int i = 0; i < bridgeData.Length; i++)
            {
                bridge[i] = new bool[bridgeData[i].Length];
                for (int j = 0; j < bridgeData[i].Length; j++)
                {
                    bridge[i][j] = bridgeData[i][j] == '.';
                }
            }
            return bridge;
        }

        public static string[] CalculateStrategy(bool[][] bridge, int initSpeed, (int L, int C, int Speed, bool Active)[] bikes, int minBikes)
        {
            // set initial speed
            for (int i = 0; i < bikes.Length; i++)
            {
                bikes[i].Speed = initSpeed;
            }

            // try to find solution with maximum number of active bikes
            for (int b = bikes.Length; b >= minBikes; b--)
            {
                // for every possible first step try to construct entire strategy
                // once successful strategy is found, return it immediately
                for (int i = 0; i < commands.Length; i++)
                {
                    var (success, strategy) = NextStep((bool[][])bridge.Clone(), ((int, int, int, bool)[])bikes.Clone(), b, new List<string>() { commands[i] });
                    if (success) return strategy;
                }
            }
            return new string[0];
        }

        static void Main(string[] args)
        {
            int M = int.Parse(Console.ReadLine()); // the amount of motorbikes to control
            int V = int.Parse(Console.ReadLine()); // the minimum amount of motorbikes that must survive

            var L = new string[4];
            L[0] = Console.ReadLine(); // L0 to L3 are lanes of the road. A dot character . represents a safe space, a zero 0 represents a hole in the road.
            L[1] = Console.ReadLine();
            L[2] = Console.ReadLine();
            L[3] = Console.ReadLine();

            foreach (var l in L)
            {
                Console.Error.WriteLine(l);
            }

            var bridge = InitBridge(L);

            var bikes = new (int L, int C, int Speed, bool Active)[M];
            string[] strategy = null;
            int step = 0;
            // game loop
            while (true)
            {
                int S = int.Parse(Console.ReadLine()); // the motorbikes' speed
                for (int i = 0; i < M; i++)
                {
                    string[] inputs = Console.ReadLine().Split(' ');
                    int X = int.Parse(inputs[0]); // x coordinate of the motorbike
                    int Y = int.Parse(inputs[1]); // y coordinate of the motorbike
                    int A = int.Parse(inputs[2]); // indicates whether the motorbike is activated "1" or detroyed "0"
                    bikes[i] = (Y, X, S, A == 1);
                }
                // calculate the strategy once and then follow it every next game step
                if (strategy == null)
                {
                    strategy = CalculateStrategy(bridge, S, bikes, V);
                }

                // A single line containing one of 6 keywords: SPEED, SLOW, JUMP, WAIT, UP, DOWN.
                var move = step < strategy.Length ? strategy[step] : "WAIT";
                Console.WriteLine(move);
                step++;
            }
        }
    }
}
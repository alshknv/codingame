using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public class Move
    {
        public (int l, int c) Position;
        public string Direction;
        public Move((int l, int c) position, string direction)
        {
            Position = position;
            Direction = direction;
        }
    }

    public static class Solution
    {
        // normal and inverted priorities
        private static readonly Dictionary<bool, string[]> directionPriority = new Dictionary<bool, string[]>() {
            {false, new string[] {"SOUTH", "EAST", "NORTH", "WEST"}},
            {true, new string[] {"WEST", "NORTH", "EAST", "SOUTH"}}
        };

        //next position based on current position and direction without any checks
        private static (int l, int c) GetNextPos((int l, int c) curPos, string direction)
        {
            return direction switch
            {
                "SOUTH" => (curPos.l + 1, curPos.c),
                "EAST" => (curPos.l, curPos.c + 1),
                "NORTH" => (curPos.l - 1, curPos.c),
                "WEST" => (curPos.l, curPos.c - 1),
                _ => throw new ArgumentOutOfRangeException(direction, "not supported direction")
            };
        }

        // find direction after we hit the obstacle
        private static string GetNextDirection(char[][] map, (int, int) currentPos, bool inverted, bool breaker)
        {
            string direction;
            bool foundDirection = false;
            var priorityIndex = 0;
            do
            {
                // try all directions in order of current priority to find where we can go
                direction = directionPriority[inverted][priorityIndex];
                priorityIndex++;
                (int l, int c) = GetNextPos(currentPos, direction);
                var turnField = map[l][c];
                if (turnField != '#' && (turnField != 'X' || breaker))
                {
                    foundDirection = true;
                    // breaker mode on, so break the wall
                    if (turnField == 'X') map[l][c] = ' ';
                }
            } while (!foundDirection);
            return direction;
        }

        // check if there's a loop
        private static bool HasLoop(List<Move> moves)
        {
            if (moves.Count > 0)
            {
                var lastMove = moves[^1];
                var repeatCount = 0;
                for (int i = moves.Count - 2; i >= 0; i--)
                {
                    if (lastMove.Position == moves[i].Position && lastMove.Direction == moves[i].Direction)
                    {
                        repeatCount++;
                    }
                    // go in a loop at least 5 times to avoid false loop detection
                    if (repeatCount >= 5) return true;
                }
            }
            return false;
        }

        private static ((int, int), string direction, bool inverted, bool breaker, string lastMove, List<Move> moveList) Move(int L, int C, char[][] map, ((int l, int c), string direction, bool inverted, bool breaker, string lastMove, List<Move> moveList) state)
        {
            ((int l, int c), string direction, bool inverted, bool breaker, string lastMove, List<Move> moveList) result =
                ((state.Item1.l, state.Item1.c), state.direction, state.inverted, state.breaker, "", state.moveList);
            // make move if possible
            var nextPos = GetNextPos(result.Item1, result.direction);
            var nextField = map[nextPos.l][nextPos.c];
            if (nextField != '#' && (nextField != 'X' || state.breaker))
            {
                if (nextField == 'X')
                {
                    map[nextPos.l][nextPos.c] = ' ';
                }
                // store the move and check if there's a loop
                result.Item1 = nextPos;
                result.lastMove = result.direction;
                result.moveList.Add(new Move(result.Item1, result.direction));
                if (HasLoop(result.moveList))
                {
                    result.lastMove = "LOOP";
                    return result;
                }
            }
            else
            {
                result.Item1 = state.Item1;
            }
            // then make turns, teleportations etc
            switch (map[nextPos.l][nextPos.c])
            {
                case '#':
                case 'X':
                    result.direction = GetNextDirection(map, result.Item1, state.inverted, state.breaker);
                    break;
                case 'N':
                    result.direction = "NORTH";
                    break;
                case 'E':
                    result.direction = "EAST";
                    break;
                case 'W':
                    result.direction = "WEST";
                    break;
                case 'S':
                    result.direction = "SOUTH";
                    break;
                case 'I':
                    result.inverted = !state.inverted;
                    break;
                case 'B':
                    result.breaker = !state.breaker;
                    break;
                case 'T':
                    for (int l = 0; l < L; l++)
                    {
                        for (int c = 0; c < C; c++)
                        {
                            if (map[l][c] == 'T' && (l != result.Item1.l || c != result.Item1.c))
                            {
                                result.Item1 = (l, c);
                                return result;
                            }
                        }
                    }
                    break;
            }
            return result;
        }
        public static string[] GetPath(string[] stringMap)
        {
            //initialize
            var L = stringMap.Length;
            var C = stringMap[0].Length;
            var map = new char[L][];
            ((int l, int c), string direction, bool inverted, bool breaker, string lastMove, List<Move> moveList) state = default;
            state.direction = "SOUTH";
            state.lastMove = "";
            state.moveList = new List<Move>();
            (int l, int c) booth = default;
            for (int l = 0; l < L; l++)
            {
                map[l] = new char[C];
                for (int c = 0; c < C; c++)
                {
                    map[l][c] = stringMap[l][c];
                    if (map[l][c] == '@') state.Item1 = (l, c);
                    if (map[l][c] == '$') booth = (l, c);
                }
            }

            // go on until the booth is reached
            var moves = new List<string>();
            do
            {
                state = Move(L, C, map, state);
                if (!string.IsNullOrEmpty(state.lastMove))
                {
                    if (state.lastMove == "LOOP") return new string[] { "LOOP" };
                    moves.Add(state.lastMove);
                }
            } while (state.Item1 != booth);
            return moves.ToArray();
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            var mapLines = new string[L];
            for (int i = 0; i < L; i++)
            {
                mapLines[i] = Console.ReadLine();
            }

            var route = GetPath(mapLines);
            for (int i = 0; i < route.Length; i++)
            {
                Console.WriteLine(route[i]);
            }
        }
    }
}
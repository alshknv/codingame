using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame
{
    public static class Player
    {
        private static readonly Dictionary<int, Dictionary<string, string>> roomDict = new Dictionary<int, Dictionary<string, string>>() {
            {0, new Dictionary<string, string>() },
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

        private static readonly Dictionary<int, Dictionary<string, int>> rotationDict = new Dictionary<int, Dictionary<string, int>>() {
            {0, new Dictionary<string, int>() { {"LEFT", 0}, {"RIGHT", 0} }},
            {1, new Dictionary<string, int>() { {"LEFT", 1}, {"RIGHT", 1} }},
            {2, new Dictionary<string, int>() { {"LEFT", 3}, {"RIGHT", 3} }},
            {3, new Dictionary<string, int>() { {"LEFT", 2}, {"RIGHT", 2} }},
            {4, new Dictionary<string, int>() { {"LEFT", 5}, {"RIGHT", 5} }},
            {5, new Dictionary<string, int>() { {"LEFT", 4}, {"RIGHT", 4} }},
            {6, new Dictionary<string, int>() { {"LEFT", 9}, {"RIGHT", 7} }},
            {7, new Dictionary<string, int>() { {"LEFT", 6}, {"RIGHT", 8} }},
            {8, new Dictionary<string, int>() { {"LEFT", 7}, {"RIGHT", 9} }},
            {9, new Dictionary<string, int>() { {"LEFT", 8}, {"RIGHT", 6} }},
            {10, new Dictionary<string, int>() { {"LEFT", 13}, {"RIGHT", 11} }},
            {11, new Dictionary<string, int>() { {"LEFT", 10}, {"RIGHT", 12} }},
            {12, new Dictionary<string, int>() { {"LEFT", 11}, {"RIGHT", 13} }},
            {13, new Dictionary<string, int>() { {"LEFT", 12}, {"RIGHT", 10} }}
        };

        private static int[,] CloneGrid(int[,] grid, int w, int h, int exit)
        {
            var clonedGrid = new int[w, h + 1];
            for (int i = 0; i < h + 1; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (i < h)
                        clonedGrid[j, i] = grid[j, i];
                    else
                        clonedGrid[j, i] = j == exit ? 3 : 0;
                }
            }
            return clonedGrid;
        }

        private static List<(int x, int y, string direction)> CreateRotationList((int x, int y)[] path, int[,] grid1, int[,] grid2)
        {
            // given grid before rotations, grid after rotations and path
            // generate list of rotations we need to perform along our path
            var rotationList = new List<(int x, int y, string direction)>();
            for (int i = 0; i < path.Length - 1; i++)
            {
                var rotateDirection = "LEFT";
                while (grid1[path[i].x, path[i].y] != grid2[path[i].x, path[i].y])
                {
                    grid1[path[i].x, path[i].y] = rotationDict[Math.Abs(grid1[path[i].x, path[i].y])]["LEFT"];
                    if (rotationList.Count >= 2 && rotationList[^1].x == path[i].x && rotationList[^1].y == path[i].y
                        && rotationList[^2].x == path[i].x && rotationList[^2].y == path[i].y)
                    {
                        // three rotations to the left are equivalent to one rotation to the right
                        rotationList.RemoveRange(rotationList.Count - 2, 2);
                        rotateDirection = "RIGHT";
                    }
                    rotationList.Add((path[i].x, path[i].y, rotateDirection));
                }
            }
            return rotationList;
        }

        public static List<(int x, int y)> FindPath(int w, int h, int[,] grid, (int x, int y) pos, string direction, int exit)
        {
            var path = new List<(int x, int y)>();
            while (pos.y < h || pos.x != exit)
            {
                if (pos.x < 0 || pos.x >= w || !roomDict[Math.Abs(grid[pos.x, pos.y])].ContainsKey(direction)) break;
                switch (roomDict[Math.Abs(grid[pos.x, pos.y])][direction])
                {
                    case "LEFT":
                        pos.x--;
                        direction = "RIGHT";
                        break;
                    case "RIGHT":
                        pos.x++;
                        direction = "LEFT";
                        break;
                    case "DOWN":
                        pos.y++;
                        direction = "TOP";
                        break;
                }
                if (pos.x >= 0 && pos.x < w && pos.y < h && grid[pos.x, pos.y] != 0)
                    path.Add((pos.x, pos.y));
            }
            return path;
        }

        public static (List<(int x, int y, string direction)> rotations, (int x, int y)[] path, int[,] grid) FindRotations(int w, int h, int[,] grid, (int x, int y) pos, string direction, int exit)
        {
            var originalGrid = CloneGrid(grid, w, h, exit);
            var rotatedGrid = CloneGrid(grid, w, h, exit);
            var path = FindPath(w, h, originalGrid, pos, direction, exit);
            var pathLast = path.Count > 0 ? path[^1] : (x: 0, y: 0);
            var deadends = new Dictionary<string, List<(int x, int y)>>();
            while (path.Count > 0 && (pathLast.x != exit || pathLast.y < h))
            {
                //rotate and find path again
                if (rotatedGrid[pathLast.x, pathLast.y] >= 0)
                    rotatedGrid[pathLast.x, pathLast.y] = rotationDict[Math.Abs(rotatedGrid[pathLast.x, pathLast.y])]["LEFT"];
                if (rotatedGrid[pathLast.x, pathLast.y] == originalGrid[pathLast.x, pathLast.y])
                {
                    // we checked all rotations, for this particular path this is dead end
                    var pathKey = path.ToKey();
                    if (deadends.ContainsKey(pathKey))
                    {
                        deadends[pathKey].Add((pathLast.x, pathLast.y));
                    }
                    else
                    {
                        deadends.Add(pathKey, new List<(int x, int y)>() { (pathLast.x, pathLast.y) });
                    }
                }

                path = FindPath(w, h + 1, rotatedGrid, pos, direction, exit);

                if (path.Count > 0)
                {
                    var backtrack = 1;
                    var pathKey = path.ToKey();
                    pathLast = path[^backtrack];
                    if (deadends.ContainsKey(pathKey))
                    {
                        while (deadends[pathKey].Contains(pathLast))
                        {
                            backtrack++;
                            pathLast = path[^backtrack];
                        }
                    }
                }
            }
            return (CreateRotationList(path.ToArray(), originalGrid, rotatedGrid), path.ToArray(), rotatedGrid);
        }

        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            int W = int.Parse(inputs[0]); // number of columns.
            int H = int.Parse(inputs[1]); // number of rows.
            var grid = new int[W, H];

            for (int i = 0; i < H; i++)
            {
                // each line represents a line in the grid and contains W integers T. The absolute value of T specifies the type of the room. If T is negative, the room cannot be rotated.
                var line = Console.ReadLine();
                Console.Error.WriteLine(line);
                var cells = line.Split(' ');
                for (var j = 0; j < cells.Length; j++)
                {
                    grid[j, i] = int.Parse(cells[j]);
                }
            }
            int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit.
            Console.Error.WriteLine($"Exit at {EX}");

            var direction = "TOP"; // starts falling from the top

            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int XI = int.Parse(inputs[0]);
                int YI = int.Parse(inputs[1]);
                var pos = (XI, YI);

                int R = int.Parse(Console.ReadLine()); // the number of rocks currently in the grid.
                var (rotateList, indyPath, indyGrid) = FindRotations(W, H, grid, pos, direction, EX);

                var rockPaths = new List<(int x, int y)[]>();
                for (int i = 0; i < R; i++)
                {
                    inputs = Console.ReadLine().Split(' ');
                    int XR = int.Parse(inputs[0]);
                    int YR = int.Parse(inputs[1]);
                    string dirR = inputs[2];
                    //find rock path in indy grid (as if necessary rotations have already been made)
                    var rockPath = FindPath(W, H, indyGrid, (XR, YR), dirR, EX).ToArray();
                    rockPaths.Add(rockPath);
                }

                // One of three commands: 'X Y LEFT', 'X Y RIGHT' or 'WAIT'
                var command = "WAIT";

                if (rotateList.Count == 0 || (indyPath.Length > 1 && rotateList.Count > 0 &&
                    (indyPath[0].x != rotateList[0].x || indyPath[0].y != rotateList[0].y)))
                {
                    // no immediate rotation needed, we have time to deal with rocks
                    var dangerousRocks = new Dictionary<int, (int step, (int x, int y) point)>();
                    for (var step = 0; step < indyPath.Length; step++)
                    {
                        for (int r = 0; r < R; r++)
                        {
                            if (step >= rockPaths[r].Length || dangerousRocks.ContainsKey(r)) continue;
                            var indyPos = indyPath[step];
                            var rockPos = rockPaths[r][step];
                            if (indyPos == rockPos)
                            {
                                // if we have found dangerous rock intersecting indy's path
                                // take closest point to the rock that can be rotated
                                var rockSteps = rockPaths[r].Take(step).Select((point, i) => (i, point)).Where(rs => grid[rs.point.x, rs.point.y] > 0)
                                    .OrderBy(rs => rs.i).FirstOrDefault();
                                if (rockSteps != default)
                                    dangerousRocks.Add(r, (rockSteps.i, rockSteps.point));
                            }
                        }
                    }

                    // closest rotating point of all dangerous rocks
                    var closestRockPoint = dangerousRocks.Values.OrderBy(x => x.step).FirstOrDefault();
                    if (closestRockPoint != default)
                    {
                        // insert rotation that turns the rock away at the beginning of the list 
                        rotateList.Insert(0, (closestRockPoint.point.x, closestRockPoint.point.y, "LEFT"));
                    }
                }

                // perform next rotation from indy's rotation list if it is not empty
                if (rotateList.Count > 0)
                {
                    command = $"{rotateList[0].x} {rotateList[0].y} {rotateList[0].direction}";
                    grid[rotateList[0].x, rotateList[0].y] = rotationDict[Math.Abs(grid[rotateList[0].x, rotateList[0].y])][rotateList[0].direction];
                }

                // change direction following the move
                switch (roomDict[Math.Abs(grid[XI, YI])][direction])
                {
                    case "LEFT":
                        direction = "RIGHT";
                        break;
                    case "RIGHT":
                        direction = "LEFT";
                        break;
                    case "DOWN":
                        direction = "TOP";
                        break;
                }
                Console.WriteLine(command);
            }
        }
    }

    public static class Extensions
    {
        public static string ToKey(this List<(int x, int y)> list)
        {
            return string.Concat(list.Select(c => $"({c.x},{c.y})"));
        }
    }
}
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
        private static List<int> GetClosestElevators(int curPos, int width, List<int> elevators)
        {
            var result = new List<int>();
            var closestRight = width + 1;
            var closestLeft = -1;
            for (int i = 0; i < elevators.Count; i++)
            {
                if (elevators[i] > curPos && elevators[i] < closestRight) closestRight = elevators[i];
                if (elevators[i] < curPos && elevators[i] > closestLeft) closestLeft = elevators[i];
            }
            if (closestRight < width - 1) result.Add(closestRight);
            if (closestLeft >= 0) result.Add(closestLeft);
            return result;
        }

        // straightforward recursive approach, enough for this simple problem
        private static (int[] path, int length) RecursiveSearch(int floor, int width, int exitFloor, int exitPos, int curPos, int genPos, List<int>[] elevators, List<int> path)
        {
            path = new List<int>(path) { curPos };
            if (floor == exitFloor)
            {
                //exit recursion return path
                var prevPos = genPos;
                var pathLength = Math.Abs(exitPos - curPos);
                for (int i = 0; i < path.Count; i++)
                {
                    pathLength += Math.Abs(path[i] - prevPos) + 1;
                    prevPos = path[i];
                }
                if (exitPos != curPos)
                    path.Add(exitPos);
                return (path.ToArray(), pathLength);
            }
            (int[] path, int length) minPath = (null, (width * elevators.Length) + 1);
            foreach (var elevator in GetClosestElevators(curPos, width, elevators[floor]))
            {
                var foundPath = RecursiveSearch(floor + 1, width, exitFloor, exitPos, elevator, genPos, elevators, path);
                if (foundPath.length < minPath.length)
                    minPath = foundPath;
            }
            return minPath;
        }

        public static (int, int)[] GetCloneBlocks(int width, int exitFloor, int exitPos, int genPos, List<int>[] elevators)
        {
            var (path, _) = RecursiveSearch(0, width, exitFloor, exitPos, genPos, genPos, elevators, new List<int>());
            var blocks = new List<(int, int)>();
            var right = true;
            for (int i = 0; i < path.Length; i++)
            {
                // place block if we need to change direction
                if (i > 0 && ((right && path[i - 1] > path[i]) || (!right && path[i - 1] < path[i])))
                {
                    blocks.Add((i - 1, path[i - 1]));
                    right = !right;
                }
            }
            return blocks.ToArray();
        }

        static void Main(string[] args)
        {
            var inputs = Console.ReadLine().Split(' ');
            int nbFloors = int.Parse(inputs[0]); // number of floors
            int width = int.Parse(inputs[1]); // width of the area
            int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
            int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
            int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
            int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
            int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
            int nbElevators = int.Parse(inputs[7]); // number of elevators

            var elevators = new List<int>[nbFloors];
            for (int i = 0; i < nbFloors; i++)
            {
                elevators[i] = new List<int>();
            }
            for (int i = 0; i < nbElevators; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
                int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
                elevators[elevatorFloor].Add(elevatorPos);
            }

            (int floor, int pos)[] blocks = null;
            var currentBlock = 0;
            // game loop
            while (true)
            {
                inputs = Console.ReadLine().Split(' ');
                int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
                int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
                string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT
                if (blocks == null)
                {
                    blocks = GetCloneBlocks(width, exitFloor, exitPos, clonePos, elevators);
                }
                if (currentBlock < blocks.Length && cloneFloor == blocks[currentBlock].floor && clonePos == blocks[currentBlock].pos)
                {
                    Console.WriteLine("BLOCK");
                    currentBlock++;
                }
                else
                {
                    Console.WriteLine("WAIT"); // action: WAIT or BLOCK
                }
            }
        }
    }
}
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
        public static int GetMaxValue(string[] rooms)
        {
            // init room data
            var graph = new (int value, int[] destinations)[rooms.Length];
            var values = new int[rooms.Length];
            for (int i = 0; i < rooms.Length; i++)
            {
                var roomData = rooms[i].Split(' ').Select(x => x != "E" ? int.Parse(x) : -1).ToArray();
                values[roomData[0]] = roomData[0] > 0 ? 0 : roomData[1];
                graph[roomData[0]].value = roomData[1];
                graph[roomData[0]].destinations = new int[2] { roomData[2], roomData[3] };
            }

            // synamic programming using queue
            var maxValue = 0;
            var queue = new Queue<int>();
            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                var currentRoom = queue.Dequeue();
                for (int i = 0; i < graph[currentRoom].destinations.Length; i++)
                {
                    var destination = graph[currentRoom].destinations[i];
                    if (destination < 0)
                    {
                        if (values[currentRoom] > maxValue)
                        {
                            maxValue = values[currentRoom];
                        }
                        continue;
                    }
                    if (values[currentRoom] + graph[destination].value > values[destination])
                    {
                        values[destination] = values[currentRoom] + graph[destination].value;
                        queue.Enqueue(destination);
                    }
                }
            }
            return maxValue;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var rooms = new string[N];
            for (int i = 0; i < N; i++)
            {
                rooms[i] = Console.ReadLine();
            }

            Console.WriteLine(GetMaxValue(rooms));
        }
    }
}
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
        public static long TotalEarnings(int sizeOfRide, int ridesPerDay, int[] groups)
        {
            //init queue with given groups
            var groupQueue = new Queue<(int vol, int idx)>(groups.Select((g, i) => (g, i)));
            long earnings = 0;
            var rideHistory = new List<(int start, int end, int earn)>();
            // repeat as many times as the spicified number of rides
            while (ridesPerDay > 0)
            {
                var groups2Ride = new List<(int vol, int idx)>(groupQueue.Count);
                var freeSize = sizeOfRide;

                // select next group to ride as long as there're still groups waiting for ride and there's enough place for next group
                while (groupQueue.Count > 0 && freeSize >= groupQueue.Peek().vol)
                {
                    var g2Ride = groupQueue.Dequeue();
                    groups2Ride.Add(g2Ride);
                    freeSize -= g2Ride.vol;
                }

                // search for current drive in the history. If found, there's a cycle
                var ride = (groups2Ride[0].idx, groups2Ride[^1].idx, earn: groups2Ride.Sum(g => g.vol));
                var historyIdx = rideHistory.IndexOf(ride);
                if (historyIdx >= 0)
                {
                    // calculate earnings for cycle
                    var cycleLength = rideHistory.Count - historyIdx;
                    var cycleCount = ridesPerDay / cycleLength;
                    long cycleEarnings = rideHistory.Where((_, i) => i >= historyIdx).Sum(h => h.earn);
                    earnings += cycleCount * cycleEarnings;
                    // some rides may have been left
                    ridesPerDay -= cycleCount * cycleLength;
                    rideHistory.Clear();
                }
                else
                {
                    // add ride to history
                    rideHistory.Add(ride);
                }

                // unless there're no rides left due to cycle calculation
                // finish ride, count earnings and enqueue groups back in queue
                if (ridesPerDay > 0)
                {
                    foreach (var group in groups2Ride)
                    {
                        earnings += group.vol;
                        groupQueue.Enqueue(group);
                    }
                    // decrease counter of rides to make
                    ridesPerDay--;
                }
            }
            return earnings;
        }

        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int L = int.Parse(inputs[0]);
            int C = int.Parse(inputs[1]);
            int N = int.Parse(inputs[2]);
            int[] groups = new int[N];
            for (int i = 0; i < N; i++)
            {
                groups[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(TotalEarnings(L, C, groups));
        }
    }
}
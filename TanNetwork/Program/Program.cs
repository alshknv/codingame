using System;
using System.Linq;
using System.Collections.Generic;

public static class Solution
{
    private static double GetDistance(double lat0, double lat1, double lon0, double lon1)
    {
        return Math.Sqrt(
                Math.Pow((lon1 - lon0) * Math.Cos((lat1 + lat0) / 2), 2) +
                Math.Pow(lat1 - lat0, 2)
            ) * 6371;
    }
    public static string[] FindPath(
        (string id, string name, double latitude, double longitude, double distToEnd)[] stops,
        (int destination, double distance)?[][] map,
        int start,
        int end)
    {
        var path = new Stack<string>();
        var dist = Enumerable.Repeat(double.MaxValue, stops.Length).ToArray();
        var prev = new int?[stops.Length];

        var stopQueue = new PriorityQueue<int, double>();
        stopQueue.Enqueue(start, stops[start].distToEnd);
        dist[start] = 0;
        while (stopQueue.Count > 0)
        {
            var currentStop = stopQueue.Dequeue();
            if (currentStop == end)
            {
                int? s = currentStop;
                while (s.HasValue)
                {
                    path.Push(stops[s.Value].name);
                    s = prev[s.Value];
                }
                break;
            }
            for (int i = 0; map[currentStop][i].HasValue; i++)
            {
                if (dist[currentStop] + map[currentStop][i].Value.distance < dist[map[currentStop][i].Value.destination])
                {
                    dist[map[currentStop][i].Value.destination] = dist[currentStop] + map[currentStop][i].Value.distance;
                    prev[map[currentStop][i].Value.destination] = currentStop;
                    stopQueue.Enqueue(map[currentStop][i].Value.destination, stops[map[currentStop][i].Value.destination].distToEnd);
                }
            }
        }
        return path.Count > 0 ? path.ToArray() : new string[] { "IMPOSSIBLE" };
    }

    public static string[] Solve(string[] input)
    {
        var stopCount = int.Parse(input[2]);
        var stops = new (string id, string name, double latitude, double longitude, double distToEnd)[stopCount];
        var map = new (int destination, double distance)?[stopCount][];
        Dictionary<string, int> stopMapping = new();

        for (int i = 0; i < stopCount; i++)
        {
            var stopInfo = input[3 + i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            stops[i] = (
                stopInfo[0],
                stopInfo[1].Replace("\"", ""),
                Math.PI / 180 * double.Parse(stopInfo[2], new System.Globalization.CultureInfo("en-US")),
                Math.PI / 180 * double.Parse(stopInfo[3], new System.Globalization.CultureInfo("en-US")),
                0);
            map[i] = new (int destination, double distance)?[stopCount];
            stopMapping.Add(stopInfo[0], i);
        }
        var routeCount = int.Parse(input[3 + stopCount]);
        var routesPerStop = new int[stopCount];
        for (int i = 0; i < routeCount; i++)
        {
            var routeInfo = input[4 + stopCount + i].Split(' ');
            var from = stopMapping[routeInfo[0]];
            var to = stopMapping[routeInfo[1]];
            map[from][routesPerStop[from]++] = (to, GetDistance(stops[from].latitude, stops[to].latitude, stops[from].longitude, stops[to].longitude));
        }

        var startPointId = stopMapping[input[0]];
        var endPointId = stopMapping[input[1]];
        var (_, _, endLatitude, endLongitude, _) = stops[endPointId];

        for (int i = 0; i < stopCount; i++)
        {
            stops[i].distToEnd = GetDistance(stops[i].latitude, endLatitude, stops[i].longitude, endLongitude);
        }

        return FindPath(stops, map, startPointId, endPointId);
    }

    static void Main(string[] args)
    {
        var input = new List<string>();
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            input.Add(line);
        }
        foreach (var stop in Solve(input.ToArray()))
        {
            Console.WriteLine(stop);
        }
    }
}
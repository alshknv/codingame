using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Codingame;

enum Carrier
{
    Me = 0, Other = 1, None = -1
}

static class GameDataSource
{
    public static int[] GetAvailableMolecules()
    {
        var available = new int[5];
        var inputs = Console.ReadLine().Split(' ');
        for (int a = 0; a < 5; a++)
        {
            available[a] = int.Parse(inputs[a]);
        }
        return available;
    }

    public static (string target, int eta, int score, int[] storage, int[] expertise)[] GetPlayers()
    {
        var players = new (string target, int eta, int score, int[] storage, int[] expertise)[2];

        for (int p = 0; p < 2; p++)
        {
            var inputs = Console.ReadLine().Split(' ');
            players[p].target = inputs[0];
            players[p].eta = int.Parse(inputs[1]);
            players[p].score = int.Parse(inputs[2]);
            players[p].storage = new int[5];
            for (int s = 0; s < 5; s++)
            {
                players[p].storage[s] = int.Parse(inputs[3 + s]);
            }
            players[p].expertise = new int[5];
            for (int e = 0; e < 5; e++)
            {
                players[p].expertise[e] = int.Parse(inputs[8 + e]);
            }
        }
        return players;
    }

    public static int[][] GetProjects()
    {
        int projectCount = int.Parse(Console.ReadLine());
        var projects = new int[projectCount][];
        for (int p = 0; p < projectCount; p++)
        {
            var inputs = Console.ReadLine().Split(' ');
            Console.Error.WriteLine(string.Join(" ", inputs));
            projects[p] = new int[5] {
                int.Parse(inputs[0]),
                int.Parse(inputs[1]),
                int.Parse(inputs[2]),
                int.Parse(inputs[3]),
                int.Parse(inputs[4])
            };
        }
        return projects;
    }

    public static (int id, Carrier carriedBy, int rank, int[] expertise, string expertiseGain, int health, int[] cost)[] GetSamples()
    {
        int sampleCount = int.Parse(Console.ReadLine());

        List<(int id, Carrier carriedBy, int rank, int[] expertise, string expertiseGain, int health, int[] cost)> samples = new();

        for (int i = 0; i < sampleCount; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            Console.Error.WriteLine(string.Join(" ", inputs));
            samples.Add((int.Parse(inputs[0]), (Carrier)int.Parse(inputs[1]), int.Parse(inputs[2]), new int[5], inputs[3], int.Parse(inputs[4]), new int[] {
                    int.Parse(inputs[5]),
                    int.Parse(inputs[6]),
                    int.Parse(inputs[7]),
                    int.Parse(inputs[8]),
                    int.Parse(inputs[9])
                }));
        }
        return samples.ToArray();
    }
}

static class Player
{
    static readonly Random random = new();
    static readonly int[] carriedMolecules = new int[5];
    static readonly Dictionary<int, string> idx2MoleculaDict = new() {
        { 0, "A" }, { 1, "B" }, { 2, "C" }, { 3, "D" }, { 4, "E" }
    };
    static readonly Dictionary<string, int> molecula2IdxDict = new() {
        { "A", 0 }, { "B", 1 }, { "C", 2 }, { "D", 3 }, { "E", 4 }
    };
    static readonly Dictionary<(string, string), int> distance = new() {
        {("START_POS","SAMPLES"), 2},       {("START_POS","DIAGNOSIS"), 2},     {("START_POS","MOLECULES"), 2},         {("START_POS","LABORATORY"), 2},
        {("SAMPLES","SAMPLES"), 0},     {("SAMPLES","DIAGNOSIS"), 3},   {("SAMPLES","MOLECULES"), 3},       {("SAMPLES","LABORATORY"), 3},
        {("DIAGNOSIS","SAMPLES"), 3},   {("DIAGNOSIS","DIAGNOSIS"), 0}, {("DIAGNOSIS","MOLECULES"), 3},     {("DIAGNOSIS","LABORATORY"), 4},
        {("MOLECULES","SAMPLES"), 3},   {("MOLECULES","DIAGNOSIS"), 3}, {("MOLECULES","MOLECULES"), 0},     {("MOLECULES","LABORATORY"), 3},
        {("LABORATORY","SAMPLES"), 3},  {("LABORATORY","DIAGNOSIS"), 4},{("LABORATORY","MOLECULES"), 3},    {("LABORATORY","LABORATORY"), 0}
    };

    static void Main(string[] args)
    {
        var projects = GameDataSource.GetProjects();
        var moveSteps = 0;
        List<string> commandHistory = new();

        // game loop
        while (true)
        {
            var players = GameDataSource.GetPlayers();
            var available = GameDataSource.GetAvailableMolecules();
            var sampleCloud = GameDataSource.GetSamples();

            Console.Error.WriteLine($"carried: {string.Join(" ", carriedMolecules)}");
            Console.Error.WriteLine($"expertise: {string.Join(" ", players[0].expertise)}");

            if (moveSteps > 0)
            {
                moveSteps--;
                Console.WriteLine();
                continue;
            }

            var mySamples = sampleCloud.Where(s => s.carriedBy == Carrier.Me).OrderBy(s => s.id).ToArray();
            var appropriateSamplesInCloud = sampleCloud.Where(s => s.carriedBy == Carrier.None && s.cost.All(c => c >= 0)
                            && s.cost.Select((c, i) => c - players[0].expertise[i]).Sum() <= 10
                            && s.cost.Select((c, i) => c - players[0].expertise[i] - available[i]).All(c => c <= 0)).ToArray();

            switch (players[0].target)
            {
                case "SAMPLES":
                    if (mySamples.Length + appropriateSamplesInCloud.Length < 3)
                    {
                        Connect((random.Next(100) % 2) + 1);
                        continue;
                    }
                    if (appropriateSamplesInCloud.Length > 0 || mySamples.Any(s => s.cost.All(c => c < 0)))
                    {
                        GoTo("DIAGNOSIS");
                        continue;
                    }
                    GoTo("MOLECULES");
                    break;
                case "DIAGNOSIS":
                    if (mySamples.Length < 3 && appropriateSamplesInCloud.Length > 0)
                    {
                        Connect(appropriateSamplesInCloud[0].id);
                        continue;
                    }

                    var tooComplexSample = Array.Find(mySamples, s => s.cost.Select((c, i) => c - players[0].expertise[i]).Sum() > 10 ||
                        s.cost.Select((c, i) => c - players[0].expertise[i] - available[i]).Any(c => c > 0));
                    if (tooComplexSample != default)
                    {
                        Connect(tooComplexSample.id);
                        continue;
                    }

                    var undiagnosedSample = Array.Find(mySamples, s => s.cost.All(sc => sc < 0));
                    if (undiagnosedSample != default)
                    {
                        Connect(undiagnosedSample.id);
                    }
                    else
                    {
                        GoTo("MOLECULES");
                    }
                    break;
                case "MOLECULES":
                    if (carriedMolecules.Sum() == 10)
                    {
                        GoTo("LABORATORY");
                        continue;
                    }

                    for (int i = 1; i < mySamples.Length; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            mySamples[i].expertise[j] = mySamples[i - 1].expertise[j] + (molecula2IdxDict[mySamples[i - 1].expertiseGain] == j ? 1 : 0);
                        }
                    }

                    var moleculaTaken = false;
                    for (int i = 0; i < mySamples.Length; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (mySamples[i].cost[j] - carriedMolecules[j] - mySamples[i].expertise[j] > 0 && available[j] > 0)
                            {
                                Connect(idx2MoleculaDict[j]);
                                carriedMolecules[j]++;
                                moleculaTaken = true;
                                break;
                            }
                        }
                        if (moleculaTaken) break;
                    }
                    if (moleculaTaken) continue;
                    GoTo("LABORATORY");
                    break;
                case "LABORATORY":
                    var sampleDropped = false;
                    for (int s = 0; s < mySamples.Length; s++)
                    {
                        if (mySamples[s] != default && mySamples[s].cost.Select((c, i) => c - carriedMolecules[i] - players[0].expertise[i]).All(x => x <= 0))
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                var dropQuantity = mySamples[s].cost[i] - players[0].expertise[i];
                                carriedMolecules[i] -= dropQuantity > 0 ? dropQuantity : 0;
                            }
                            players[0].expertise[molecula2IdxDict[mySamples[s].expertiseGain]]++;
                            Connect(mySamples[s].id);
                            sampleDropped = true;
                            break;
                        }
                    }
                    if (sampleDropped) continue;

                    if (mySamples.Length > 0 && (commandHistory.Count < 2 || commandHistory[^1] != "GOTO" || commandHistory[^2] != "GOTO"))
                    {
                        GoTo("MOLECULES");
                    }
                    else
                    {
                        GoTo(appropriateSamplesInCloud.Length < 3 ? "SAMPLES" : "DIAGNOSIS");
                    }
                    break;
                default:
                    GoTo("SAMPLES");
                    break;
            }

            void Connect(object value)
            {
                Console.WriteLine($"CONNECT {value}");
                commandHistory.Add("CONNECT");
            }


            void GoTo(string destination)
            {
                moveSteps = distance[(players[0].target, destination)] - 1;
                Console.WriteLine($"GOTO {destination}");
                commandHistory.Add("GOTO");
            }
        }
    }
}
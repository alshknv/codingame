using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public static class Solution
{
    const string CardOrder = "2345678910JQKA";

    static string Compare(Stack<string> stack1, Stack<string> stack2)
    {
        var card1 = stack1.Peek();
        var card2 = stack2.Peek();
        if (CardOrder.IndexOf(card1[0..^1]) > CardOrder.IndexOf(card2[0..^1])) return "1";
        else if (CardOrder.IndexOf(card1[0..^1]) < CardOrder.IndexOf(card2[0..^1])) return "2";
        else return "WAR";
    }

    static void BackToDeck(Queue<string> deck, Stack<string> stack1, Stack<string> stack2)
    {
        var b1 = stack1.ToArray();
        var b2 = stack2.ToArray();
        for (int i = b1.Length - 1; i >= 0; i--)
        {
            deck.Enqueue(b1[i]);
        }
        for (int i = b2.Length - 1; i >= 0; i--)
        {
            deck.Enqueue(b2[i]);
        }
    }

    public static string Solve(string[] input)
    {
        var n = int.Parse(input[0]);
        var deck1 = new Queue<string>();
        for (int i = 0; i < n; i++)
            deck1.Enqueue(input[i + 1]);
        var m = int.Parse(input[n + 1]);
        var deck2 = new Queue<string>();
        for (int i = 0; i < m; i++)
            deck2.Enqueue(input[i + n + 2]);
        var rounds = 0;

        var battle1 = new Stack<string>();
        var battle2 = new Stack<string>();
        while (deck1.TryDequeue(out string pl1Card) && deck2.TryDequeue(out string pl2Card))
        {
            rounds++;
            battle1.Push(pl1Card);
            battle2.Push(pl2Card);
            string outcome;
            do
            {
                outcome = Compare(battle1, battle2);
                if (outcome.Equals("WAR"))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!deck1.TryDequeue(out pl1Card) || !deck2.TryDequeue(out pl2Card)) return "PAT";
                        battle1.Push(pl1Card);
                        battle2.Push(pl2Card);
                    }
                }
            } while (outcome.Equals("WAR"));
            BackToDeck(outcome.Equals("1") ? deck1 : deck2, battle1, battle2);
            battle1.Clear();
            battle2.Clear();
        }
        var winningPlayer = deck1.Count > 0 ? 1 : 2;
        return $"{winningPlayer} {rounds}";
    }

    static void Main(string[] args)
    {
        var input = new List<string>
        {
            Console.ReadLine()
        };
        int n = int.Parse(input[^1]); // the number of cards for player 1
        for (int i = 0; i < n; i++)
        {
            input.Add(Console.ReadLine()); // the n cards of player 1
        }
        input.Add(Console.ReadLine());
        int m = int.Parse(input[^1]); // the number of cards for player 2
        for (int i = 0; i < m; i++)
        {
            input.Add(Console.ReadLine()); // the m cards of player 2
        }

        Console.WriteLine(Solve(input.ToArray()));
    }
}
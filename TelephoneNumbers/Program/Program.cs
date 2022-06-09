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
        public static int TrieCount(string[] phones)
        {
            var trie = new List<(char symbol, List<int> edges)>() {
                ('*', new List<int>())
            };
            foreach (var phone in phones)
            {
                var current = trie[0];
                foreach (var symbol in phone)
                {
                    var next = current.edges.Find(e => trie[e].symbol == symbol);
                    if (next != default)
                    {
                        current = trie[next];
                    }
                    else
                    {
                        var newNode = (symbol, new List<int>());
                        trie.Add(newNode);
                        current.edges.Add(trie.Count - 1);
                        current = newNode;
                    }
                }
            }
            return trie.Count - 1;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var phones = new string[N];
            for (int i = 0; i < N; i++)
            {
                phones[i] = Console.ReadLine();
            }
            Console.WriteLine(TrieCount(phones));
        }
    }
}
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
        private static readonly Dictionary<char, int> CharValues = new Dictionary<char, int>() {
            {'e',1}, {'a',1}, {'i',1}, {'o',1}, {'n',1}, {'r',1}, {'t',1}, {'l',1}, {'s',1}, {'u',1},
            {'d',2}, {'g',2},
            {'b',3}, {'c',3}, {'m',3}, {'p',3},
            {'f',4}, {'h',4}, {'v',4}, {'w',4}, {'y',4},
            {'k',5},
            {'j',8},{'x',8},
            {'q',10},{'z',10}
        };

        private static int WordValue(string word)
        {
            var value = 0;
            foreach (var c in word)
            {
                value += CharValues[c];
            }
            return value;
        }

        private static Hashtable GetWordHashTable(string word)
        {
            var ht = new Hashtable();
            foreach (var c in word)
            {
                if (ht.ContainsKey(c))
                {
                    ht[c] = (int)ht[c] + 1;
                }
                else
                {
                    ht.Add(c, 1);
                }
            }
            return ht;
        }

        public static string MostValuableWord(string[] words, string letters)
        {
            (string word, int value) mostValuableWord = ("", 0);
            var lettersHt = GetWordHashTable(letters);
            foreach (var word in words)
            {
                var wordHt = GetWordHashTable(word);
                var properLetters = wordHt.Count;
                foreach (DictionaryEntry wht in wordHt)
                {
                    if (!lettersHt.ContainsKey(wht.Key)) continue;
                    if ((int)wordHt[wht.Key] > (int)lettersHt[wht.Key]) continue;
                    properLetters--;
                }
                if (properLetters == 0)
                {
                    var wordValue = WordValue(word);
                    if (wordValue > mostValuableWord.value)
                    {
                        mostValuableWord = (word, wordValue);
                    }
                }
            }
            return mostValuableWord.word;
        }

        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            var words = new string[N];
            for (int i = 0; i < N; i++)
            {
                words[i] = Console.ReadLine();
            }
            string LETTERS = Console.ReadLine();
            Console.WriteLine(MostValuableWord(words, LETTERS));
        }
    }
}
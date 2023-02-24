using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
public static class Solution
{
    private static (string str, Dictionary<string, string> subs) StringWithoutQuotes(string cgxString)
    {
        var outStringBuilder = new StringBuilder();
        var substitutions = new Dictionary<string, string>();
        var subIndices = new Dictionary<string, int>();
        var subIdx = 0;
        var i = 0;
        while (i < cgxString.Length)
        {
            var quoteBegin = cgxString.IndexOf('\'', i);
            if (quoteBegin < 0)
            {
                outStringBuilder.Append(cgxString[i..]);
                break;
            }
            outStringBuilder.Append(cgxString[i..quoteBegin]);
            if (quoteBegin < 0) break;
            var quoteEnd = cgxString.IndexOf('\'', quoteBegin + 1);
            var quotedString = cgxString[quoteBegin..(quoteEnd + 1)];
            if (!subIndices.TryGetValue(quotedString, out int existingIdx))
            {
                existingIdx = subIdx++;
                subIndices.Add(quotedString, existingIdx);
                substitutions.Add($"[{existingIdx}]", quotedString);
            }
            outStringBuilder.Append('[').Append(existingIdx).Append(']');
            i = quoteEnd + 1;
        }
        return (outStringBuilder.ToString(), substitutions);
    }

    private static string FormatCgx(string cgxString)
    {
        var outStringBuilder = new StringBuilder();
        int indent = 0;
        for (int i = 0; i < cgxString.Length; i++)
        {
            switch (cgxString[i])
            {
                case '(':
                    outStringBuilder
                        .AppendLine()
                        .Append(' ', indent++ * 4)
                        .Append('(')
                        .AppendLine()
                        .Append(' ', indent * 4);
                    break;
                case ')':
                    outStringBuilder
                        .AppendLine()
                        .Append(' ', --indent * 4).Append(')');
                    break;
                case ';':
                    outStringBuilder
                        .Append(';')
                        .AppendLine()
                        .Append(' ', indent * 4);
                    break;
                default:
                    outStringBuilder.Append(cgxString[i]);
                    break;
            }
        }
        return outStringBuilder.ToString();
    }

    private static string ReturnQuotedValues(string str, Dictionary<string, string> subs)
    {
        foreach (var key in subs.Keys)
        {
            str = str.Replace(key, subs[key]);
        }
        return str;
    }

    public static string[] Solve(string[] input)
    {
        var cgxString = string.Concat(input.Select(x => x.Trim()));
        //pass 1 detect strings in quotes
        (cgxString, Dictionary<string, string> substitutions) = StringWithoutQuotes(cgxString);
        //pass 2 remove spaces left
        cgxString = cgxString.Replace(" ", "").Replace("\t", "");
        // pass 3 -format
        var formattedStr = FormatCgx(cgxString);
        // pass 4 - return values in quotes
        var stringWithQuotedValues = ReturnQuotedValues(formattedStr, substitutions);
        // split
        var finalResult = stringWithQuotedValues.Split(Environment.NewLine);
        // remove empty lines
        return finalResult.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    }

    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        var input = new string[N];
        for (int i = 0; i < N; i++)
        {
            input[i] = Console.ReadLine();
        }

        foreach (var line in Solve(input))
        {
            Console.WriteLine(line);
        }
    }
}
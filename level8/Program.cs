using System.ComponentModel;
using System.Diagnostics;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("level8.in");
string escapes = "\'\"\\";

Console.WriteLine(Part1a(input));
Console.WriteLine(Part1b(input));
Console.WriteLine(Part2(input));



int Part1a(string[] input)
{
    int a = 0, b = 0;

    foreach (var line in input)
    {
        a += line.Length;
        int printable = 0;

        for (int i = 0; i < line.Length; i++)
        {
            int x = 0;
            if (char.IsLetterOrDigit(line[i]))
            {
                printable++;
                x++;
            }
            if (line[i] == '\\' && line[i + 1] == 'x')
            {
                if (x == 1) Debugger.Break();
                x++;
                printable++;
                i += 3;
            }
            if (line[i] == '\\' && escapes.Contains(line[i + 1]))
            {
                if (x == 1) Debugger.Break();
                x++;
                printable++;
                i++;
            }

            if (x > 1) Debugger.Break();
        }

        b += printable;
    }

    return a - b;
}

int Part1b(string[] input)
{
    int a = 0, b = 0;

    foreach (var line in input)
    {
        string replaced = line[1..^1];
        replaced = Regex.Replace(replaced, @"\\x[A-Fa-f0-9]{2}", "_");
        replaced = replaced.Replace("\\\"", "\"").Replace("\\\'", "\'").Replace("\\\\", "\\");
        a += line.Length;
        b += replaced.Length;
    }

    return a - b;
}

int Part2(string[] input)
{
    int a = 0, b = 0;

    foreach (var line in input)
    {
        b += line.Length;

        string replaced = line.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\'", "\\\'");
        replaced = "\"" + replaced + "\"";
        int count = replaced.Length;
        a += count;
    }

    return a - b;
}
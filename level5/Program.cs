using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("level5.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));

int Part1(string[] input)
{
    string[] not = new[] { "ab", "cd", "pq", "xy" };
    string vowel = "aeiou";
    int count = 0;
    foreach (var line in input)
    {
        if (line.Count(t => vowel.Contains(t)) >= 3 && (line.Chunk(2).Any(t => t.Length == 2 && t[0] == t[1]) || line.Skip(1).Chunk(2).Any(t => t.Length == 2 && t[0] == t[1])) && !not.Any(t => line.Contains(t))) count++;
    }
    return count;
}

int Part2(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        if (Regex.IsMatch(line, @"([a-z]).\1|([a-z])\2"))
        {
        }
    }
    return count;
}
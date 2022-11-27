using System.Text;
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("level19.in");

List<(string Key, string Value)> replacements = new();
string sb = input.Last();
string current = null;

List<string> result = new List<string>();

foreach (var line in input[0..^2])
{
    var split = line.Split(" => ");
    replacements.Add((split[0], split[1]));
}

foreach ((string Key, string Value) in replacements)
{
    var matches = Regex.Matches(sb, Key).OfType<Match>().ToList();
    foreach (var match in matches)
    {
        current = sb.Remove(match.Index, Key.Length).Insert(match.Index, Value);
        Console.WriteLine(current);
        result.Add(current);
    }
}

Console.WriteLine();
Console.WriteLine(string.Join(Environment.NewLine, result.Distinct()));

Console.WriteLine(result.Distinct().Count());



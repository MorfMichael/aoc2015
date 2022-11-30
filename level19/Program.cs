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

replacements = replacements.Where(t => t.Value != "SiTh").OrderBy(t => t.Key).ToList(); // error

int count = 0;
while (replacements.Any(t => sb.Contains(t.Value)))
{
    foreach ((string Key, string Value) in replacements)
    {
        if (sb.Contains(Value))
        {
            count += Regex.Matches(sb, Value).Count;
            sb = sb.Replace(Value, Key);
        }
    }
}

Console.WriteLine(sb);
Console.WriteLine(count);
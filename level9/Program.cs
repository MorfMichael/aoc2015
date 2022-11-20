using System.ComponentModel;
using System.Linq;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

string[] input = File.ReadAllLines("level9.in");

Console.WriteLine(" --- LEVEL 9 --- ");

Console.WriteLine($"Part1: {Part1(input)}");
Console.WriteLine($"Part2: {Part2(input)}");

int Part1(string[] input)
{
    List<(string Start, string End, int Distance)> result = new List<(string Start, string End, int Distance)>();

    foreach (var line in input)
    {
        var split = line.Split(" = ");
        var split2 = split[0].Split(" to ");

        int distance = int.Parse(split[1]);
        string source = split2[0];
        string destination = split2[1];
        result.Add((source, destination, distance));
    }

    List<string> destinations = result.SelectMany(t => new[] { t.Start, t.End }).Distinct().ToList();

    var permutations = GetPermutations(destinations);

    Dictionary<List<string>, int> res = new Dictionary<List<string>, int>();

    foreach (var permutation in permutations)
    {
        int sum = 0;
        for (int i = 0; i < permutation.Count-1; i++)
        {
            sum += result.FirstOrDefault(t => (t.Start == permutation[i] && t.End == permutation[i + 1]) || (t.Start == permutation[i + 1] && t.End == permutation[i])).Distance;
        }

        res.Add(permutation, sum);
    }

    foreach (var r in res)
        Console.WriteLine($"{string.Join(" -> ", r.Key)}: {r.Value}");

    return res.Select(t => t.Value).Min();
}

int Part2(string[] input)
{
    List<(string Start, string End, int Distance)> result = new List<(string Start, string End, int Distance)>();

    foreach (var line in input)
    {
        var split = line.Split(" = ");
        var split2 = split[0].Split(" to ");

        int distance = int.Parse(split[1]);
        string source = split2[0];
        string destination = split2[1];
        result.Add((source, destination, distance));
    }

    List<string> destinations = result.SelectMany(t => new[] { t.Start, t.End }).Distinct().ToList();

    var permutations = GetPermutations(destinations);

    Dictionary<List<string>, int> res = new Dictionary<List<string>, int>();

    foreach (var permutation in permutations)
    {
        int sum = 0;
        for (int i = 0; i < permutation.Count - 1; i++)
        {
            sum += result.FirstOrDefault(t => (t.Start == permutation[i] && t.End == permutation[i + 1]) || (t.Start == permutation[i + 1] && t.End == permutation[i])).Distance;
        }

        res.Add(permutation, sum);
    }

    foreach (var r in res)
        Console.WriteLine($"{string.Join(" -> ", r.Key)}: {r.Value}");

    return res.Select(t => t.Value).Max();
}

List<List<string>> GetPermutations(List<string> destinations)
{
    var ds = destinations.Select(t => new List<string> { t }).ToList();

    List<List<string>> result = ds;
    for (int i = 0; i < destinations.Count - 1; i++)
    {
        result = result.SelectMany(t => ds.Where(x => !t.Contains(x.First())), (x, y) => x.Concat(y).ToList()).ToList();
    }

    return result;
}
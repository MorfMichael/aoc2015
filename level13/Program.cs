using System.Security.Cryptography.X509Certificates;

string[] input = File.ReadAllLines("level13.in");

List<(string Name, string NextTo, int Value)> arrangement = new();
List<string> names = new();

Console.WriteLine(" --- LEVEL 13 --- ");

Parse(input);

Console.WriteLine("Part1: " + Part1(input));


void Parse(string[] input, bool part2 = false)
{
    foreach (var line in input)
    {
        string replaced = line.Replace(" happiness units by sitting next to", string.Empty).Replace(" would", string.Empty).Replace("gain ", "+").Replace("lose ", "-").Replace(".", string.Empty);
        string[] split = replaced.Split(" ");
        arrangement.Add((split[0], split[2], int.Parse(split[1])));
        Console.WriteLine(split[0] + " -> " + split[2] + ": " + int.Parse(split[1]));
    }

    names = arrangement.Select(t => t.Name).Distinct().ToList();

    if (part2)
    {
        foreach (var name in names)
        {
            arrangement.Add(("Me", name, 0));
            arrangement.Add((name, "Me", 0));
        }

        names = arrangement.Select(t => t.Name).Distinct().ToList();
    }
}

int Part1(string[] input)
{
    var permutations = GetPermutations(names);
    List<(List<string> Arrangement, int Value)> order = new();

    foreach (var permutation in permutations)
    {
        int sum = 0;

        for (int i = 0; i < permutation.Count; i++)
        {
            var forward = arrangement.FirstOrDefault(t => t.Name == permutation[i] && t.NextTo == permutation[i == permutation.Count-1 ? 0 : i+1]);
            sum += forward.Value;

            var backward = arrangement.FirstOrDefault(t => t.Name == permutation[i] && t.NextTo == permutation[i == 0 ? permutation.Count-1 : i-1]);
            sum += backward.Value;

            Console.Write($" {backward.Value} {permutation[i]} {forward.Value} ");
        }
        Console.WriteLine(sum);

        order.Add((permutation, sum));
    }
    int result = order.Max(x => x.Value);
    return result;
}

List<List<string>> GetPermutations(List<string> names)
{
    List<List<string>> result = names.Select(t => new List<string> { t }).ToList();
    for (int i = 0; i < names.Count-1; i++)
    {
        result = result.SelectMany(t => names.Where(x => !t.Contains(x)).ToList(), (t,x) => t.Concat(new[] { x }).ToList()).ToList();
    }

    return result;
}
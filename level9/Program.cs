string[] input = File.ReadAllLines("sample.in");

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

    Dictionary<string, (string, int)> dict = new Dictionary<string, (string, int)>();



    return 0;
}

int Part2(string[] input)
{
    return 0;
}
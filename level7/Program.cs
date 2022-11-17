using System.Text.RegularExpressions;

Dictionary<string, string> patterns = new Dictionary<string, string>
{
    {  @"^(\d+) -> ([a-z]+)$", "INIT" },
    {  @"^([a-z0-9]+) AND ([a-z0-9]+) -> ([a-z]+)$", "AND" },
    {  @"^([a-z0-9]+) OR ([a-z0-9]+) -> ([a-z]+)$", "OR" },
    {  @"^NOT ([a-z0-9]+) -> ([a-z]+)$", "NOT" },
    {  @"^([a-z0-9]+) RSHIFT (\d+) -> ([a-z]+)$", "RSHIFT" },
    {  @"^([a-z0-9]+) LSHIFT (\d+) -> ([a-z]+)$", "LSHIFT" },
};

string[] input = File.ReadAllLines("sample.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));

int Part1(string[] input)
{
    Dictionary<string, ushort> signals = new Dictionary<string, ushort>();

    var instruction = input.FirstOrDefault();
    while (instruction != null)
    {
        if ()
    }

    foreach (var line in input)
    {
        foreach (var pattern in patterns)
        {
            var match = Regex.Match(line, pattern.Key);
            if (match.Success)
            {
                switch (pattern.Value)
                {
                    case "INIT":
                        string signal = match.Groups[2].Value;
                        if (!signals.ContainsKey(signal)) signals.Add(signal, ushort.Parse(match.Groups[1].Value));
                        break;
                    case "AND":
                        var a = match.Groups[1].Value;
                        var b = match.Groups[2].Value;
                        var dest = match.Groups[3].Value;

                }
            }
        }
    }


    return 0;
}

int Part2(string[] input)
{
    return 0;
}

/*
 * 123 -> x     ^(\d+) -> ([a-z]+)$
456 -> y
x AND y -> d        ^([a-z]+) AND ([a-z]+) -> ([a-z]+)$
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
 * */
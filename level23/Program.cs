string[] input = File.ReadAllLines("level23.in");

Dictionary<string, uint> register = new()
{
    {"a",1},
    {"b",0},
};

for (int i = 0; i < input.Length; i++)
{
    var split = input[i].Split(" ");
    string instruction = split[0];
    string value = split[1].Replace(",", "");
    int? offset = null;
    if (split.Length > 2)
        offset = int.TryParse(split[2], out var off) ? off-1 : null;
    else if (instruction == "jmp")
        offset = int.TryParse(split[1], out var off) ? off - 1 : null;

    switch (instruction)
    {
        case "hlf": register[value] /= 2; break;
        case "tpl": register[value] *= 3; break;
        case "inc": register[value] += 1; break;
        case "jmp": i += offset.Value; break;
        case "jie":
            if (register[value] % 2 == 0) i += offset.Value;
            break;
        case "jio":
            if (register[value] == 1)
                i += offset.Value;
            break;
    }
}

Console.WriteLine(register["b"]);
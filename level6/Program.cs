string[] input = File.ReadAllLines("level6.in");

List<(string instruction, (int x, int y) start, (int x, int y) end)> instructions = new();

foreach (var line in input)
{
    var split = line.Split(" ");
    string instruction = split[0] == "turn" ? split[1] : split[0];
    int[] start = split[split[0] == "turn" ? 2 : 1].Split(",").Select(int.Parse).ToArray();
    int[] end = split[^1].Split(",").Select(int.Parse).ToArray();
    instructions.Add((instruction, (start[0], start[1]), (end[0], end[1])));
}

Console.WriteLine(Part1(instructions));
Console.WriteLine(Part2(instructions));

int Part1(List<(string instruction, (int x,int y) start, (int x,int y) end)> instructions)
{
    bool[,] map = new bool[1000, 1000];

    foreach (var instruction in instructions)
    {
        for (int x = instruction.start.x; x <= instruction.end.x; x++)
        {
            for (int y = instruction.start.y; y <= instruction.end.y; y++)
            {
                if (instruction.instruction == "on") map[x, y] = true;
                else if (instruction.instruction == "off") map[x, y] = false;
                else map[x, y] = !map[x, y];
            }
        }
    }

    int count = 0;
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            if (map[x, y]) count++;
        }
    }

    return count;
}

int Part2(List<(string instruction, (int x, int y) start, (int x, int y) end)> instructions)
{
    int[,] map = new int[1000, 1000];

    foreach (var instruction in instructions)
    {
        for (int x = instruction.start.x; x <= instruction.end.x; x++)
        {
            for (int y = instruction.start.y; y <= instruction.end.y; y++)
            {
                if (instruction.instruction == "on") map[x, y] += 1;
                else if (instruction.instruction == "off") map[x, y] -= (map[x, y] >= 1 ? 1 : 0);
                else map[x, y] += 2;
            }
        }
    }

    int count = 0;
    for (int x = 0; x < map.GetLength(0); x++)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            count += map[x,y];
        }
    }

    return count;
}
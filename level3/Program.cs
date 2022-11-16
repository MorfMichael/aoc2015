using System.ComponentModel.Design;

string input = File.ReadAllText("level3.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));


int Part1(string input)
{
    (int x, int y) santa = (0, 0);

    Dictionary<(int, int), int> delivery = new Dictionary<(int, int), int>() { { (0, 0), 1 } };

    foreach (char c in input)
    {
        if (c == '^') santa.y--;
        if (c == 'v') santa.y++;
        if (c == '<') santa.x--;
        if (c == '>') santa.x++;

        if (delivery.ContainsKey(santa)) delivery[santa]++;
        else delivery.Add(santa, 1);
    }

    return delivery.Count(t => t.Value >= 1);
}

int Part2(string input)
{
    bool flag = true;
    (int x, int y) santa = (0, 0);
    (int x, int y) robo = (0, 0);

    Dictionary<(int, int), int> delivery = new Dictionary<(int, int), int>() { { (0, 0), 2 } };

    foreach (char c in input)
    {
        if (c == '^') if (flag) { santa.y--; } else robo.y--;
        if (c == 'v') if (flag) { santa.y++; } else robo.y++;
        if (c == '<') if (flag) { santa.x--; } else robo.x--;
        if (c == '>') if (flag) { santa.x++; } else robo.x++;

        if (delivery.ContainsKey(flag ? santa : robo)) delivery[flag ? santa : robo]++;
        else delivery.Add(flag ? santa : robo, 1);

        flag = !flag;
    }

    return delivery.Count(t => t.Value >= 1);
}
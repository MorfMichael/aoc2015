using System.Runtime.CompilerServices;

string input = File.ReadAllText("level1.in");


Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));

int Part1(string input)
{
    int a = 0;

    foreach (var c in input)
    {
        if (c == '(') a++;
        if (c == ')') a--;
    }
    return a;
}

int Part2(string input)
{
    int a = 0;
    int i = 1;

    foreach (var c in input)
    {
        if (c == '(') a++;
        if (c == ')') a--;

        if (a == -1) break;
        i++;
    }

    return i++;
}
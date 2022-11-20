using System.IO.Pipes;

string input = "3113322113";

//Console.WriteLine("Part1: " + Part1(input, 40));
Console.WriteLine("Part2: " + Part2(input, 40));

int Part1(string input, int count)
{
    for (int j = 0; j < count; j++)
    {
        Console.WriteLine(j);
        Console.WriteLine(input.Length);
        int c = 0;
        char p = input[0];
        string tmp = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != p)
            {
                tmp += c.ToString() + p.ToString();
                c = 0;
            }
            c++;
            p = input[i];
        }

        tmp += c.ToString() + p.ToString();
        input = tmp;
    }
    return input.Length;
}

int Part2(string input, int count)
{
    for (int j = 0; j < count; j++)
    {
        Console.WriteLine(j);
        int c = 0, i = 0;
        char p = input[0];
        string tmp = string.Empty;
        
        //todo...
        
        tmp += c.ToString() + p.ToString();
        input = tmp;
    }

    return input.Length;
}
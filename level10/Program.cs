using System.IO.Pipes;
using System.Text;

string input = "3113322113";

Console.WriteLine("Part1: " + Part1(input, 40));
Console.WriteLine("Part2: " + Part1(input, 50));

int Part1(string input, int count)
{
    for (int j = 0; j < count; j++)
    {
        Console.WriteLine(j);
        Console.WriteLine(input.Length);
        int c = 0;
        char p = input[0];
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != p)
            {
                sb.Append(c.ToString() + p.ToString());
                c = 0;
            }
            c++;
            p = input[i];
        }

        sb.Append(c.ToString() + p.ToString());
        input = sb.ToString();
    }
    return input.Length;
}
using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("level5.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));

int Part1(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        bool rule1 = Regex.Matches(line, @"[aeiou]").Count >= 3;
        bool rule2 = Regex.IsMatch(line, @"([a-z])\1");
        bool rule3 = !Regex.IsMatch(line, @"^(.*)((ab)|(cd)|(pq)|(xy))(.*)$");
        if (rule1 && rule2 && rule3) count++;
    }
    return count;
}

int Part2(string[] input)
{
    int count = 0;
    foreach (var line in input)
    {
        bool rule1 = Regex.IsMatch(line, @"([a-z]{2})(.*)\1");
        bool rule2 = Regex.IsMatch(line, @"([a-z]).\1");
        if (rule1 && rule2)
        {
            count++;
        }
    }
    return count;
}
using System.Security.AccessControl;
using System.Text;

Console.WriteLine("Part1: " + Part1("cqjxjnds"));
Console.WriteLine("Part2: " + Part1("cqjxxyzz"));

string Part1(string input)
{
    StringBuilder sb = new StringBuilder(input);

    do
    {
        //Console.WriteLine(sb.ToString());
        Update(sb, sb.Length - 1);
    }
    while (!CheckPassword(sb.ToString()));

    return sb.ToString();
}

bool CheckNot(StringBuilder sb, char ch)
{
    if (sb.ToString().Contains(ch))
    {
        int idx = sb.ToString().IndexOf(ch);
        sb[idx] = (char)(ch+1);
        for (int i = idx+1; i < sb.Length-1; i++) sb[i] = 'a';
        return true;
    }

    return false;
}

void Update(StringBuilder sb, int index)
{
    if (CheckNot(sb, 'i')) return;
    if (CheckNot(sb, 'o')) return;
    if (CheckNot(sb, 'l')) return;

    if (sb[index] < 'z') 
    {
        sb[index]++;
    }
    else
    {
        sb[index] = 'a';
        Update(sb, index - 1);
    }
}

bool CheckPassword(string sb)
{
    int count = 0, cons = 0;
    List<string> pairs = new();
    char previous = sb[0];

    for (int i = 0; i < sb.Length; i++)
    {
        if ("iol".Contains(sb[i])) return false;

        if (i > 0 && sb[i] == previous)
        {
            string pair = $"{previous}{sb[i]}";
            if (!pairs.Contains(pair)) pairs.Add(pair);
        }

        if (sb[i] == previous + 1) count++;
        else count = 0;
        if (count >= 2)
        {
            cons++;
            count = 0;
        }

        previous = sb[i];
    }

    return cons >= 1 && pairs.Count >= 2;
}
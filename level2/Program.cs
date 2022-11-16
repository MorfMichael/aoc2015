using System.Linq;
using System.Security.Cryptography.X509Certificates;

string[] input = File.ReadAllLines("level2.in");

Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));


int Part1(string[] input)
{
    //2*l*w + 2*w*h + 2*h*l
    int sum = input.Select(t => t.Split("x").Select(int.Parse).ToList()).Select(t => (w: t[0], l: t[1], h: t[2], smallest: t.OrderBy(x => x).Take(2).ToArray())).Sum(x => 2*x.l*x.w + 2*x.w*x.h + 2*x.h*x.l + x.smallest[0] * x.smallest[1] );
    return sum;
}

int Part2(string[] input)
{
    int sum = input.Select(t => t.Split("x").Select(int.Parse).ToList()).Select(t => (w: t[0], l: t[1], h: t[2], smallest: t.OrderBy(x => x).Take(2).ToArray())).Sum(x => 2*x.smallest[0] + 2 * x.smallest[1] + x.l*x.w*x.h);
    return sum;
}
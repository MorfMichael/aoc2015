using System.Runtime.InteropServices;

int input = 36000000;

Dictionary<int, int> elves = new();

int house = 0;
long count = 0;
while (count < input)
{
    var divisors = GetDivisorsMe(++house);
    foreach (var divisor in divisors)
    {
        if (elves.ContainsKey(divisor)) elves[divisor]++;
        else elves.Add(divisor, 1);
    }
    count = divisors.Sum(t => elves[t] < 50 ? (t * 11) : 0);
}

Console.WriteLine($"House {house}: {count}");

static int[] GetDivisorsMe(int n)
{
    if (n <= 0)
    {
        return null;
    }

    List<int> divisors = new List<int>();
    for (int i = 1; i <= Math.Sqrt(n); i++)
    {
        if (n % i == 0)
        {
            divisors.Add(i);
            if (i != n / i)
            {
                divisors.Add(n / i);
            }
        }
    }
    divisors.Sort();
    return divisors.ToArray();
}
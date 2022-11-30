int input = 36000000;

int house = 0;
long count = 0;
while (count < input)
{
    count = GetDivisorsMe(++house).Sum(x => x*10);
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
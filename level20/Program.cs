int input = 36000000;

int count = 1000;
Dictionary<int, int> houses = new Dictionary<int, int>();


for (int step = 0; step < 10; step++)
{
    for (int i = 1; i <= 10; i++) // elves
    {
        for (int j = 1; j <= 10; j++)
        {
            if (j % i == 0)
            {
                if (houses.ContainsKey(j)) houses[j]++;
                else houses.Add(j, 1);
            }
        }
    }
}

foreach ((int number, int value) in houses)
{
    Console.WriteLine($"House {number}: {value}");
}
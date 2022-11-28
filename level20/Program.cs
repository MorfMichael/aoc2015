int input = 36000000;

Dictionary<int, int> houses = new Dictionary<int, int>();

//1000441
//831601
int house = 600000;
int count = 0;
while (count < input)
{
    count = 0;
    int elve = 1;
    while (elve <= house)
    {
        if (house % elve == 0) count += elve * 10;
        elve++;
    }

    //count = Enumerable.Range(1, house).Where(t => house % t == 0).Sum(x => x*10);
    //Console.WriteLine($"House {house}: {count}");
    house++;
}

Console.WriteLine(house);

//for (int i = 1; i <= 10; i++)
//{
//    for (int j = 1; j <= 10; j++)
//    {
//        if (j % i == 0)
//        {
//            if (houses.ContainsKey(j)) houses[j] += i * 10;
//            else houses.Add(j, 10);
//        }
//    }
//}

//foreach ((int number, int value) in houses)
//{
//    Console.WriteLine($"House {number}: {value}");
//}
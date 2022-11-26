List<int> input = File.ReadAllLines("level17.in").Select(int.Parse).OrderBy(x => x).ToList();

int count = 0;
Calculate(0, 150);
Console.WriteLine(count);

void Calculate(int start, int check)
{
    if (check > 0)
    {
        for (int i = start; i < input.Count; i++)
        {
            if (input[i] > check) return;
            check -= input[i];
            if (check == 0) count++;
            Calculate(i + 1, check);
            check += input[i];
        }
    }
}

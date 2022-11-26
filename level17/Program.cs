List<int> input = File.ReadAllLines("level17.in").Select(int.Parse).OrderBy(x => x).ToList();

int count = 0;
List<int> containers = new List<int>();
Calculate(0, 150, 0);
int min = containers.Min();
int minCount = containers.Count(x => x == min);
Console.WriteLine(minCount);

void Calculate(int start, int check, int depth)
{
    if (check > 0)
    {
        for (int i = start; i < input.Count; i++)
        {
            if (input[i] > check) return;
            check -= input[i];
            if (check == 0)
            { 
                count++;
                containers.Add(depth);
            };
            Calculate(i + 1, check, depth+1);
            check += input[i];
        }
    }
}

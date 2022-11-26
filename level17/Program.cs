List<Container> input = File.ReadAllLines("sample.in").Select(t => new Container(int.Parse(t))).ToList();

List<List<Container>> result = new List<List<Container>>();
Permutate(new List<Container>(), input, result, 25);
foreach (var permutation in result)
{
    Console.WriteLine(string.Join(",", permutation.Select(t => t.Size)));
}

void Permutate(IEnumerable<Container> current, IEnumerable<Container> source, List<List<Container>> result, int check)
{
    foreach (var container in source)
    {
        var newcurrent = current.Append(container);

        if (newcurrent.Sum(x => x.Size) == check && !result.Any(t => newcurrent.All(x => t.Contains(x))))
        {
            result.Add(newcurrent.ToList());
        }
        else if (newcurrent.Sum(x => x.Size) < check)
        {
            Permutate(current.Append(container), source.Where(t => t != container), result, check);
        }
    }
}

class Container
{
    public int Size { get; set; }
    public Container(int size)
    {
        Size = size;
    }

    public override string ToString() => Size.ToString();
}
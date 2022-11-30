int[] input = File.ReadAllLines("level24.in").Select(int.Parse).OrderByDescending(x => x).ToArray();

int size = input.Sum() / 3;

List<(Group a, Group b, Group c)> bla = new();

Permutationa(new Group(), new Group(), new Group(), input, size, bla);

var best = bla.OrderBy(t => t.a.Count).ThenBy(t => t.a.Multiply()).FirstOrDefault();

Console.WriteLine(best.a.Multiply());
//List<Group> permutations = new();
//Permutations(new Group(), input, size, permutations);
//var combinations = Combinations(permutations).ToList();
//var combination = combinations.OrderBy(t => t.a.Count).ThenBy(t => t.a.Multiply()).FirstOrDefault();
//Console.WriteLine(permutations.Count);
//Console.WriteLine(combinations.Count);
//Console.WriteLine(combination.a.Multiply());

void Permutations(Group current, int[] source, int check, List<Group> result)
{
    foreach (var s in source)
    {
        var newc = new Group(current.Append(s));
        if (newc.Sum() > check) continue;
        if (result.Any(x => x.All(newc.Contains))) continue;
        if (newc.Sum() == check)
        {
            //Console.WriteLine(newc);
            result.Add(newc);
            continue;
        }
        else
        {
            Permutations(newc, source.Where(t => t != s).ToArray(), check, result);
        }
    }
}
IEnumerable<(Group a, Group b, Group c)> Combinations(List<Group> permutations)
{
    foreach (var a in permutations)
    {
        foreach (var b in permutations)
        {
            foreach (var c in permutations)
            {
                if (a.Any(b.Contains) || a.Any(c.Contains) ||
                    b.Any(a.Contains) || b.Any(c.Contains) ||
                    c.Any(a.Contains) || c.Any(b.Contains)) continue;
                yield return (a, b, c);
            }
        }
    }
}
void Permutationa(Group a, Group b, Group c, int[] source, int check, List<(Group a, Group b, Group c)> result)
{
    foreach (var s in source)
    {
        if (a.Contains(s) || b.Contains(s) || c.Contains(s)) continue;

        var newa = new Group(a);
        var newb = new Group(b);
        var newc = new Group(c);

        if (a.Sum() + s <= check) newa = new Group(a.Append(s));
        else if (b.Sum() + s <= check) newb = new Group(b.Append(s));
        else if (c.Sum() + s <= check) newc = new Group(c.Append(s));
        else continue;

        if (newa.Sum() == check && newb.Sum() == check && newc.Sum() == check && !result.Any(t => t.a.All(newa.Contains) && t.b.All(newb.Contains) && t.c.All(newc.Contains)))
        {
            Console.WriteLine(newa + "\t\t" + newb + "\t\t" + newc);
            result.Add((newa, newb, newc));
            continue;
        }
        else
        {
            Permutationa(newa, newb, newc, source.Where(t => t != s).ToArray(), check, result);
        }
    }
}

void Permutationsa(List<int> current, int[] source, int check, List<List<int>> result)
{
    foreach (var s in source)
    {
        var n = current.Append(s).ToList();

        if (n.Sum() > check) continue;
        if (result.Any(t => t.ToString() == n.ToString())) continue;

        if (n.Sum() == check)
        {
            result.Add(n);
            continue;
        }

        Permutationsa(n, source.Where(t => t != s).ToArray(), check, result);
    }
}

class Group : List<int>
{
    public Group() : base() { }
    public Group(IEnumerable<int> collection) : base(collection) { }

    public int Multiply()
    {
        return this.Aggregate((p, c) => p * c);
    }

    public Group ToList() => this.ToList();

    public override string ToString() => string.Join(",", this.OrderByDescending(t => t));
}
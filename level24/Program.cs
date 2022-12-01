using System.Collections.Specialized;
using System.Reflection.Metadata.Ecma335;

int[] input = File.ReadAllLines("level24.in").Select(int.Parse).OrderByDescending(x => x).ToArray();

// part 1
//int size = input.Sum() / 3;
// part 2
int size = input.Sum() / 4;
int count = int.MaxValue;

List<List<long>> permutations = Permutation(new(), input, size).ToList();

var qe = permutations.OrderBy(t => t.Count).ThenBy(t => t.Aggregate((p,c) => p*c)).First();
long result = qe.Aggregate((p, c) => p * c);

Console.WriteLine(result);

IEnumerable<List<long>> Permutation(List<long> current, int[] source, long check)
{
    if (current.Count > count) yield break;

    long remaining = check - current.Sum();
    for (int i = 0; i < source.Length; i++)
    {
        var s = source[i];
        if (s > remaining) continue;
        var c = current.ToList();
        c.Add(s);

        if (s == remaining)
        {
            if (c.Count < count) count = c.Count;
            yield return c;
        }
        else
        {
            foreach (var child in Permutation(c, source.Skip(i + 1).ToArray(), check))
            {
                yield return child;
            }
        }
    }
}
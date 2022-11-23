using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("level16.in");

List<Aunt> aunts = input.Select(Aunt.Parse).ToList();

List<(string property, int value)> checks = new() {
("children", 3),
("cats", 7),
("samoyeds", 2),
("pomeranians", 3),
("akitas", 0),
("vizslas", 0),
("goldfish", 5),
("trees", 3),
("cars", 2),
("perfumes", 1),
};


Aunt result = null;

foreach (var aunt in aunts)
{
    if (aunt.Check(checks))
    {
        result = aunt;
        break;
    }
}

if (result != null)
    Console.WriteLine(result.Id);
else
    Console.WriteLine("not found!");


class Aunt
{
    public static Aunt Parse(string line) => new Aunt(line);

    public Aunt(string line)
    {
        var matches = Regex.Matches(line, @"[A-z]+(.*?)\d+").OfType<Match>().ToList();
        var entries = matches.Select(t => t.Value.Replace(":", string.Empty).Split(" ")).Select(t => new { Key = t[0], Value = int.Parse(t[1]) }).ToList();
        entries.ForEach(x => Properties.Add(x.Key, x.Value));
    }

    public int Id => Properties["Sue"];

    public Dictionary<string, int> Properties { get; set; } = new Dictionary<string, int>();

    public bool Check(List<(string property, int value)> checks)
    {
        List<(string property, bool correct)> result = new();
        foreach (var check in checks)
        {
            if (Properties.ContainsKey(check.property))
                result.Add((check.property, Properties[check.property] == check.value));
        }

        return result.All(x => x.correct);
    }
}

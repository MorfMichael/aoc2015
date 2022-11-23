using System.Linq;

string[] input = File.ReadAllLines("level15.in");

List<Ingredient> ingredients = new();

Console.WriteLine(" --- LEVEL 15 --- ");

Parse(input);

Console.WriteLine("Part1: " + Part1());
//Console.WriteLine("Part1: " + Part2());


void Parse(string[] input)
{
    ingredients = input.Select(Ingredient.Parse).ToList();
}

int Part1()
{
    var permutations = GetPermutations();

    List<string> properties = ingredients.SelectMany(t => t.Properties).Select(t => t.Key).Distinct().ToList();
    
    List<int> scores = new();

    foreach (var permutation in permutations)
    {
        int permsum = 1;
        foreach (var property in properties.Where(t => t != "calories"))
        {
            int sum = 0;
            for (int i = 0; i < permutation.Count; i++)
            {
                sum += permutation[i] * ingredients[i].Properties[property];
            }
            permsum *= (sum < 0 ? 0 : sum);
        }
        scores.Add(permsum);
    }

    return scores.Max();
}

int Part2()
{
    return 0;
}

List<List<int>> GetPermutations()
{
    List<List<int>> result = new();

    for (int a = 1; a <= 100; a++)
    {
        for (int b = 1; b <= 100; b++)
        {
            for (int c = 1; c <= 100; c++)
            {
                for (int d = 1; d <= 100; d++)
                {
                    if (a + b + c + d == 100) result.Add(new List<int> { a, b, c, d });
                }
            }
        }
    }

    return result;
}

class Ingredient
{
    public static Ingredient Parse(string line) => new Ingredient(line);

    public static int Score(Ingredient a, int amounta, Ingredient b, int amountb)
    {
        int sum = 0;

        var zipped = a.Properties.Zip(b.Properties);
        Dictionary<string, int> result = new Dictionary<string, int>();
        foreach (var zip in zipped)
        {
            if (zip.First.Key == "calories") continue;

            int ab = amounta * zip.First.Value + amountb * zip.Second.Value;
            result.Add(zip.First.Key, ab < 0 ? 0 : ab);
        }

        foreach (var res in result)
        {
            sum *= res.Value;
        }

        return sum;
    }

    public Ingredient(string line)
    {
        var split = line.Split(": ");
        Name = split[0];
        var properties = split[1].Split(", ");

        foreach (var prop in properties)
        {
            var propsplit = prop.Split(" ");
            Properties.Add(propsplit[0], int.Parse(propsplit[1]));
        }
    }
    
    public string Name { get; set; }

    public Dictionary<string, int> Properties { get; set; } = new();
}
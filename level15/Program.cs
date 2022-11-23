using System.Security.Cryptography.X509Certificates;

string[] input = File.ReadAllLines("sample.in");

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
    var permutations = GetPermutations(ingredients.Count);

    return 0;
}

int Part2()
{
    return 0;
}

List<List<int>> GetPermutations(int count)
{
    List<List<int>> result = new();

    var possible = Enumerable.Range(0,count).Select(t => Enumerable.Range(1, 100).ToList()).ToList();

    foreach (var p in possible)
    {
        
    }

    return possible;
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
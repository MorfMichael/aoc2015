using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Schema;

string input = File.ReadAllText("level12.in");

Console.WriteLine(" --- LEVEL 12 --- ");

Console.WriteLine("Part1: " + Part1(input));
Console.WriteLine("Part2: " + Part2(input));

int Part1(string input)
{
    int sum = 0;

    var node = JsonNode.Parse(input);
    sum += CheckNode(node);
    return sum;
}

int Part2(string input)
{
    int sum = 0;

    var node = JsonNode.Parse(input);
    sum += CheckNode(node);
    return sum;
}

int CheckNode(JsonNode node)
{
    int sum = 0;
    try
    {
        int value = node.GetValue<JsonElement>().GetInt32();
        //Console.WriteLine(value);
        return value;
    }
    catch { }

    if (node.GetType() == typeof(JsonArray))
    {
        foreach (var entry in node.AsArray())
        {
            sum += CheckNode(entry);
        }
    }
    else if (node.GetType() == typeof(JsonObject))
    {
        var obj = node.AsObject();

        if (obj.Any(t => GetValue(t.Value) == "red")) return 0;

        foreach ((string key, JsonNode value) in obj)
        {
            if (int.TryParse(key, out var parsed)) sum += parsed;
            sum += CheckNode(value);
        }
    }

    return sum;
}

string GetValue(JsonNode node)
{
    try
    {
        return node.GetValue<string>();
    }
    catch
    { return null; }
}
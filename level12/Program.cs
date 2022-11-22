using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

string input = File.ReadAllText("level12.in");

Console.WriteLine(" --- LEVEL 12 --- ");

Console.WriteLine("Part1: " + Part1(input));
Console.WriteLine("Part2: " + Part2(input));

int Part1(string input)
{
    int sum = 0;

    var node = JsonNode.Parse(input);
    while (node != null)
    {
        if (node.GetType() == typeof(JsonArray))
        {
            foreach (var entry in node.AsArray())
            {
                node = entry;
            }
        }
        else if (node.GetType() == typeof(JsonObject))
        {
            var obj = node.AsObject();
            foreach (var entry in obj)
            {
                if (int.TryParse(entry.Key, out var parsed)) sum += parsed; ;

            }
        }
    }

    return 0;
}

int Part2(string input)
{
    return 0;
}

using System.Buffers;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

string[] input = File.ReadAllLines("level7.in");

//Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));


int Part1(string[] input)
{
    Dictionary<string, ushort> signals = new Dictionary<string, ushort>();
    List<Operation> operations = input.Select(Operation.Parse).ToList();

    Operation operation = operations.FirstOrDefault(x => !x.Finished);
    while (operation != null)
    {
        operation.Operate(signals, operations);
        operation = operations.FirstOrDefault(x => !x.Finished);
    }

    Console.WriteLine(string.Join(Environment.NewLine, signals.OrderBy(t => t.Key).Select(t => $"{t.Key}: {t.Value}")));

    return signals["a"];
}

int Part2(string[] input)
{
    Dictionary<string, ushort> signals = new Dictionary<string, ushort>();
    signals.Add("b", 46065);
    List<Operation> operations = input.Select(Operation.Parse).ToList();
    //operations.Insert(0, Operation.Parse("a -> b"));

    Operation operation = operations.FirstOrDefault(x => !x.Finished);
    while (operation != null)
    {
        operation.Operate(signals, operations);
        operation = operations.FirstOrDefault(x => !x.Finished);
    }

    Console.WriteLine(string.Join(Environment.NewLine, signals.OrderBy(t => t.Key).Select(t => $"{t.Key}: {t.Value}")));


    return signals["a"];
}

/*
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
123 -> x
456 -> y
 * */
public abstract class Operation
{
    public string Source { get; set; }

    public string Destination { get; set; }

    public bool Finished { get; set; }

    public Operation(string line)
    {
        var split = line.Split(" -> ");
        Source = split[0];
        Destination = split[1];
    }

    public static Operation Parse(string line)
    {
        if (line.Contains("AND")) return new AND(line);
        if (line.Contains("OR")) return new OR(line);
        if (line.Contains("NOT")) return new NOT(line);
        if (line.Contains("RSHIFT")) return new RSHIFT(line);
        if (line.Contains("LSHIFT")) return new LSHIFT(line);

        return new INIT(line);
    }

    public void Operate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        if (signals.ContainsKey(Destination))
        {
            Finished = true;
            return;
        }

        InternalOperate(signals, operations);

        Finished = true;
    }

    public abstract void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations);

    protected ushort Find(string signal, Dictionary<string, ushort> signals, List<Operation> operations)
    {
        if (!signals.ContainsKey(signal))
        {
            var operation = operations.FirstOrDefault(t => t.Destination == signal);
            operation?.Operate(signals, operations);
        }

        return signals[signal];
    }
}

public class INIT : Operation
{
    public INIT(string line) : base(line) { }

    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        ushort signal = ushort.TryParse(Source, out var s) ? s : Find(Source, signals, operations);

        signals.Add(Destination, signal);
    }
}

public class AND : Operation
{
    public AND(string line) : base(line) { }

    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        var split = Source.Split(" AND ");
        ushort left = ushort.TryParse(split[0], out var l) ? l : Find(split[0], signals, operations);
        ushort right = ushort.TryParse(split[1], out var r) ? r : Find(split[1], signals, operations);

        signals.Add(Destination, (ushort)(left & right));
    }
}

public class OR : Operation
{
    public OR(string line) : base(line)
    {

    }
    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        var split = Source.Split(" OR ");
        var left = ushort.TryParse(split[0], out var l) ? l : Find(split[0], signals, operations);
        var right = ushort.TryParse(split[1], out var r) ? r : Find(split[1], signals, operations);

        signals.Add(Destination, (ushort)(left | right));
    }
}

public class NOT : Operation
{
    public NOT(string line) : base(line) { }

    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        var split = Source.Replace("NOT ", string.Empty);
        var right = ushort.TryParse(split, out var r) ? r : Find(split, signals, operations);

        signals.Add(Destination, (ushort)(~right));
    }
}

public class LSHIFT : Operation
{
    public LSHIFT(string line) : base(line) { }

    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        var split = Source.Split(" LSHIFT ");
        var left = ushort.TryParse(split[0], out var l) ? l : Find(split[0], signals, operations);
        var right = ushort.Parse(split[1]);

        signals.Add(Destination, (ushort)(left << right));
    }
}

public class RSHIFT : Operation
{
    public RSHIFT(string line) : base(line) { }

    public override void InternalOperate(Dictionary<string, ushort> signals, List<Operation> operations)
    {
        var split = Source.Split(" RSHIFT ");
        var left = ushort.TryParse(split[0], out var l) ? l : Find(split[0], signals, operations);
        var right = ushort.Parse(split[1]);

        signals.Add(Destination, (ushort)(left >> right));
    }
}
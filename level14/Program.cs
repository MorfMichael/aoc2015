Console.WriteLine(" --- LEVEL 14 --- ");

string[] input = File.ReadAllLines("level14.in");
//input = "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.\r\nDancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.".Split(Environment.NewLine);
List<Comet> comets = new();

Parse(input);

Console.WriteLine("Part1: " + Part1(2503));

Console.WriteLine(string.Join(Environment.NewLine, comets.Select(t => $"{t.Name}: {t.Distance} {t.Rest}")));

void Parse(string[] input)
{
    foreach (var line in input)
    {
        string replaced = line.Replace(" can fly", string.Empty).Replace(" km/s for", string.Empty).Replace(" seconds, but then must rest for", string.Empty).Replace(" seconds.", string.Empty);
        var split = replaced.Split(" ");
        comets.Add(new Comet(split[0], int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3])));
    }
}

int Part1(int seconds)
{
    for (int i = 1; i <= seconds; i++)
    {
        foreach (var comet in comets)
        {
            comet.FlyOrRest();
        }
    }

    return comets.Max(x => x.Distance);
}

class Comet
{
    public Comet(string name, int speed, int speedTime, int restTime)
    {
        Name = name;
        Speed = speed;
        SpeedTime = speedTime;
        RestTime = restTime;

        Counter = speedTime;
        Rest = false;
    }

    public string Name { get; set; }

    public int Speed { get; set; }

    public int SpeedTime { get; set; }

    public int RestTime { get; set; }

    public int Counter { get; set; }

    public int Distance { get; set; }

    public bool Rest { get; set; }

    public int Points { get; set; }

    public int Calculate(int seconds)
    {
        int count = seconds / (SpeedTime + RestTime);
        int spare = seconds - count * (SpeedTime + RestTime);
        return (count * Speed * SpeedTime) + (spare < SpeedTime ? spare * Speed : SpeedTime * Speed);
    }

    public void FlyOrRest()
    {
        if (Counter == 0)
        {
            if (Rest) Counter = SpeedTime;
            else Counter = RestTime;

            Rest = !Rest;
        }

        if (!Rest) Distance += Speed;
        Counter--;
    }

    public override string ToString() => $"{Name}: {Distance} {(Rest ? "resting" : "flying")}";
}
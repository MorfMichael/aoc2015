using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

List<Weapon> weapons = new()
{
    new Weapon("Dagger",8,4),
    new Weapon("Shortsword", 10, 5),
    new Weapon("Warhammer", 25, 6),
    new Weapon("Longsword", 40, 7),
    new Weapon("Greataxe", 74, 8),
};

List<Armor> armors = new()
{
    new Armor("Leather", 13, 1),
    new Armor("Chainmail", 31, 2),
    new Armor("Splintmail", 53, 3),
    new Armor("Bandedmail", 75, 4),
    new Armor("Platemail", 102, 5),
};

List<Ring> rings = new()
{
    new Ring("Damage+1", 25, 1, 0),
    new Ring("Damage+2", 50, 2, 0),
    new Ring("Damage+3", 100, 3, 0),
    new Ring("Defense+1", 20, 0, 1),
    new Ring("Defense+2", 40, 0, 2),
    new Ring("Defense+3", 80, 0, 3)
};

var attachments = weapons.Concat<Attachment>(armors).Concat(rings).ToList();

List<List<Attachment>> combinations = Permutate(new List<Attachment>(), attachments).ToList();

var result = combinations.Select(t => (Me: Player.Create("Me", 100, t), Boss: Player.Create("Boss", 109, 8, 2), Attachments: t)).Select(t => (t.Me, t.Boss, t.Attachments, Cost: Play(t.Me, t.Boss))).Where(t => t.Cost >= 0).ToList();

Console.WriteLine(result.Max(x => x.Cost));

IEnumerable<List<Attachment>> Permutate(List<Attachment> current, List<Attachment> source)
{
    foreach (var s in source)
    {
        if (current.Any(x => x.Type == AttachmentType.Weapon) && s.Type == AttachmentType.Weapon) continue;
        if (current.Any(x => x.Type == AttachmentType.Armor) && s.Type == AttachmentType.Armor) continue;
        if (current.Count(x => x.Type == AttachmentType.Ring) >= 2 && s.Type == AttachmentType.Ring) continue;

        var res = current.Append(s).ToList();

        int weapons = res.Count(x => x.Type == AttachmentType.Weapon);
        int armors = res.Count(x => x.Type == AttachmentType.Armor);
        int rings = res.Count(x => x.Type == AttachmentType.Ring);

        if (weapons == 1 && armors >= 0 && armors <= 1 && rings >= 0 && rings <= 2)
            yield return res;

        foreach (var child in Permutate(res, source.Where(t => t != s).ToList()))
        {
            yield return child;
        }
    }
}


int Play(Player a, Player b)
{
    bool change = true;
    while (true)
    {
        if (change) a.Attack(b);
        else b.Attack(a);

        change = !change;

        if (a.IsDead) return a.Cost;
        if (b.IsDead) return -1;
    }
}

IEnumerable<List<Attachment>> GetCombinations(List<Attachment> current, List<Attachment> source)
{
    foreach (var s in source)
    {
        if (current.Count(x => x.Type == AttachmentType.Ring) >= 2 && s.Type == AttachmentType.Ring) continue;
        if (current.Any(t => t.Type == AttachmentType.Armor) && s.Type == AttachmentType.Armor) continue;

        var c = current.Append(s).ToList();
        yield return c;

        foreach (var child in GetCombinations(c, source.Where(t => t != s).ToList()))
        {
            yield return child;
        }
    }

}

class Player
{
    public static Player Create(string name, int points, int damage, int armor) => new Player(name, points, damage, armor);
    public static Player Create(string name, int points, List<Attachment> attachments)
    {
        var player = new Player(name, points, 0, 0);
        player.Use(attachments);
        return player;
    }

    public Player(string name, int points, int damage, int armor)
    {
        Attachments = new List<Attachment>();
        Name = name;
        Points = points;
        Armor = armor;
        Damage = damage;
    }

    public bool IsDead => Points <= 0;

    public string Name { get; set; }

    public int Points { get; set; }

    public int Armor { get; set; }

    public int Damage { get; set; }

    public int Cost => Attachments.Sum(x => x.Cost);

    public List<Attachment> Attachments { get; set; } = new List<Attachment>();

    public void Use(IEnumerable<Attachment> attachments)
    {
        foreach (var attachment in attachments)
        {
            Attachments.Add(attachment);
            Armor += attachment.Armor;
            Damage += attachment.Damage;
        }
    }

    public void Attack(Player player)
    {
        int damage = Damage - player.Armor;
        player.Points -= damage > 0 ? damage : 1;
        //Console.WriteLine($"{Name} attacks {player.Name}: {Damage} - {player.Armor} = {(damage > 0 ? damage : 1)} | {player.Points}");
    }

    public override string ToString() => IsDead ? Name + " is dead!" : $"{Name}: {Points}";
}

enum AttachmentType
{
    Weapon,
    Armor,
    Ring
}

abstract class Attachment
{
    public Attachment(string name, int cost, int damage, int armor)
    {
        Name = name;
        Cost = cost;
        Damage = damage;
        Armor = armor;
    }

    public AttachmentType Type { get; set; }

    public string Name { get; set; }

    public int Cost { get; set; }

    public int Damage { get; set; }

    public int Armor { get; set; }
}

class Weapon : Attachment
{
    public Weapon(string name, int cost, int damage) : base(name, cost, damage, 0)
    {
        Type = AttachmentType.Weapon;
    }
}

class Armor : Attachment
{
    public Armor(string name, int cost, int armor) : base(name, cost, 0, armor)
    {
        Type = AttachmentType.Armor;
    }
}

class Ring : Attachment
{
    public Ring(string name, int cost, int damage, int armor) : base(name, cost, damage, armor)
    {
        Type = AttachmentType.Ring;
    }
}
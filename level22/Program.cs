var start = new Game
{
    Boss = 71,
    Damage = 10,
    Player = 50,
    Mana = 500
};

Stack<Game> games = new();
games.Push(start);
int min = int.MaxValue;

while (games.Any())
{
    var game = games.Pop();

    if (game.Cost > min) continue;

    game.Effects();
    if (game.Boss <= 0)
    {
        if (game.Cost < min) min = game.Cost;
        continue;
    }

    List<Game> newgames = new();
    var spells = GetSpells(game);
    foreach (var spell in spells)
    {
        int cost = SpellCost(spell);
        var copy = game.Generate();
        copy.Cast(spell, cost);
        if (copy.Boss <= 0)
        {
            if (copy.Cost < min) min = copy.Cost;
            continue;
        }
        newgames.Add(copy);
    }

    foreach (var newg in newgames)
    {
        newg.Effects();
        if (newg.Boss <= 0)
        {
            if (newg.Cost < min) min = newg.Cost;
            continue;
        }

        newg.DoDamage();
        if (newg.Player <= 0) continue;

        games.Push(newg);
    }
}

Console.WriteLine(min);


Spell[] GetSpells(Game game)
{
    var spells = Enum.GetValues(typeof(Spell)).OfType<Spell>();
    if (game.Shield > 0) spells = spells.Where(t => t != Spell.Shield);
    if (game.Poison > 0) spells = spells.Where(t => t != Spell.Poison);
    if (game.Recharge > 0) spells = spells.Where(t => t != Spell.Recharge);
    return spells.Where(t => SpellCost(t) <= game.Mana).ToArray();
}

void Log(string message)
{
    //Console.WriteLine(message);
    //Console.ReadKey();
}

int SpellCost(Spell spell) => spell switch
{
    Spell.Missile => 53,
    Spell.Drain => 73,
    Spell.Shield => 113,
    Spell.Poison => 173,
    Spell.Recharge => 229,
    _ => 0
};



enum Spell
{
    Missile,
    Drain,
    Shield,
    Poison,
    Recharge
}
class Game
{
    public int Boss { get; set; }
    public int Damage { get; set; }

    public int Player { get; set; }

    public int Mana { get; set; }
    public int Armor => Shield > 0 ? 7 : 0;

    public int Shield { get; set; }
    public int Poison { get; set; }
    public int Recharge { get; set; }

    public int Cost { get; set; }

    public void Effects()
    {
        if (Shield > 0)
        {
            Log("EFFECT: Shield | " + Shield + " | Armor: " + Armor);
            Shield--;
        }

        if (Poison > 0)
        {
            Log("EFFECT: Poison | " + Poison + " | Boss: " + Boss + " -> " + (Boss - 3));
            Boss -= 3;
            Poison--;
        }

        if (Recharge > 0)
        {
            Log("EFFECT: Recharge | " + Recharge + " | Mana: " + Mana + " -> " + (Mana + 101));
            Mana += 101;
            Recharge--;
        }
    }

    public void Cast(Spell spell, int cost)
    {
        Log("CAST: " + spell.ToString() + ": " + cost + " | " + Mana.ToString() + " -> " + (Mana - cost));
        Cost += cost;
        Mana -= cost;

        switch (spell)
        {
            case Spell.Missile:
                Log("Missile: " + Boss + " - 4 = " + (Boss - 4));
                Boss -= 4;
                break;
            case Spell.Drain:
                Log("Drain:" + Environment.NewLine + Boss + " - 2 = " + (Boss - 2) + Environment.NewLine + Player + " + 2 = " + (Player + 2));
                Boss -= 2;
                Player += 2;
                break;
            case Spell.Shield:
                Shield = 6;
                break;
            case Spell.Poison:
                Poison = 6;
                break;
            case Spell.Recharge:
                Recharge = 5;
                break;
        }
    }

    public void DoDamage()
    {
        int damage = Damage - Armor;
        damage = damage > 0 ? damage : 1;
        Log("Boss damage: " + Player + " - " + damage + " = " + (Player - damage));
        Player -= damage;
    }

    public Game Generate() => new Game
    {
        Boss = Boss,
        Damage = Damage,
        Player = Player,
        Mana = Mana,
        Shield = Shield,
        Poison = Poison,
        Recharge = Recharge,
        Cost = Cost
    };

    public override string ToString() => $"Boss: {Boss}, Player: {Player}, Mana: {Mana}, Armor: {Armor}, Shield: {Shield}, Poison: {Poison}, Recharge: {Recharge}";

    void Log(string message)
    {
        //Console.WriteLine(message);
        //Console.ReadKey();
    }
}
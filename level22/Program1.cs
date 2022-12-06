State game = new()
{
    WPoints = 50,
    WMana = 500,
    BPoints = 71,
    BDamage = 10,
    Wizard = false,
};

List<State> lost = new List<State>();
List<State> won = new List<State>();
List<State> other = new List<State>();

Stack<State> queue = new();
queue.Push(game.Generate(Spell.Recharge));
queue.Push(game.Generate(Spell.Poison));
queue.Push(game.Generate(Spell.Shield));
queue.Push(game.Generate(Spell.Drain));
queue.Push(game.Generate(Spell.Missile));

int min = int.MaxValue;
int count = 0;
while (queue.Any())
{
    count++;
    var current = queue.Pop();

    if (current != default)
    {
        //Console.WriteLine(current);
        //Console.WriteLine(current.Spell);
        //Console.ReadKey();
        int spellCost = SpellCost(current.Spell);
        if (spellCost > min || current.Cost > min) continue;
        if (spellCost > current.WMana) continue;

        ProcessEffects(current);
        if (current.BPoints <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("win with " + current.Cost);
            Console.ResetColor();
            won.Add(current);
            if (current.Cost < min) min = current.Cost;
            continue;
        }

        if (current.Wizard) // wizard
        {
            // cast new spell
            CastSpell(current);
            if (current.BPoints <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("win with " + current.Cost);
                Console.ResetColor();
                won.Add(current);
                if (current.Cost < min) min = current.Cost;
                continue;
            }
        }
        else // boss
        {
            int damage = current.BDamage - current.WArmor;
            damage = damage > 0 ? damage : 1;
            current.WPoints -= damage;

            if (current.WPoints <= 0) continue;
        }

        var spells = PossibleSpells(current);
        //var spells = new[] { Spell.Missile };
        foreach (var spell in spells)
        {
            queue.Push(current.Generate(spell));
        }
    }
}

Console.WriteLine(min);

int SpellCost(Spell spell) => spell switch
{
    Spell.Missile => 53,
    Spell.Drain => 73,
    Spell.Shield => 113,
    Spell.Poison => 173,
    Spell.Recharge => 229,
    _ => 0,
};

void CastSpell(State state)
{
    int cost = SpellCost(state.Spell);
    state.Cost += cost;
    state.WMana -= cost;
    if (state.Spell == Spell.Missile)
    {
        state.BPoints -= 4;
    }
    else if (state.Spell == Spell.Drain)
    {
        state.BPoints -= 2;
        state.WPoints += 2;
    }
    else if (state.Spell == Spell.Shield)
    {
        state.WShield = 6;
        state.WArmor = 7;
    }
    else if (state.Spell == Spell.Poison)
    {
        state.WPoison = 6;
    }
    else if (state.Spell == Spell.Recharge)
    {
        state.WRecharge = 5;
    }
}

void ProcessEffects(State state)
{
    if (state.WShield > 0)
    {
        state.WShield--;
        if (state.WShield == 0) state.WArmor = 0;
    }

    if (state.WPoison > 0)
    {
        state.BPoints -= 3;
        state.WPoison--;
    }

    if (state.WRecharge > 0)
    {
        state.WMana += 101;
        state.WRecharge--;
    }
}

Spell[] PossibleSpells(State state)
{
    var qry = Enum.GetValues(typeof(Spell)).OfType<Spell>();

    if (state.WShield > 0) qry = qry.Where(t => t != Spell.Shield);
    if (state.WPoison > 0) qry = qry.Where(t => t != Spell.Poison);
    if (state.WRecharge > 0) qry = qry.Where(t => t != Spell.Recharge);

    return qry.ToArray();
}

enum Spell
{
    Recharge,
    Poison,
    Shield,
    Drain,
    Missile,
}

class State
{
    public State Generate(Spell spell) => new State
    {
        WPoints = WPoints,
        WMana = WMana,
        WArmor = WArmor,
        WShield = WShield,
        WPoison = WPoison,
        WRecharge = WRecharge,
        BPoints = BPoints,
        BDamage = BDamage,
        Cost = Cost,
        Wizard = !Wizard,
        Spell = spell,
    };

    public int WPoints { get; set; }
    public int WMana { get; set; }
    public int WArmor { get; set; }
    public int WShield { get; set; }
    public int WPoison { get; set; }
    public int WRecharge { get; set; }
    public int BPoints { get; set; }
    public int BDamage { get; set; }
    public int Cost { get; set; }
    public bool Wizard { get; set; }
    public Spell Spell { get; set; }

    public override string ToString() => $"{WPoints} | {WMana} | {WArmor} | {WShield} | {WPoison} | {WRecharge} | {BPoints} | {BDamage} | {Cost} | {Spell}";
}
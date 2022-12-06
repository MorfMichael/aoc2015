//using System.Collections.Generic;
//using System.Numerics;

//var boss = new Boss(71, 10);
//var wizard = new Wizard(50, 500);

////var value = wizard.Turn(new Shield(), boss, new(), int.MaxValue).ToList();

////var spells = Spell.All().Where(t => !wizard.Effects.Any(x => x.Type == t.Type)).ToList();
////Console.WriteLine("New Turns: " + string.Join(",", spells));
////foreach (var spell in spells)
////{
////    Console.WriteLine(spell);
////    foreach (var t in wizard.Clone().Turn(spell, this.Clone(), costs.ToList(), min))
////    {
////        yield return t;
////    }
////}
//bool start = true;
//int min = 0, cost = 0;
//while (!wizard.IsDead && !boss.IsDead)
//{
//    if (start)
//    {
//        var spells = Spell.All().Where(t => !wizard.Effects.Any(x => x.Type == t.Type)).ToList();
//        Console.WriteLine("New Turns: " + string.Join(",", spells));
//        foreach (var spell in spells)
//        {
//            int result = wizard.Turn(spell, boss, cost);
//            if (result == -1 || result == 0) break;
//        }
//    }
//    else
//    {
//        int result = boss.Turn(wizard);
//        if (result == -1) return;
//        if (result == 1)
//        {

//        }
//    }
//}


//class Boss
//{
//    public static Boss Create(int points, int damage) => new Boss(points, damage);

//    public Boss(int points, int damage)
//    {
//        Points = points;
//        Damage = damage;
//    }

//    public bool IsDead => Points <= 0;

//    public string Name => this.GetType().Name;

//    public int Points { get; set; }

//    public int Damage { get; set; }

//    public Boss Clone() => new Boss(Points, Damage);

//    public int Turn(Wizard wizard)
//    {
//        Console.WriteLine($"{Environment.NewLine}{Name} | {Points} points");
//        //Console.ReadKey()

//        wizard.DoEffects(this);

//        if (IsDead)
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("Boss died!");
//            Console.ResetColor();
//            return 1;
//        }

//        int damage = Damage - wizard.Armor;
//        damage = damage > 0 ? damage : 1;
//        Console.WriteLine($"{Damage} - {wizard.Armor} = {damage}");
//        //Console.ReadKey()
//        wizard.Points -= damage;

//        if (wizard.IsDead)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine("Wizard died!");
//            Console.ResetColor();
//            return -1;
//        }

//        return 0;
//    }

//    public override string ToString() => IsDead ? Name + " is dead!" : $"{Name}: {Points}";
//}

//class Wizard
//{
//    public Wizard(int points, int mana)
//    {
//        Points = points;
//        Mana = mana;
//    }

//    public string Name => this.GetType().Name;
//    public int Damage { get; set; }
//    public int Armor => Effects.OfType<Shield>().FirstOrDefault()?.Armor ?? 0;
//    public int Points { get; set; }
//    public int Mana { get; set; }

//    public bool IsDead => Points <= 0;

//    public List<Effect> Effects { get; set; } = new List<Effect>();

//    public Wizard Clone() => new Wizard(Points, Mana) { Damage = Damage, Effects = Effects.ToList() };

//    public void DoEffects(Boss boss)
//    {
//        foreach (var effect in Effects)
//        {
//            effect.Apply(this, boss);
//        }
//        Effects = Effects.Where(t => t.Timer > 0).ToList();
//    }

//    public int Turn(Spell spell, Boss boss, int cost)
//    {
//        Console.WriteLine($"{Environment.NewLine}{Name} | {(Effects.Any() ? string.Join(",",Effects) : "<no effects>")} | {Points} points | {Mana} mana");
//        //Console.ReadKey()

//        DoEffects(boss);

//        if (boss.IsDead)
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("Boss died!");
//            Console.ResetColor();
//            return cost;
//        }


//        if (spell.Cost > Mana)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine("Costs to high!");
//            Console.ResetColor();
//            return -1;
//        }

//        Mana -= spell.Cost;

//        if (spell is Effect eff)
//        {
//            Console.WriteLine($"{eff.Name} | {eff.Cost} Mana");
//            //Console.ReadKey()
//            Effects.Add(eff);
//        }
//        else
//            spell.Apply(this, boss);

//        if (boss.IsDead)
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("Boss died!");
//            Console.ResetColor();
//        }

//        return spell.Cost;
//    }
//}

//enum SpellType
//{
//    Missile,
//    Drain,
//    Shield,
//    Poison,
//    Recharge
//}

//abstract class Spell
//{
//    public static List<Spell> All() => new() { new Missile(), new Drain(), new Shield(), new Poison(), new Recharge() };

//    public Spell(int cost)
//    {
//        Cost = cost;
//    }
//    public string Name => this.GetType().Name;
//    public int Cost { get; set; }
//    public abstract SpellType Type { get; }

//    public abstract void Apply(Wizard wizard, Boss boss);

//    public override string ToString() => Name;
//}

//abstract class Effect : Spell
//{
//    public Effect(int cost, int timer) : base(cost)
//    {
//        Timer = timer;
//    }

//    public int Timer { get; set; }

//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        if (Timer > 0) Timer--;
//        if (Timer == 0)
//        {
//            Console.WriteLine($"{Name} wears off");
//            //Console.ReadKey()
//        }
//    }

//    public override string ToString() => base.ToString() + $" ({Timer})";
//}

//class Missile : Spell
//{
//    public Missile() : base(53)
//    {
//        Damage = 4;
//    }

//    public override SpellType Type => SpellType.Missile;
//    public int Damage { get; set; }

//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        Console.WriteLine($"Player casts Magic Missile, dealing {Damage} damage. Costs {Cost}");
//        //Console.ReadKey()
//        boss.Points -= Damage;
//    }
//}

//class Drain : Spell
//{
//    public Drain() : base(73)
//    {
//        Damage = 2;
//        Points = 2;
//    }

//    public override SpellType Type => SpellType.Drain;
//    public int Damage { get; set; }
//    public int Points { get; set; }

//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        Console.WriteLine($"Player casts Drain, dealing {Damage} damage, and healing {Points} hit points. Costs {Cost}");
//        //Console.ReadKey()
//        wizard.Points += 2;
//        boss.Points -= Damage;
//    }
//}

//class Shield : Effect
//{
//    public Shield() : base(113, 6)
//    {
//        Armor = 7;
//    }

//    public override SpellType Type => SpellType.Shield;
//    public int Armor { get; set; }

//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        base.Apply(wizard, boss);
//        Console.WriteLine("Shield's Timer is now " + Timer);
//        //Console.ReadKey()
//    }
//}

//class Poison : Effect
//{
//    public Poison() : base(173, 6)
//    {
//        Damage = 3;
//    }

//    public override SpellType Type => SpellType.Poison;
//    public int Damage { get; set; }

//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        Console.WriteLine($"Poison deals {Damage} damage; its timer is now {Timer}.");
//        //Console.ReadKey()
//        boss.Points -= Damage;
//        base.Apply(wizard, boss);
//    }
//}

//class Recharge : Effect
//{
//    public Recharge() : base(229, 5)
//    {
//        Mana = 101;
//    }

//    public override SpellType Type => SpellType.Recharge;
//    public int Mana { get; set; }
//    public override void Apply(Wizard wizard, Boss boss)
//    {
//        Console.WriteLine($"Recharge provides {Mana} mana; its timer is now {Timer}.");
//        //Console.ReadKey()
//        wizard.Mana += Mana;
//        base.Apply(wizard, boss);
//    }
//}
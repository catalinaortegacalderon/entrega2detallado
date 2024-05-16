namespace Fire_Emblem;

public class Prescience : Skill
{
    public Prescience() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrue();
        this.Conditions[1] = new AlwaysTrue();
        this.Conditions[1] = new OrCondition([new UnitStartsCombat(), new UseCertainWeapons(["Magic", "Bow"])]);
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeRivalsStatsIn("Atk",-5);
        this.Effects[0] = new ChangeRivalsStatsIn("Res",-5);
        this.Effects[1] = new PercentualDamageReduction(0.7, "All");
    }
}
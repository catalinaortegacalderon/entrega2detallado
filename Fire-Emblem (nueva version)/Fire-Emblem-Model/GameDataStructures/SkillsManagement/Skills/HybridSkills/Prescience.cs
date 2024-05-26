namespace Fire_Emblem_Model;

public class Prescience : Skill
{
    public Prescience() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new OrCondition([new UnitStartsCombatCondition(), new UseCertainWeaponsCondition(["Magic", "Bow"])]);
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeRivalsStatsIn("Atk",-5);
        this.Effects[1] = new ChangeRivalsStatsIn("Res",-5);
        this.Effects[2] = new PercentualDamageReduction(0.7, "First Attack");
    }
}
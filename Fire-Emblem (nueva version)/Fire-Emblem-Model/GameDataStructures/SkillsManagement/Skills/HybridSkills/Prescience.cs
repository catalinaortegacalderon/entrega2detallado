namespace Fire_Emblem_Model;

public class Prescience : Skill
{
    public Prescience() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new OrCondition([new MyUnitStartsCombatCondition(), new MyUnitUsesCertainWeaponsCondition(["Magic", "Bow"])]);
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeRivalsStatsInEffect("Atk",-5);
        this.Effects[1] = new ChangeRivalsStatsInEffect("Res",-5);
        this.Effects[2] = new PercentualDamageReductionEffect(0.7, "First Attack");
    }
}
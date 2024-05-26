namespace Fire_Emblem_Model;

public class MoonTwinWing : Skill
{
    public MoonTwinWing() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new MyHpIsBiggerThanCondition(0.25);
        this.Conditions[1] = new MyHpIsBiggerThanCondition(0.25);
        this.Conditions[2] = new AndCondition([new CompareTotalSpdCondition(), new MyHpIsBiggerThanCondition(0.25)]);
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeRivalsStatsInEffect("Spd", -5);
        this.Effects[1] = new ChangeRivalsStatsInEffect("Atk", -5);
        this.Effects[2] = new PercentualDamageReductionDeterminedByStatDifferenceEffect("Spd", 4);

    }
}

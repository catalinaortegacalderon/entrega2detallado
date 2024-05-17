namespace Fire_Emblem_Model;

public class MoonTwinWing : Skill
{
    public MoonTwinWing() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new OwnHpBiggerThan(0.25);
        this.Conditions[1] = new OwnHpBiggerThan(0.25);
        this.Conditions[2] = new AndCondition([new CompareTotalSpd(), new OwnHpBiggerThan(0.25)]);
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeRivalsStatsIn("Spd", -5);
        this.Effects[1] = new ChangeRivalsStatsIn("Atk", -5);
        this.Effects[2] = new PercentualDamageReductionDeterminedByStatDifference("Spd", 4);

    }
}

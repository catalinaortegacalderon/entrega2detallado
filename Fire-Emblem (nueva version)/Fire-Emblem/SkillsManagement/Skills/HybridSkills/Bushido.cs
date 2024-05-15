namespace Fire_Emblem;

public class Bushido : Skill
{
public Bushido() : base()
{
    this.Conditions = new Condition[2];
    this.Conditions[0] = new AlwaysTrue();
    this.Conditions[1] = new CompareTotalSpd();
    this.Effects = new Effect[2];
    this.Effects[0] = new ExtraDamageReduction(7, "All");
    this.Effects[1] = new PercentualDamageReductionDeterminedByStatDifference("Spd", 4);

}
}
namespace Fire_Emblem;

public class Dodge : Skill
{ 
    public Dodge() : base() 
    {
    this.Conditions = new Condition[1];
    this.Conditions[0] = new CompareTotalSpd(); 
    this.Effects = new Effect[1];
    this.Effects[0] = new PercentualDamageReductionDeterminedByStatDifference("Spd", 4); 
    }
}
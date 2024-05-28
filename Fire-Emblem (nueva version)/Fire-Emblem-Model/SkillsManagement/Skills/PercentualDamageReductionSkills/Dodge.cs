using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class Dodge : Skill
{ 
    public Dodge() : base() 
    {
    this.Conditions = new Condition[1];
    this.Conditions[0] = new CompareTotalSpdCondition(); 
    this.Effects = new Effect[1];
    this.Effects[0] = new PercentualDamageReductionDeterminedByStatDifferenceEffect(StatType.Spd, 4); 
    }
}
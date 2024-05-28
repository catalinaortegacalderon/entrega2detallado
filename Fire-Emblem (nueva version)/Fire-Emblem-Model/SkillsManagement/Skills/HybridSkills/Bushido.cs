using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;
public class Bushido : Skill
{
public Bushido() : base()
{
    this.Conditions = new Condition[2];
    this.Conditions[0] = new AlwaysTrueCondition();
    this.Conditions[1] = new CompareTotalSpdCondition();
    this.Effects = new Effect[2];
    this.Effects[0] = new ExtraDamageReductionEffect(7, DamageEffectCategory.All);
    this.Effects[1] = new PercentualDamageReductionDeterminedByStatDifferenceEffect(StatType.Spd, 4);

}
}
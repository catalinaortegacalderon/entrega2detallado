using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class Bravery : Skill
{ 
    public Bravery() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrueCondition(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new ExtraDamageReductionEffect(5, DamageEffectCategory.All); 
    }
}
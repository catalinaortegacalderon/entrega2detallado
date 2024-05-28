using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class BlueSkies: Skill
{
    public BlueSkies() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Effects = new Effect[2];
        this.Effects[0] = new AbsolutDamageReductionEffect(5);
        this.Effects[1] = new ExtraDamageReductionEffect(5, DamageEffectCategory.All);

    }
}
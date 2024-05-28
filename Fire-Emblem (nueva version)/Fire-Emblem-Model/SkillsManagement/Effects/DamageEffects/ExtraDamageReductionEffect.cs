using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class ExtraDamageReductionEffect: Effect
{
    private DamageEffectCategory Type;
    private int amount;
    
    public ExtraDamageReductionEffect(int amount, DamageEffectCategory type) : base()
    {
        this.amount = amount;
        this.Type = type;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    { 
        if (this.Type == DamageEffectCategory.All)
        {
            myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + this.amount;
        }
        else if (this.Type == DamageEffectCategory.FirstAttack)
        {
            myUnit.DamageEffects.ExtraDamageFirstAttack= myUnit.DamageEffects.ExtraDamageFirstAttack + this.amount;
        }
        else if (this.Type == DamageEffectCategory.FollowUp)
        {
            myUnit.DamageEffects.ExtraDamageFollowup = myUnit.DamageEffects.ExtraDamageFollowup + this.amount;
        }
    }
}
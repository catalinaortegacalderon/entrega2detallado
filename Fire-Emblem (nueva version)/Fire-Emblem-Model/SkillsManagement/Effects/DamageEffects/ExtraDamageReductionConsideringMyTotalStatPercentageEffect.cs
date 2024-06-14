using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;


public class ExtraDamageReductionConsideringMyTotalStatPercentageEffect : Effect
{
    
    // todo: arreglar
    private readonly double _percentage;
    private readonly StatType _stat;
    private readonly DamageEffectCategory _type;

    public ExtraDamageReductionConsideringMyTotalStatPercentageEffect(DamageEffectCategory type, 
        StatType stat, double percentage)
    {
        _type = type;
        _stat = stat;
        _percentage = percentage;
    }
    
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        var amount = _stat switch
        {
            StatType.Res => myUnit.Res + myUnit.ActiveBonus.Res * myUnit.ActiveBonusNeutralizer.Res +
                            myUnit.ActivePenalties.Res * myUnit.ActivePenaltiesNeutralizer.Res,
            StatType.Atk => myUnit.Atk + myUnit.ActiveBonus.Atk * myUnit.ActiveBonusNeutralizer.Atk +
                            myUnit.ActivePenalties.Atk * myUnit.ActivePenaltiesNeutralizer.Atk,
            StatType.Def => myUnit.Def + myUnit.ActiveBonus.Def * myUnit.ActiveBonusNeutralizer.Def +
                            myUnit.ActivePenalties.Def * myUnit.ActivePenaltiesNeutralizer.Def,
            StatType.Spd => myUnit.Spd + myUnit.ActiveBonus.Spd * myUnit.ActiveBonusNeutralizer.Spd +
                            myUnit.ActivePenalties.Spd * myUnit.ActivePenaltiesNeutralizer.Spd,
            _ => 0
        };

        amount = Convert.ToInt32(Math.Truncate(amount * _percentage));
        
        
        if (_type == DamageEffectCategory.All)
            myUnit.DamageEffects.ExtraDamage += amount;
        else if (_type == DamageEffectCategory.FirstAttack)
            myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
        else if (_type == DamageEffectCategory.FollowUp)
            myUnit.DamageEffects.ExtraDamageFollowup += amount;
    }
}
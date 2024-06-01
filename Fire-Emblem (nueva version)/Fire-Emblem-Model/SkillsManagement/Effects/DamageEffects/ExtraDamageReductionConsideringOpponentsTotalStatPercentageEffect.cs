using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class ExtraDamageReductionConsideringOpponentsTotalStatPercentageEffect : Effect
{
    private readonly double _percentage;
    private readonly StatType _stat;
    private readonly DamageEffectCategory _type;

    public ExtraDamageReductionConsideringOpponentsTotalStatPercentageEffect(DamageEffectCategory type, 
        StatType stat, double percentage)
    {
        _type = type;
        _stat = stat;
        _percentage = percentage;
    }
    
    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        var amount = 0;
        
        if (_stat == StatType.Res)
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizer.Res
                                  + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizer.Res;
        if (_stat == StatType.Atk)
            amount =
                opponentsUnit.Atk + opponentsUnit.ActiveBonus.Atk * opponentsUnit.ActiveBonusNeutralizer.Atk
                                  + opponentsUnit.ActivePenalties.Atk * opponentsUnit.ActivePenaltiesNeutralizer.Atk;
        if (_stat == StatType.Def)
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralizer.Def
                                  + opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenaltiesNeutralizer.Def;
        if (_stat == StatType.Spd)
            amount =
                opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizer.Spd
                                  + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizer.Spd;
        
        amount = Convert.ToInt32(Math.Truncate(amount * _percentage));
        
        
        if (_type == DamageEffectCategory.All)
            myUnit.DamageEffects.ExtraDamage += amount;
        else if (_type == DamageEffectCategory.FirstAttack)
            myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
        else if (_type == DamageEffectCategory.FollowUp)
            myUnit.DamageEffects.ExtraDamageFollowup += amount;
    }
}
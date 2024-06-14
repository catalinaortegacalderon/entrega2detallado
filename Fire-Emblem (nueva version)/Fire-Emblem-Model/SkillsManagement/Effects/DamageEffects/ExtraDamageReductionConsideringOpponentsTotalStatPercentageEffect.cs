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
        var amount = _stat switch
        {
            StatType.Res => opponentsUnit.Res +
                            opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizer.Res +
                            opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizer.Res,
            StatType.Atk => opponentsUnit.Atk +
                            opponentsUnit.ActiveBonus.Atk * opponentsUnit.ActiveBonusNeutralizer.Atk +
                            opponentsUnit.ActivePenalties.Atk * opponentsUnit.ActivePenaltiesNeutralizer.Atk,
            StatType.Def => opponentsUnit.Def +
                            opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralizer.Def +
                            opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenaltiesNeutralizer.Def,
            StatType.Spd => opponentsUnit.Spd +
                            opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizer.Spd +
                            opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizer.Spd,
            _ => 0
        };
        
        amount = Convert.ToInt32(Math.Truncate(amount * _percentage));
        
        switch (_type)
        {
            case DamageEffectCategory.All:
                myUnit.DamageEffects.ExtraDamage += amount;
                break;
            case DamageEffectCategory.FirstAttack:
                myUnit.DamageEffects.ExtraDamageFirstAttack += amount;
                break;
            case DamageEffectCategory.FollowUp:
                myUnit.DamageEffects.ExtraDamageFollowup += amount;
                break;
        }
    }
}

using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.DamageEffects;

public class ExtraDamageReductionConsideringOpponentsTotalStatPercentajeEffect: Effect
{
    private DamageEffectCategory _type;
    private StatType _stat;
    private double _percentage;
    
    public ExtraDamageReductionConsideringOpponentsTotalStatPercentajeEffect(DamageEffectCategory type, StatType stat, double percentage) : base()
    {
        this._type = type;
        this._stat = stat;
        this._percentage = percentage;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
    {
        int amount = 0;
        if (this._stat == StatType.Res)
        {
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Res * opponentsUnit.ActiveBonusNeutralizator.Res
                                  + opponentsUnit.ActivePenalties.Res * opponentsUnit.ActivePenaltiesNeutralizator.Res;
        }
        if (this._stat == StatType.Atk)
        {
            amount =
                opponentsUnit.Atk + opponentsUnit.ActiveBonus.Attk * opponentsUnit.ActiveBonusNeutralizator.Attk
                                  + opponentsUnit.ActivePenalties.Attk * opponentsUnit.ActivePenaltiesNeutralizator.Attk;
        }
        if (this._stat == StatType.Def)
        {
            amount =
                opponentsUnit.Res + opponentsUnit.ActiveBonus.Def * opponentsUnit.ActiveBonusNeutralizator.Def
                                  + opponentsUnit.ActivePenalties.Def * opponentsUnit.ActivePenaltiesNeutralizator.Def;
        }
        if (this._stat == StatType.Spd)
        {
            amount =
                opponentsUnit.Spd + opponentsUnit.ActiveBonus.Spd * opponentsUnit.ActiveBonusNeutralizator.Spd
                                  + opponentsUnit.ActivePenalties.Spd * opponentsUnit.ActivePenaltiesNeutralizator.Spd;
        }
        amount = Convert.ToInt32(Math.Truncate(amount * this._percentage));
        if (this._type == DamageEffectCategory.All)
        {
            myUnit.DamageEffects.ExtraDamage = myUnit.DamageEffects.ExtraDamage + amount;
        }
        else if (this._type == DamageEffectCategory.FirstAttack)
        {
            myUnit.DamageEffects.ExtraDamageFirstAttack= myUnit.DamageEffects.ExtraDamageFirstAttack + amount;
        }
        else if (this._type == DamageEffectCategory.FollowUp)
        {
            myUnit.DamageEffects.ExtraDamageFollowup = myUnit.DamageEffects.ExtraDamageFollowup + amount;
        }
    }
}
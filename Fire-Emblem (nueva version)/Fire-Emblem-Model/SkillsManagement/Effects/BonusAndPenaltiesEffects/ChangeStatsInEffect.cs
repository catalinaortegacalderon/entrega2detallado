using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;

public class ChangeStatsInEffect : Effect
{
    private StatType _stat;
    public ChangeStatsInEffect(StatType stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == StatType.Atk)
        {
            if ( Amount > 0) myUnit.ActiveBonus.Attk  = myUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Attk  = myUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == StatType.Def)
        {
            if ( Amount > 0) myUnit.ActiveBonus.Def  = myUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Def  = myUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == StatType.Res)
        {
            if ( Amount > 0) myUnit.ActiveBonus.Res  = myUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Res  = myUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == StatType.Spd)
        {
            if ( Amount > 0) myUnit.ActiveBonus.Spd  = myUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Spd  = myUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}
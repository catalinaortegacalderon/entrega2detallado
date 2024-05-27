using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class ChangeOpponentsStatsInEffect : Effect
{
    private StatType _stat;
    public ChangeOpponentsStatsInEffect(StatType stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == StatType.Atk)
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Attk  = opponentsUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Attk  = opponentsUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == StatType.Def)
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Def  = opponentsUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Def  = opponentsUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == StatType.Res)
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Res  = opponentsUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Res  = opponentsUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == StatType.Spd)
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Spd  = opponentsUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Spd  = opponentsUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}
namespace Fire_Emblem_Model;

public class ChangeOpponentsStatsInEffect : Effect
{
    private String _stat;
    public ChangeOpponentsStatsInEffect(String stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == "Atk")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Attk  = opponentsUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Attk  = opponentsUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Def  = opponentsUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Def  = opponentsUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Res  = opponentsUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Res  = opponentsUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) opponentsUnit.ActiveBonus.Spd  = opponentsUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) opponentsUnit.ActivePenalties.Spd  = opponentsUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}
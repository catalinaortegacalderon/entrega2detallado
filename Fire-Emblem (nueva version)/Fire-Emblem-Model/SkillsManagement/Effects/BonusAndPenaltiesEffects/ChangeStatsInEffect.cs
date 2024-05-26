namespace Fire_Emblem_Model;

public class ChangeStatsInEffect : Effect
{
    private String _stat;
    public ChangeStatsInEffect(String stat, int amount) : base()
    {
        this.Amount = amount;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == "Atk")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Attk  = myUnit.ActiveBonus.Attk + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Attk  = myUnit.ActivePenalties.Attk + this.Amount;
        }
        else if (_stat == "Def")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Def  = myUnit.ActiveBonus.Def + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Def  = myUnit.ActivePenalties.Def + this.Amount;
        }
        else if (_stat == "Res")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Res  = myUnit.ActiveBonus.Res + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Res  = myUnit.ActivePenalties.Res + this.Amount;
        }
        else if (_stat == "Spd")
        {
            if ( Amount > 0) myUnit.ActiveBonus.Spd  = myUnit.ActiveBonus.Spd + this.Amount;
            if ( Amount < 0) myUnit.ActivePenalties.Spd  = myUnit.ActivePenalties.Spd + this.Amount;
        }
    }
}
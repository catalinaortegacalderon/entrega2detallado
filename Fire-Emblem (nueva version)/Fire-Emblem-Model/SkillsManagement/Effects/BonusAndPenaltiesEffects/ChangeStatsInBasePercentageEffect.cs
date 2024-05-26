namespace Fire_Emblem_Model;

public class ChangeStatsInBasePercentageEffect : Effect
{
    private String _stat;
    private double _percentage;
    public ChangeStatsInBasePercentageEffect(String stat, double percentage) : base()
    {
        this._percentage =  percentage;
        this._stat = stat;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == "Atk")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Attk  += (int)(this._percentage * myUnit.Atk);
            if ( _percentage < 0) myUnit.ActivePenalties.Attk  +=  (int)(this._percentage * myUnit.Atk);
        }
        else if (_stat == "Def")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Def  +=   (int)(this._percentage * myUnit.Def);
            if ( _percentage < 0) myUnit.ActivePenalties.Def  +=  (int)(this._percentage * myUnit.Def);
        }
        else if (_stat == "Res")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Res  +=  (int)(this._percentage * myUnit.Res);
            if ( _percentage < 0) myUnit.ActivePenalties.Res  +=  (int)(this._percentage * myUnit.Res);
        }
        else if (_stat == "Spd")
        {
            if ( _percentage > 0) myUnit.ActiveBonus.Spd  +=  (int)(this._percentage * myUnit.Spd);
            if ( _percentage < 0) myUnit.ActivePenalties.Spd  += (int)(this._percentage * myUnit.Spd);
        }
    }
}
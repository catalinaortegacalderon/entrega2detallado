namespace Fire_Emblem_Model;

public class ChangeStatsInBasePlusOnePointForEveryEffect : Effect
{
    private string _stat;
    private int _baseIncrease;
    private int _divisor;
    public ChangeStatsInBasePlusOnePointForEveryEffect(String stat, int baseIncrease, int divisor) : base()
    {
        this._stat = stat;
        this._divisor = divisor;
        this._baseIncrease = baseIncrease;
    }

    public override void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        if (_stat == "Spd")
        {
            double division = (myUnit.Spd / _divisor);
            Amount = Convert.ToInt32(Math.Truncate(division));
            myUnit.ActiveBonus.Spd += (Amount + _baseIncrease);
        }
    }
}
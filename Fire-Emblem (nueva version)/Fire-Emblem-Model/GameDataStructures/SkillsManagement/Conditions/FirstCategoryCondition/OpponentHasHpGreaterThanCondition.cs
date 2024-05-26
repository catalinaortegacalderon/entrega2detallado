namespace Fire_Emblem_Model;

public class OpponentHasHpGreaterThanCondition : Condition
{
    private double _percentage;
    public OpponentHasHpGreaterThanCondition(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (opponentsUnit.CurrentHp >= opponentsUnit.HpMax * this._percentage)
        {
            return true;
        }
        return false;
    }
}
namespace Fire_Emblem_Model;

public class MyHpIsBiggerThanCondition : Condition
{
    private double _amount;
    public MyHpIsBiggerThanCondition(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (Math.Round((double)myUnit.CurrentHp / myUnit.HpMax,2) >= this._amount)
        {
            return true;
        }
        return false;
    }
}
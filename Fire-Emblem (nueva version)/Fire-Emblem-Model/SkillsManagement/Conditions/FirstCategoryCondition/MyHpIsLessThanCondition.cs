namespace Fire_Emblem_Model;

public class MyHpIsLessThanCondition : Condition
{
    private double _amount;
    public MyHpIsLessThanCondition(double amount) : base()
    {
        this._amount = amount;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.CurrentHp <= myUnit.HpMax * this._amount)
        {
            return true;
        }
        return false;
    }
}
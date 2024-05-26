namespace Fire_Emblem_Model;

public class RivalStartsAttackAndMyHpIsLessThan : Condition
{
    private double _percentage;
    public RivalStartsAttackAndMyHpIsLessThan(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        if (myUnit.CurrentHp <= myUnit.HpMax * this._percentage && !myUnit.IsAttacking)
        {
            return true;
        }
        return false;
    }
}
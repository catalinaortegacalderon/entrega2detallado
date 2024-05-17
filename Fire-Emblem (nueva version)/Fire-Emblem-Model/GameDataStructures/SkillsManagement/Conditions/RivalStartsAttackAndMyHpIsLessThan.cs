namespace Fire_Emblem_Model;

public class RivalStartsAttackAndMyHpIsLessThan : Condition
{
    private double _percentage;
    public RivalStartsAttackAndMyHpIsLessThan(double percentage) : base()
    {
        this._percentage = percentage;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        if (myUnit.CurrentHp <= myUnit.HpMax * this._percentage && !iAmAttacking)
        {
            return true;
        }
        return false;
    }
}
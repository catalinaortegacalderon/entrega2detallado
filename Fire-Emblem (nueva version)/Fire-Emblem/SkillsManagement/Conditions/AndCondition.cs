namespace Fire_Emblem;

public class AndCondition : Condition
{
    private Condition[] conditions;

    public AndCondition(Condition[] conditions) : base()
    {
        this.conditions = conditions;
    }
    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        foreach (Condition condition in this.conditions)
        {
            if (condition.Verify(myUnit, opponentsUnit, iAmAttacking ) == false)
            {
                return false;
            }
        } 
        return true;
    }
}
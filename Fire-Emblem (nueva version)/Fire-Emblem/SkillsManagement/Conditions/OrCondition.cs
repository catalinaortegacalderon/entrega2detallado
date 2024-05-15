using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class OrCondition : Condition
{
    private Condition[] conditions;

    public OrCondition(Condition[] conditions) : base()
    {
        this.conditions = conditions;
    }

    public override bool Verify(Unit myUnit, Unit opponentsUnit, bool iAmAttacking)
    {
        foreach (Condition condition in this.conditions)
        {
            if (condition.Verify(myUnit, opponentsUnit, iAmAttacking ) == true)
            {
                return true;
            }
        } 
        return false;
    }
}
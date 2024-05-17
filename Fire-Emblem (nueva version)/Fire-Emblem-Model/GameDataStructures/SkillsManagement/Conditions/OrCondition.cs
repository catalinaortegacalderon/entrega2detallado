using System.Runtime.CompilerServices;
using Fire_Emblem;

namespace Fire_Emblem_Model;

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
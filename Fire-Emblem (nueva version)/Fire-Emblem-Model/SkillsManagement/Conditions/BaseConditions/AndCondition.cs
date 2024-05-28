using Fire_Emblem;

namespace Fire_Emblem_Model;

public class AndCondition : Condition
{
    private Condition[] conditions;

    public AndCondition(Condition[] conditions) : base()
    {
        this.conditions = conditions;
    }
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        foreach (Condition condition in this.conditions)
        {
            if (condition.DoesItHold(myUnit, opponentsUnit) == false)
            {
                return false;
            }
        } 
        return true;
    }
}
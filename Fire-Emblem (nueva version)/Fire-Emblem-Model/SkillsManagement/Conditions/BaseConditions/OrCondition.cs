using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

public class OrCondition : Condition
{
    private Condition[] conditions;

    public OrCondition(Condition[] conditions) : base()
    {
        this.conditions = conditions;
    }

    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        foreach (Condition condition in this.conditions)
        {
            if (condition.DoesItHold(myUnit, opponentsUnit) == true)
            {
                return true;
            }
        } 
        return false;
    }
}
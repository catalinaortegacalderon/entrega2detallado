using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

public class OrCondition : Condition
{
    private readonly Condition[] conditions;

    public OrCondition(Condition[] conditions)
    {
        this.conditions = conditions;
    }

    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        foreach (var condition in conditions)
            if (condition.DoesItHold(myUnit, opponentsUnit))
                return true;
        return false;
    }
}
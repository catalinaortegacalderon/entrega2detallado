using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

public class AndCondition : Condition
{
    private readonly Condition[] conditions;

    public AndCondition(Condition[] conditions)
    {
        this.conditions = conditions;
    }

    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        foreach (var condition in conditions)
            if (condition.DoesItHold(myUnit, opponentsUnit) == false)
                return false;
        return true;
    }
}
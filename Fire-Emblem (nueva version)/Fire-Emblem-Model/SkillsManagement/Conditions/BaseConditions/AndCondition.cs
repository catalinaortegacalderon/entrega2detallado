using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

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
        Console.WriteLine(" SE CUMPLE LA CONDICION AND");
        return true;
    }
}
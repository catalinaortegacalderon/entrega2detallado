using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

namespace ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;

public class MyUnitStartsCombatCondition : Condition
{
    public override bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        Console.WriteLine(myUnit.Name);
        Console.WriteLine("is atacking: " + myUnit.IsAttacking);
        return myUnit.IsAttacking;
    }
}
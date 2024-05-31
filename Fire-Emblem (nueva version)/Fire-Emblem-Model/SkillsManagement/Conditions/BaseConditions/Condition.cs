using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace ConsoleApp1.SkillsManagement.Conditions.BaseConditions;

public class Condition
{
    protected ConditionPriority Priority;
    
    protected Condition() : base()
    {
        this.Priority = ConditionPriority.PriorityOfBonusAndPenalties;
    }
    public virtual bool DoesItHold(Unit myUnit, Unit opponentsUnit)
    {
        return true;
    }

    public ConditionPriority GetPriority()
    {
        return this.Priority;
    }
    
    public void ChangePriorityBecauseEffectPriorityIsBigger(ConditionPriority priority)
    {
        this.Priority = priority;
    }
}
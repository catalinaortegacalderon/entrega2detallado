using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;

public class Condition
{
    protected int Priority;
    
    protected Condition() : base()
    {
        this.Priority = 1;
    }
    public virtual bool Verify(Unit myUnit, Unit opponentsUnit)
    {
        return true;
    }

    public int GetPriority()
    {
        return this.Priority;
    }
    
    public void ChangePriorityBecauseOfSecondCategoryEffect(int priority)
    {
        this.Priority = priority;
    }
}
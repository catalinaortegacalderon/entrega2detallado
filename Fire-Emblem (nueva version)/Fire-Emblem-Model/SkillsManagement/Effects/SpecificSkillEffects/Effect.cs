using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;

public class Effect
{
    protected int Priority;
    protected int Amount;
    public Effect()
    {
        this.Priority = 1;
    }
    public virtual void ApplyEffect(Unit myUnit, Unit opponentsUnit)
        
    {
        return;
    }

    public virtual int GetPriority()
    {
        return this.Priority;
    }
}
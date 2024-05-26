using Fire_Emblem;

namespace Fire_Emblem_Model;

public class Gentility : Skill
{
    public Gentility() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrueCondition(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReductionEffect(5); 
    }
}

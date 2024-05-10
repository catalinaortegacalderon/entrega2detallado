namespace Fire_Emblem;

public class Gentility : Skill
{
    public Gentility() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrue(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReduction(5); 
    }
}

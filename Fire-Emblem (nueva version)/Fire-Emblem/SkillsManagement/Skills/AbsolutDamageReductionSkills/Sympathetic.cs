namespace Fire_Emblem;

public class Sympathetic : Skill
{
    public Sympathetic() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new RivalStartsAttackAndMyHpIsLessThan(0.5); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReduction(5); 
    }
}
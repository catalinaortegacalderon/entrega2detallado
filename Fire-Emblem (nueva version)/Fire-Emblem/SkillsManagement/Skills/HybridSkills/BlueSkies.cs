namespace Fire_Emblem;

public class BlueSkies: Skill
{
    public BlueSkies() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrue();
        this.Conditions[1] = new AlwaysTrue();
        this.Effects = new Effect[2];
        this.Effects[0] = new AbsolutDamageReduction(5);
        this.Effects[1] = new ExtraDamageReduction(5, "All");

    }
}
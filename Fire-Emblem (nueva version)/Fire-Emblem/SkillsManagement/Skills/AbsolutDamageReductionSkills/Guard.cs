namespace Fire_Emblem;

public class Guard : Skill
{
    public Guard(String weapon) : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentUsesCertainWeapon([weapon]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReduction(5); 
    }
}

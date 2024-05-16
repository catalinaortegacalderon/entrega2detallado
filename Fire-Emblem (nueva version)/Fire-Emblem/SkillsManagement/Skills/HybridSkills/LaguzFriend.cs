namespace Fire_Emblem;

public class LaguzFriend : Skill
{
    public LaguzFriend() : base()
    {
        this.Conditions = new Condition[5];
        this.Conditions[0] = new AlwaysTrue();
        this.Conditions[1] = new AlwaysTrue();
        this.Conditions[2] = new AlwaysTrue();
        this.Conditions[3] = new AlwaysTrue();
        this.Conditions[4] = new AlwaysTrue();
        this.Effects = new Effect[5];
        this.Effects[0] = new PercentualDamageReduction(0.5, "All");
        this.Effects[1] = new ChangeStatsInBasePercentage("Def", -0.5);
        this.Effects[2] = new ChangeStatsInBasePercentage("Res", -0.5);
        this.Effects[3] = new NeutralizeOneOfMyBonus("Def");
        this.Effects[4] = new NeutralizeOneOfMyBonus("Res");
    }
    
}
namespace Fire_Emblem_Model;

public class LaguzFriend : Skill
{
    public LaguzFriend() : base()
    {
        this.Conditions = new Condition[5];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
        this.Conditions[3] = new AlwaysTrueCondition();
        this.Conditions[4] = new AlwaysTrueCondition();
        this.Effects = new Effect[5];
        this.Effects[0] = new PercentualDamageReductionEffect(0.5, "All");
        this.Effects[1] = new ChangeStatsInBasePercentageEffect("Def", -0.5);
        this.Effects[2] = new ChangeStatsInBasePercentageEffect("Res", -0.5);
        this.Effects[3] = new NeutralizeOneOfMyBonusEffect("Def");
        this.Effects[4] = new NeutralizeOneOfMyBonusEffect("Res");
    }
    
}
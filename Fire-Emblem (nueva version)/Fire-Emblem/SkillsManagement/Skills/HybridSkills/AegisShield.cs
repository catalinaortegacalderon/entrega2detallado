namespace Fire_Emblem;

public class AegisShield : Skill
{
    public AegisShield() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrue();
        this.Conditions[1] = new AlwaysTrue();
        this.Conditions[2] = new AlwaysTrue();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn("Def", 6);
        this.Effects[1] = new ChangeStatsIn("Res", 3);
        this.Effects[2] = new PercentualDamageReduction(0.5, "First Attack");
    }
    
}
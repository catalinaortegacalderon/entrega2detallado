namespace Fire_Emblem_Model;

public class AegisShield : Skill
{
    public AegisShield() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new AlwaysTrueCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn("Def", 6);
        this.Effects[1] = new ChangeStatsIn("Res", 3);
        this.Effects[2] = new PercentualDamageReduction(0.5, "First Attack");
    }
    
}
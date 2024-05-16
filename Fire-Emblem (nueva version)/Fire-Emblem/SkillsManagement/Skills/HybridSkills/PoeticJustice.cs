namespace Fire_Emblem;

public class PoeticJustice : Skill
{
    public PoeticJustice() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrue();
        this.Conditions[1] = new AlwaysTrue();
        this.Conditions[1].ChangePriorityBecauseOfSecondCategoryEffect(2);
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeRivalsStatsIn("Spd",-4);
        this.Effects[1] = new ExtraDamageReductionConsideringOpponentsTotalStatPercentaje( "All",  "Atk", 0.15);
    }
}
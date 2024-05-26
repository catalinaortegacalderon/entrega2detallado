namespace Fire_Emblem_Model;

public class PoeticJustice : Skill
{
    public PoeticJustice() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[1].ChangePriorityBecauseOfSecondCategoryEffect(2);
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeRivalsStatsInEffect("Spd",-4);
        this.Effects[1] = new ExtraDamageReductionConsideringOpponentsTotalStatPercentajeEffect( "All",  "Atk", 0.15);
    }
}
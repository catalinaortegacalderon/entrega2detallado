namespace Fire_Emblem_Model;

public class SpeedPlus5 : Skill
{
    public SpeedPlus5() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
        this.Effects = new Effect[] { new ChangeStatsInEffect( "Spd", 5) };
    }
}
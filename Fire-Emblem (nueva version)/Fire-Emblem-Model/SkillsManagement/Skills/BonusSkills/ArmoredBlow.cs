namespace Fire_Emblem_Model;

public class ArmoredBlow : Skill
{
    public ArmoredBlow() : base()
    {
        this.Conditions = new Condition[] { new MyUnitStartsCombatCondition() };
        this.Effects = new Effect[] { new ChangeStatsInEffect("Def", 8) };
    }
}
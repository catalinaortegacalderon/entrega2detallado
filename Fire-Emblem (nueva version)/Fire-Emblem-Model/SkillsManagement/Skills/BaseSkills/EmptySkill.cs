namespace Fire_Emblem_Model;

public class EmptySkill : Skill
{
    public EmptySkill() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
        this.Effects = new Effect[] { new EmptyEffect() };
    }
}
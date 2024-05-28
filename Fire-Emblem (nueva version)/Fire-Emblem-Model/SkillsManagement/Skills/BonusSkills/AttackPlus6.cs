using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class AttackPlus6 : Skill
{
    public AttackPlus6() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Atk, 6) };
    }
}
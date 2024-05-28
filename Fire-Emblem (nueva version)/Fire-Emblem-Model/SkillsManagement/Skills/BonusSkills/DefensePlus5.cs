using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class DefensePlus5 : Skill
{
    public DefensePlus5() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Def, 5) };
    }
}
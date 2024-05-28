using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class ResistancePlus5 : Skill
{
    public ResistancePlus5() : base()
    {
        this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            
        this.Effects = new Effect[] { new ChangeStatsInEffect( StatType.Res, 5) };
    }
}
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class AtkAndResPlus5 : Skill
{
    public AtkAndResPlus5() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition(); 
        this.Conditions[1] = new AlwaysTrueCondition(); 

        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 5); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 5);
            
    }
}
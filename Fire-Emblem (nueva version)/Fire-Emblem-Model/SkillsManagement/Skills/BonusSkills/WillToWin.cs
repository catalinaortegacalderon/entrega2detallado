using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class WillToWin : Skill
{
    public WillToWin() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyHpIsLessThanCondition(0.5); 
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 8); 
    }
}
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class ChaosStyle : Skill
{
    public ChaosStyle() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new ChaosStyleCondition();
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Spd, 3); 
    }
}
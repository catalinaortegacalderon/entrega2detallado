using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class BrazenSpdDef : Skill
{
    public BrazenSpdDef() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
        this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Spd,10); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Def, 10); 
    }
}
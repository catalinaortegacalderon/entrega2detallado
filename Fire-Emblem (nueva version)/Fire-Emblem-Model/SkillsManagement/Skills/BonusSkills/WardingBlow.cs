using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class WardingBlow : Skill
{
    public WardingBlow() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Res, 8);
    }
}
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class DeathBlow : Skill
{
    public DeathBlow() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Atk, 8);
    }
}
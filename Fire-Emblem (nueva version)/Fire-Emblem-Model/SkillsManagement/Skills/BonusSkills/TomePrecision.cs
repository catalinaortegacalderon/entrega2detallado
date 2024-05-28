using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class TomePrecision : Skill
{
    public TomePrecision() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([Weapon.Magic]);
        this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([Weapon.Magic]);
            
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk,6); 
        this.Effects[1] = new ChangeStatsInEffect(StatType.Spd, 6); 
    }
}
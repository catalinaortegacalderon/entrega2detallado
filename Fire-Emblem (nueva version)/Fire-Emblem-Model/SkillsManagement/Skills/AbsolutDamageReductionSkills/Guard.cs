using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class Guard : Skill
{
    public Guard(Weapon weapon) : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentUsesCertainWeaponCondition([weapon]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReductionEffect(5); 
    }
}

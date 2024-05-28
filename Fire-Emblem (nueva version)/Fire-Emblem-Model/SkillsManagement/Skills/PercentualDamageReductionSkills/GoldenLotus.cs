using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class GoldenLotus :  Skill
{
    public GoldenLotus() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentUsesCertainWeaponCondition([Weapon.Sword, Weapon.Axe, Weapon.Lance, Weapon.Bow]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReductionEffect(0.5,DamageEffectCategory.FirstAttack); 
    }
}

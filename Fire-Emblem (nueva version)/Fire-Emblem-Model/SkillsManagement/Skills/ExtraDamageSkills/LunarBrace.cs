using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class LunarBrace : Skill
{ 
    public LunarBrace() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new  AndCondition([new MyUnitUsesCertainWeaponsCondition([Weapon.Sword, Weapon.Bow, Weapon.Axe, Weapon.Lance]), new MyUnitStartsCombatCondition()]);
        this.Conditions[0].ChangePriorityBecauseEffectPriorityIsBigger(2);
        this.Effects = new Effect[1];
        this.Effects[0] = new LunarBraceEffect();
    }
}
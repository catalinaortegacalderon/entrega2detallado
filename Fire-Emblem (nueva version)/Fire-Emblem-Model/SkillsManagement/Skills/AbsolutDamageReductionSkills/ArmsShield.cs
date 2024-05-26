namespace Fire_Emblem_Model;

public class ArmsShield : Skill
{
    public ArmsShield() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyUnitHasWeaponAdvantageCondition(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReductionEffect(5); 
    }
}
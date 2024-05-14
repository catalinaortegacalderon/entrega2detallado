namespace Fire_Emblem;

public class ArmsShield : Skill
{
    public ArmsShield() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new UnitHasWeaponAdvantage(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new AbsolutDamageReduction(5); 
    }
}
namespace Fire_Emblem;

public class LunarBrace : Skill
{ 
    public LunarBrace() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new UseCertainWeaponAndStartCombat(["Sword", "Bow", "Axe", "Lance"]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReductionDeterminedByStatDifference("Spd", 4); 
    }
}
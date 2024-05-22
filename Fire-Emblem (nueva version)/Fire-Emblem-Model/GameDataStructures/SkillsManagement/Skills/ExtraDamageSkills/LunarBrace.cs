namespace Fire_Emblem_Model;

public class LunarBrace : Skill
{ 
    public LunarBrace() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new  AndCondition([new UseCertainWeapons(["Sword", "Bow", "Axe", "Lance"]), new UnitStartsCombat()]);
        this.Conditions[0].ChangePriorityBecauseOfSecondCategoryEffect(2);
        this.Effects = new Effect[1];
        this.Effects[0] = new LunarBraceEffect();
    }
}
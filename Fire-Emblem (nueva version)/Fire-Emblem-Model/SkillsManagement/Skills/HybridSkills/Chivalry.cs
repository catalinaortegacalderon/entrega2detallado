namespace Fire_Emblem_Model;

public class Chivalry : Skill
{
    public Chivalry() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AndCondition([new MyUnitStartsCombatCondition(), new OpponentHasFullHpCondition()]);
        this.Conditions[1] = new AndCondition([new MyUnitStartsCombatCondition(), new OpponentHasFullHpCondition()]);
        this.Effects = new Effect[2];
        this.Effects[0] = new AbsolutDamageReductionEffect(2);
        this.Effects[1] = new ExtraDamageReductionEffect(2, "All");
    }
    
}
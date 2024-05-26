namespace Fire_Emblem_Model;

public class RemoteSturdy : Skill
{
    public RemoteSturdy() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new UnitStartsCombatCondition();
        this.Conditions[1] = new UnitStartsCombatCondition();
        this.Conditions[2] = new UnitStartsCombatCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn("Atk",7);
        this.Effects[1] = new ChangeStatsIn("Def",10);
        this.Effects[2] = new PercentualDamageReduction(0.7, "First Attack");
    }
}
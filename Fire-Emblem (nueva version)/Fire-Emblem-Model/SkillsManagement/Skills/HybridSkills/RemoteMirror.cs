namespace Fire_Emblem_Model;

public class RemoteMirror : Skill
{
    // hay muchos remote, tal vez agrupar
    public RemoteMirror() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
        this.Conditions[1] = new MyUnitStartsCombatCondition();
        this.Conditions[2] = new MyUnitStartsCombatCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsInEffect("Atk",7);
        this.Effects[1] = new ChangeStatsInEffect("Res",10);
        this.Effects[2] = new PercentualDamageReductionEffect(0.7, "First Attack");
    }
}
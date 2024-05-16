namespace Fire_Emblem;

public class RemoteMirror : Skill
{
    // hay muchos remote, tal vez agrupar
    public RemoteMirror() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new UnitStartsCombat();
        this.Conditions[1] = new UnitStartsCombat();
        this.Conditions[2] = new UnitStartsCombat();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn("Atk",7);
        this.Effects[1] = new ChangeStatsIn("Res",10);
        this.Effects[2] = new PercentualDamageReduction(0.7, "First Attack");
    }
}
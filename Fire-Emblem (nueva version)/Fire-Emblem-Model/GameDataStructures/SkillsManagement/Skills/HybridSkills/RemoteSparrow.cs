namespace Fire_Emblem_Model;
public class RemoteSparrow : Skill
{
    public RemoteSparrow() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new UnitStartsCombat();
        this.Conditions[1] = new UnitStartsCombat();
        this.Conditions[2] = new UnitStartsCombat();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn("Atk",7);
        this.Effects[1] = new ChangeStatsIn("Spd",7);
        this.Effects[2] = new PercentualDamageReduction(0.7, "First Attack");
    }
}
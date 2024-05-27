using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class GuardBearing : Skill
{
    public GuardBearing() : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new AlwaysTrueCondition();
        this.Conditions[2] = new MyUnitStartsCombatCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeOpponentsStatsInEffect(StatType.Spd,-4);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Def,-4);
        this.Effects[2] = new PercentualDamageReductionEffect(0.7, "First Attack");
    }
}

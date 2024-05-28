using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;

public class FairFight : Skill
{
    public FairFight() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
        this.Conditions[1] = new MyUnitStartsCombatCondition();
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk,6);
        this.Effects[1] = new ChangeOpponentsStatsInEffect(StatType.Atk, 6);
    }
}
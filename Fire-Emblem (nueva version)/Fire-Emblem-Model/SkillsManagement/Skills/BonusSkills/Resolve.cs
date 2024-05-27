using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class Resolve : Skill
{
    public Resolve() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new MyHpIsLessThanCondition(0.75); 
        this.Conditions[1] = new MyHpIsLessThanCondition(0.75);
        this.Effects = new Effect[2];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Def, 7); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 7); 
    }
}
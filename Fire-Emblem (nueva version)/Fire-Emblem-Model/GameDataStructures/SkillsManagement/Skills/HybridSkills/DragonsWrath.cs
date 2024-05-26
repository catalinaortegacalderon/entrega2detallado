namespace Fire_Emblem_Model;

public class DragonsWrath : Skill
{
    public DragonsWrath() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new CompareMyAtkWithOpponentsRes();
        this.Effects = new Effect[2];
        this.Effects[0] = new PercentualDamageReduction(0.75, "First Attack");
        this.Effects[1] = new DragonsWrathSecondEffect();
    }
}


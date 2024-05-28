using ConsoleApp1.DataTypes;

namespace Fire_Emblem_Model;

public class DragonsWrath : Skill
{
    public DragonsWrath() : base()
    {
        this.Conditions = new Condition[2];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Conditions[1] = new CompareMyAtkWithOpponentsResCondition();
        this.Effects = new Effect[2];
        this.Effects[0] = new PercentualDamageReductionEffect(0.75, DamageEffectCategory.FirstAttack);
        this.Effects[1] = new DragonsWrathSecondEffect();
    }
}


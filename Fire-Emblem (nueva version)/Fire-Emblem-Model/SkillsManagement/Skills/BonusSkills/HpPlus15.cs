namespace Fire_Emblem_Model;

public class HpPlus15 : Skill
{
    public HpPlus15() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrueCondition();
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeHpInEffect(15);
    }
}
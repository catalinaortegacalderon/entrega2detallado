namespace Fire_Emblem_Model;

public class Boost : Skill
{
    public Boost() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyHpIsLessThanOpponentsHpPlusCondition(3); 
            
        this.Effects = new Effect[1];
    }
}
namespace Fire_Emblem_Model;

public class DragonWall : Skill
{
    public DragonWall() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new CompareTotalRes(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReductionDeterminedByStatDifference("Res", 4); 
    }
}
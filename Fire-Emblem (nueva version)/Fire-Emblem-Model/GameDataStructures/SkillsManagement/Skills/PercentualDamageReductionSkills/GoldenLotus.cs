namespace Fire_Emblem_Model;

public class GoldenLotus :  Skill
{
    public GoldenLotus() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentUsesCertainWeapon(["Sword", "Axe", "Lance", "Bow"]); 
        this.Effects = new Effect[1];
        this.Effects[0] = new PercentualDamageReduction(0.5,"First Attack"); 
    }
}

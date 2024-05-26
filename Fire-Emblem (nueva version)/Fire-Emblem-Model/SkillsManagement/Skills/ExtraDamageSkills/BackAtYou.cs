namespace Fire_Emblem_Model;

public class BackAtYou : Skill
{ 
    public BackAtYou() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new OpponentStartsCombatCondition(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new BackAtYouEffect(); 
    }
}
namespace Fire_Emblem.ExtraDamageSkills;

public class Bravery : Skill
{ 
    public Bravery() : base() 
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new AlwaysTrue(); 
        this.Effects = new Effect[1];
        this.Effects[0] = new ExtraDamageReduction(5, "All"); 
    }
}
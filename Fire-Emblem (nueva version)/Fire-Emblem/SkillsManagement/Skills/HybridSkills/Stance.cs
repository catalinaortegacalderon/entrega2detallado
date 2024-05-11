namespace Fire_Emblem;

// ARREGLAR NOMBRES DE PARAMS QUE RECIBE LA FUNCION

// TAMBIEN USO PARA POSTURE, TAL VEZ CAMBIAR ESTO

public class Stance : Skill
{
    public Stance(String statThatChanges1, string statThatChanges2, int amount1, int amount2) : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new OpponentStartsCombat();
        this.Conditions[1] = new OpponentStartsCombat();
        this.Conditions[2] = new OpponentStartsCombat();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsIn(statThatChanges1, amount1); 
        this.Effects[1] = new ChangeStatsIn(statThatChanges2, amount2); 
        this.Effects[2] = new PercentualDamageReduction(0.9, "Followup"); 
    }
}
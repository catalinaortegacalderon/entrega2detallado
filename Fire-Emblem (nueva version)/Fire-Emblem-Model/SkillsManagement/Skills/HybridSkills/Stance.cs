using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.DamageEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

// ARREGLAR NOMBRES DE PARAMS QUE RECIBE LA FUNCION

// TAMBIEN USO PARA POSTURE, TAL VEZ CAMBIAR ESTO

public class Stance : Skill
{
    public Stance(StatType statThatChanges1, StatType statThatChanges2, int amount1, int amount2) : base()
    {
        this.Conditions = new Condition[3];
        this.Conditions[0] = new OpponentStartsCombatCondition();
        this.Conditions[1] = new OpponentStartsCombatCondition();
        this.Conditions[2] = new OpponentStartsCombatCondition();
        this.Effects = new Effect[3];
        this.Effects[0] = new ChangeStatsInEffect(statThatChanges1, amount1); 
        this.Effects[1] = new ChangeStatsInEffect(statThatChanges2, amount2); 
        this.Effects[2] = new PercentualDamageReductionEffect(0.9, DamageEffectCategory.FollowUp); 
    }
}
using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class DeathBlow : Skill
{
    public DeathBlow() : base()
    {
        this.Conditions = new Condition[1];
        this.Conditions[0] = new MyUnitStartsCombatCondition();
            
        this.Effects = new Effect[1];
        this.Effects[0] = new ChangeStatsInEffect(StatType.Atk, 8);
    }
}
using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;

public class ArmoredBlowSkill : Skill
{
    public ArmoredBlowSkill() : base()
    {
        this.Conditions = new Condition[] { new MyUnitStartsCombatCondition() };
        
        this.Effects = new Effect[] { new ChangeStatsInEffect(StatType.Def, 8) };
    }
}
using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Conditions.FirstCategoryConditions;
using ConsoleApp1.SkillsManagement.Effects.BonusAndPenaltiesEffects;
using ConsoleApp1.SkillsManagement.Effects.NeutralizationEffects;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.SkillsManagement.Skills.HybridSkills;

public class DragonSkinSkill : Skill
{
    public DragonSkinSkill() : base()
    {
        this.Conditions = new Condition[5];
        this.Conditions[0] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
            new OpponentStartsCombatCondition()]);
        this.Conditions[1] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
            new OpponentStartsCombatCondition()]);
        this.Conditions[2] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
            new OpponentStartsCombatCondition()]);
        this.Conditions[3] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
            new OpponentStartsCombatCondition()]);
        this.Conditions[4] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
            new OpponentStartsCombatCondition()]);
            
        this.Effects = new Effect[5];
        this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
        this.Effects[1] = new ChangeStatsInEffect( StatType.Spd, 6); 
        this.Effects[2] = new ChangeStatsInEffect( StatType.Def, 6);
        this.Effects[3] = new ChangeStatsInEffect( StatType.Res, 6);
        this.Effects[4] = new NeutralizeOpponentsBonusEffect();
    }
}
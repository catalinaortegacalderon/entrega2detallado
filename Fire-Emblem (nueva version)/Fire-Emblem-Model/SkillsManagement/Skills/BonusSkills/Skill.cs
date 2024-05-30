using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;

namespace ConsoleApp1.SkillsManagement.Skills.BonusSkills;


    public abstract class Skill
    {
        protected Condition[] Conditions;
        protected Effect[] Effects;
        
        public Condition GetCondition(int index)
        {
            return this.Conditions[index];
        }
        
        public Effect GetEffect(int index)
        {
            return this.Effects[index];
        }

        public int GetConditionLength()
        {
            return this.Conditions.Length;
        }
        
    }
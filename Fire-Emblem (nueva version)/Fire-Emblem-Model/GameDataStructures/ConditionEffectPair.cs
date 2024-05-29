using System.Diagnostics;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.GameDataStructures;

public class ConditionEffectPair
{
    public Condition Condition;
    public Effect Effect;
    public Unit UnitThatHasThePair;
    public Unit OpponentsUnit;

    public ConditionEffectPair(Unit unitThatHasThePair, Unit opponentsUnit, Skill skill,  int pairIndex)
    {
        this.UnitThatHasThePair = unitThatHasThePair;
        this.OpponentsUnit = opponentsUnit;
        this.Condition = skill.Conditions[pairIndex];
        this.Effect = skill.Effects[pairIndex];
    }
}
using System.Diagnostics;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.PenaltySkills;

namespace ConsoleApp1.GameDataStructures;

public class ConditionEffectPair
{
    public readonly Condition Condition;
    public readonly Effect Effect;
    public readonly Unit UnitThatHasThePair;
    public readonly Unit OpponentsUnit;

    public ConditionEffectPair(Unit unitThatHasThePair, Unit opponentsUnit, Skill skill,  int pairIndex)
    {
        this.UnitThatHasThePair = unitThatHasThePair;
        this.OpponentsUnit = opponentsUnit;
        this.Condition = skill.GetCondition(pairIndex);
        this.Effect = skill.GetEffect(pairIndex);
        ManageDivineRecreationsSpecialCase();
    }

    private void ManageDivineRecreationsSpecialCase()
    {
        if (Effect is DivineRecreationEffect && UnitThatHasThePair.StartedTheRound == true)
        {
            Condition.ChangePriorityBecauseEffectPriorityIsBigger(5);
        }
    }
}
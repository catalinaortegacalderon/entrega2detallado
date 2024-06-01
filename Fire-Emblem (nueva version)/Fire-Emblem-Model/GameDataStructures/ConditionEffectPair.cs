using ConsoleApp1.DataTypes;
using ConsoleApp1.SkillsManagement.Conditions.BaseConditions;
using ConsoleApp1.SkillsManagement.Effects.SpecificSkillEffects;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;

namespace ConsoleApp1.GameDataStructures;

public class ConditionEffectPair
{
    public readonly Condition Condition;
    public readonly Effect Effect;
    public readonly Unit OpponentsUnit;
    public readonly Unit UnitThatHasThePair;

    public ConditionEffectPair(Unit unitThatHasThePair, Unit opponentsUnit, Skill skill, int pairIndex)
    {
        // todo: sacar this
        UnitThatHasThePair = unitThatHasThePair;
        OpponentsUnit = opponentsUnit;
        Condition = skill.GetCondition(pairIndex);
        Effect = skill.GetEffect(pairIndex);
        ManageDivineRecreationsSpecialCase();
    }

    private void ManageDivineRecreationsSpecialCase()
    {
        if (Effect is DivineRecreationEffect && UnitThatHasThePair.StartedTheRound)
            Condition.ChangePriorityBecauseEffectPriorityIsBigger(ConditionPriority
                .PriorityOfDivineRecreationWhenUnitBeginsCombat);
        if (Effect is DivineRecreationEffect && OpponentsUnit.StartedTheRound)
            Condition.ChangePriorityBecauseEffectPriorityIsBigger(ConditionPriority
                .PriorityOfDivineRecreationWhenOpponentBeginsCombat);
    }
}
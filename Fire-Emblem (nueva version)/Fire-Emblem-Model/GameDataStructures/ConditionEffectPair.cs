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
        if (Effect is DivineRecreationEffect && UnitThatHasThePair.StartedTheRound)
        {
            Console.WriteLine("maneje el caso especial, aumentando prioridad de " + UnitThatHasThePair.Name);
            Condition.ChangePriorityBecauseEffectPriorityIsBigger(5);
        }
        if (Effect is DivineRecreationEffect && OpponentsUnit.StartedTheRound)
        {
            // aca en verdad la estoy reiniciando
            Console.WriteLine("maneje el caso especial, aumentando prioridad de " + UnitThatHasThePair.Name);
            Condition.ChangePriorityBecauseEffectPriorityIsBigger(4);
        }
    }
}
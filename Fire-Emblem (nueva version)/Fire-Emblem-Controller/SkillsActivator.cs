using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem;

public class SkillsActivator
{
    
    public static void ActivateSkills(Unit atackingUnit, Unit defensiveUnit)
    {
        ConditionEffectPairsList conditionEffectPairs = GetAllConditionEffectPairs(atackingUnit, defensiveUnit);
        conditionEffectPairs.Prioritize();
        ApplyAllValidEffects(conditionEffectPairs);
        Unit hello;
    }

    // todo: encapsular lista
    private static ConditionEffectPairsList GetAllConditionEffectPairs(Unit attackingUnit, Unit defensiveUnit)
    {
        var conditionEffectPairs = new ConditionEffectPairsList();
        foreach (var skill in attackingUnit.Skills)
            for (var i = 0; i < skill.GetConditionLength(); i++)
                conditionEffectPairs.AddConditionEffectPair(new ConditionEffectPair(attackingUnit,
                    defensiveUnit, skill, i));
        foreach (var skill in defensiveUnit.Skills)
            for (var i = 0; i < skill.GetConditionLength(); i++)
                conditionEffectPairs.AddConditionEffectPair(new ConditionEffectPair(defensiveUnit,
                    attackingUnit, skill, i));
        return conditionEffectPairs;
    }
    

    // todo: encapsular
    private static void ApplyAllValidEffects(ConditionEffectPairsList prioritizedList)
    {
        foreach (var conditionEffectPair in prioritizedList)
            if (conditionEffectPair.Condition.DoesItHold(conditionEffectPair.UnitThatHasThePair,
                    conditionEffectPair.OpponentsUnit))
                conditionEffectPair.Effect.ApplyEffect(conditionEffectPair.UnitThatHasThePair,
                    conditionEffectPair.OpponentsUnit);
    }
}
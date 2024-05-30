namespace ConsoleApp1.GameDataStructures;

public static class DataStructuresResetter
{
    
    public static void ResetBonusPenaltiesAndNeutralizatorsToASpecificValue(BonusPenaltiesAndNeutralizators dataStructure, 
        int valueToResetTo)
    {
        dataStructure.Attk = valueToResetTo;
        dataStructure.Spd = valueToResetTo;
        dataStructure.Def = valueToResetTo;
        dataStructure.Res = valueToResetTo;
        dataStructure.AtkFollowup = valueToResetTo;
        dataStructure.AtkFirstAttack = valueToResetTo;
        dataStructure.DefFirstAttack = valueToResetTo;
        dataStructure.ResFirstAttack = valueToResetTo;
    }
    
    public static void ResetDamageGameStructure(DataStructureDamageEffects dataStructure)
    {
        dataStructure.ExtraDamage = 0;
        dataStructure.ExtraDamageFirstAttack = 0;
        dataStructure.ExtraDamageFollowup = 0;
        dataStructure.PercentageReduction = 1;
        dataStructure.PercentageReductionOpponentsFirstAttack = 1;
        dataStructure.PercentageReductionOpponentsFollowup = 1;
        dataStructure.AbsolutDamageReduction = 0;
    }
    
}
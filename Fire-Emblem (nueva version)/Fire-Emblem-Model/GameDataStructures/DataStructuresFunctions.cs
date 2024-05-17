namespace Fire_Emblem_Model;

public class DataStructuresFunctions
{
    public static BonusPenaltiesAndNeutralizations CreateStructure(int startValuesIn)
    {
        BonusPenaltiesAndNeutralizations dataStructure = new BonusPenaltiesAndNeutralizations();
        dataStructure.Attk = startValuesIn;
        dataStructure.Spd = startValuesIn;
        dataStructure.Def = startValuesIn;
        dataStructure.Res = startValuesIn;
        dataStructure.AtkFollowup = startValuesIn;
        dataStructure.AtkFirstAttack = startValuesIn;
        dataStructure.DefFirstAttack = startValuesIn;
        dataStructure.ResFirstAttack = startValuesIn;
        return dataStructure;
    }
    
    public static void SetStructureTo(BonusPenaltiesAndNeutralizations dataStructure, int startValuesIn)
    {
        dataStructure.Attk = startValuesIn;
        dataStructure.Spd = startValuesIn;
        dataStructure.Def = startValuesIn;
        dataStructure.Res = startValuesIn;
        dataStructure.AtkFollowup = startValuesIn;
        dataStructure.AtkFirstAttack = startValuesIn;
        dataStructure.DefFirstAttack = startValuesIn;
        dataStructure.ResFirstAttack = startValuesIn;
    }
    
    public static void ResetDamageStructure(DataStructureDamageEffects dataStructure)
    {
        dataStructure.ExtraDamage = 0;
        dataStructure.ExtraDamageFirstAttack = 0;
        dataStructure.ExtraDamageFollowup = 0;
        dataStructure.PorcentualReduction = 1;
        dataStructure.PorcentualReductionRivalsFirstAttack = 1;
        dataStructure.PorcentualReductionRivalsFollowup = 1;
        dataStructure.AbsolutDamageReduction = 0;
    }
    
}
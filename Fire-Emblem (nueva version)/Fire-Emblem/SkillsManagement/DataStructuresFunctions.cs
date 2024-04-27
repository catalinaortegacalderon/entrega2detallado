namespace Fire_Emblem;

public class DataStructuresFunctions
{
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
}
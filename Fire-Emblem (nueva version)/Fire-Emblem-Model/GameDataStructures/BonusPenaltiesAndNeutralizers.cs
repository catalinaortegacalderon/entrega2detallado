namespace ConsoleApp1.GameDataStructures;

public class BonusPenaltiesAndNeutralizers
{
    
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public int AtkFollowup;
    public int AtkFirstAttack;
    public int DefFirstAttack;
    public int ResFirstAttack;
    public bool HpBonusActivated = false;
    
    public BonusPenaltiesAndNeutralizers(int startValuesIn)
    {
        Atk = startValuesIn;
        Spd = startValuesIn;
        Def = startValuesIn;
        Res = startValuesIn;
        AtkFollowup = startValuesIn;
        AtkFirstAttack = startValuesIn;
        DefFirstAttack = startValuesIn;
        ResFirstAttack = startValuesIn;
    }
}
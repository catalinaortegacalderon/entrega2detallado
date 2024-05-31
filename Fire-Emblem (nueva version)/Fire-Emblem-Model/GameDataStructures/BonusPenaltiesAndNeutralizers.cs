namespace ConsoleApp1.GameDataStructures;

public class BonusPenaltiesAndNeutralizers
{
    // todo: sacar this
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
        this.Atk = startValuesIn;
        this.Spd = startValuesIn;
        this.Def = startValuesIn;
        this.Res = startValuesIn;
        this.AtkFollowup = startValuesIn;
        this.AtkFirstAttack = startValuesIn;
        this.DefFirstAttack = startValuesIn;
        this.ResFirstAttack = startValuesIn;
    }
}
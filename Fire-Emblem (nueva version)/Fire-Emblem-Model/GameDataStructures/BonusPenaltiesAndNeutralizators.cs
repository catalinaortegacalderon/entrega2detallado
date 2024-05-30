namespace ConsoleApp1.GameDataStructures;

public class BonusPenaltiesAndNeutralizators
{
    public int Attk;
    public int Spd;
    public int Def;
    public int Res;
    public int AtkFollowup;
    public int AtkFirstAttack;
    public int DefFirstAttack;
    public int ResFirstAttack;
    public bool HpBonusActivated = false;
    
    public BonusPenaltiesAndNeutralizators(int startValuesIn)
    {
        this.Attk = startValuesIn;
        this.Spd = startValuesIn;
        this.Def = startValuesIn;
        this.Res = startValuesIn;
        this.AtkFollowup = startValuesIn;
        this.AtkFirstAttack = startValuesIn;
        this.DefFirstAttack = startValuesIn;
        this.ResFirstAttack = startValuesIn;
    }
}
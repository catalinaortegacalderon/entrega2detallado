using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class BonusPenaltiesAndNeutralizations
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

    public BonusPenaltiesAndNeutralizations(int startValuesIn)
    {
        if (startValuesIn == 0)
        {
            this.Attk = 0;
            this.Spd = 0;
            this.Def = 0;
            this.Res = 0;
            this.AtkFollowup = 0;
            this.AtkFirstAttack = 0;
            this.DefFirstAttack = 0;
            this.ResFirstAttack = 0;
        }

        if (startValuesIn == 1)
        {
            this.Attk = 1;
            this.Spd = 1;
            this.Def = 1;
            this.Res = 1;
            this.AtkFollowup = 1;
            this.AtkFirstAttack = 1;
            this.DefFirstAttack = 1;
            this.ResFirstAttack = 1;
        }
        
    }
    public void ResetStructureToZero()
    {
        this.Attk = 0;
        this.Spd = 0;
        this.Def = 0;
        this.Res = 0;
        this.AtkFollowup = 0;
        this.AtkFirstAttack = 0;
        this.DefFirstAttack = 0;
        this.ResFirstAttack = 0;
    }
    
    public void ResetStructureToOne()
    {
        this.Attk = 1;
        this.Spd = 1;
        this.Def = 1;
        this.Res = 1;
        this.AtkFollowup = 1;
        this.AtkFirstAttack = 1;
        this.DefFirstAttack = 1;
        this.ResFirstAttack = 1;
    }
}
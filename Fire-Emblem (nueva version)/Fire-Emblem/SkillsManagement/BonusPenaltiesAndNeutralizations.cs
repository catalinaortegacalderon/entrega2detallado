using System.Runtime.CompilerServices;

namespace Fire_Emblem;

public class BonusPenaltiesAndNeutralizations
{
    public int attk;
    public int spd;
    public int def;
    public int res;
    public int atkFollowup;
    public int atkFirstAttack;
    public int defFirstAttack;
    public int resFirstAttack;
    public bool hpBonusActivated = false;

    public BonusPenaltiesAndNeutralizations(int startValuesIn)
    {
        if (startValuesIn == 0)
        {
            this.attk = 0;
            this.spd = 0;
            this.def = 0;
            this.res = 0;
            this.atkFollowup = 0;
            this.atkFirstAttack = 0;
            this.defFirstAttack = 0;
            this.resFirstAttack = 0;
        }

        if (startValuesIn == 1)
        {
            this.attk = 1;
            this.spd = 1;
            this.def = 1;
            this.res = 1;
            this.atkFollowup = 1;
            this.atkFirstAttack = 1;
            this.defFirstAttack = 1;
            this.resFirstAttack = 1;
        }
        
    }
    public void ResetStructureToZero()
    {
        this.attk = 0;
        this.spd = 0;
        this.def = 0;
        this.res = 0;
        this.atkFollowup = 0;
        this.atkFirstAttack = 0;
        this.defFirstAttack = 0;
        this.resFirstAttack = 0;
    }
    
    public void ResetStructureToOne()
    {
        this.attk = 1;
        this.spd = 1;
        this.def = 1;
        this.res = 1;
        this.atkFollowup = 1;
        this.atkFirstAttack = 1;
        this.defFirstAttack = 1;
        this.resFirstAttack = 1;
    }
}
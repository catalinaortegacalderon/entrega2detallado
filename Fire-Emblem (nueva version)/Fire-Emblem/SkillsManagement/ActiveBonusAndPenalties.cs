namespace Fire_Emblem;

public class ActiveBonusAndPenalties
{
    public int attk;
    public int spd;
    public int def;
    public int res;
    public int atkFollowup;

    public ActiveBonusAndPenalties()
    {
        this.attk = 0;
        this.spd = 0;
        this.def = 0;
        this.res = 0;
    }

    public void ReestablecerBonusACero()
    {
        this.attk = 0;
        this.spd = 0;
        this.def = 0;
        this.res = 0;
        this.atkFollowup = 0;
    }

}
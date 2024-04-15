namespace Fire_Emblem;

public class BonusActivos
{
    public int attk;
    public int spd;
    public int def;
    public int res;

    public BonusActivos()
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
    }

}
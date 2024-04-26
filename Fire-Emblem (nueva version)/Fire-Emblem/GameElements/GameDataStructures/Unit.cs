using System.Runtime.CompilerServices;
using Fire_Emblem_View;

namespace Fire_Emblem;


public class Unit
{
    public View view;
    public string name = "";
    public string weapon;
    public string gender;
    public int hpMax;
    public int currentHp = 0;
    public int attk;
    public int spd;
    public int def;
    public int res;
    public BonusPenaltiesAndNeutralizations activeBonus = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations activePenalties = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations activeBonusNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public BonusPenaltiesAndNeutralizations activePenaltiesNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public GameLogs gameLogs = new GameLogs();
    public Skill[] skills = new Skill[] { new EmptySkill(), new EmptySkill() };
    
    public void Setear_valores(string nombre, string arma, string genero, int hp_actual,int hp_max, int attk, int spd, int def, int res, View view)
    {
        this.name = nombre;
        this.weapon = arma;
        this.gender = genero;
        this.hpMax = hp_max;
        this.currentHp = hp_actual;
        this.attk = attk;
        this.spd = spd;
        this.def = def;
        this.res = res;
        this.view = view;
    }
}
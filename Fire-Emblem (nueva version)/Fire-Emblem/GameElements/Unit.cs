using System.Runtime.CompilerServices;
using Fire_Emblem_View;

namespace Fire_Emblem;


public class Unit
{
    public View view;
    public string nombre = "";
    public string arma;
    public string genero;
    public int hp_max;
    public int hp_actual = 0;
    public int attk;
    public int spd;
    public int def;
    public int res;
    // tal ves todos los de aca abajo pueden ser igual
    public BonusPenaltiesAndNeutralizations activeBonus = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations activePenalties = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations activeBonusNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public BonusPenaltiesAndNeutralizations activePenaltiesNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public GameLogs gameLogs = new GameLogs();
    public Skill[] habilidades = new Skill[2];
    
    public  void Setear_valores(string nombre, string arma, string genero, int hp_actual,int hp_max, int attk, int spd, int def, int res, View view)
    {
        this.nombre = nombre;
        this.arma = arma;
        this.genero = genero;
        this.hp_max = hp_max;
        this.hp_actual = hp_actual;
        this.attk = attk;
        this.spd = spd;
        this.def = def;
        this.res = res;
        this.view = view;
    }

    public Unit()
    {
        // Si una unidad es vacía, su nombre es vacío
        this.nombre = "";
        this.hp_actual = 0;
        this.habilidades[0] = new EmptySkill(this.view);
        this.habilidades[1] = new EmptySkill(this.view);
    }
}
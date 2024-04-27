using System.Runtime.CompilerServices;
using Fire_Emblem_View;

namespace Fire_Emblem;


public class Unit
{
    public string Name = "";
    public string Weapon;
    public string Gender;
    public int HpMax;
    public int CurrentHp = 0;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public BonusPenaltiesAndNeutralizations ActiveBonus = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations ActivePenalties = new BonusPenaltiesAndNeutralizations(0);
    public BonusPenaltiesAndNeutralizations ActiveBonusNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public BonusPenaltiesAndNeutralizations ActivePenaltiesNeutralization = new BonusPenaltiesAndNeutralizations(1);
    public GameLogs GameLogs = new GameLogs();
    public Skill[] Skills = new Skill[] { new EmptySkill(), new EmptySkill() };
}
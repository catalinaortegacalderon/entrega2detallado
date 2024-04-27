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
    public BonusPenaltiesAndNeutralizations ActiveBonus = DataStructuresFunctions.CreateStructure(0);
    public BonusPenaltiesAndNeutralizations ActivePenalties = DataStructuresFunctions.CreateStructure(0);
    public BonusPenaltiesAndNeutralizations ActiveBonusNeutralization = DataStructuresFunctions.CreateStructure(1);
    public BonusPenaltiesAndNeutralizations ActivePenaltiesNeutralization = DataStructuresFunctions.CreateStructure(1);
    public GameLogs GameLogs = new GameLogs();
    public Skill[] Skills = new Skill[] { new EmptySkill(), new EmptySkill() };
}
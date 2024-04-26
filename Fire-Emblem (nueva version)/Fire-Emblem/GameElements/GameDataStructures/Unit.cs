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
    
}
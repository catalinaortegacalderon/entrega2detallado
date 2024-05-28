using Fire_Emblem_Model.DataTypes;
using Fire_Emblem_Model.GameDataStructures.Lists;

namespace Fire_Emblem_Model;



public class Unit
{
    public string Name = "";
    public Weapon Weapon;
    public Gender Gender;
    public int HpMax;
    public int CurrentHp = 0;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public bool IsAttacking;
    public BonusPenaltiesAndNeutralizations ActiveBonus = DataStructuresFunctions.CreateStructure(0);
    public BonusPenaltiesAndNeutralizations ActivePenalties = DataStructuresFunctions.CreateStructure(0);
    public BonusPenaltiesAndNeutralizations ActiveBonusNeutralization = DataStructuresFunctions.CreateStructure(1);
    public BonusPenaltiesAndNeutralizations ActivePenaltiesNeutralization = DataStructuresFunctions.CreateStructure(1);
    public DataStructureDamageEffects DamageEffects = new DataStructureDamageEffects();
    public string LastOpponentName = "";
    public bool HasStartedACombat = false;
    public bool HasBeenBeenInACombatStartedByTheOpponent = false;
    public bool StartedTheRound;
    //public GameLogs GameLogs = new GameLogs();
    public SkillsList Skills = new SkillsList();
    //public Skill[] Skills = new Skill[] { new EmptySkill(), new EmptySkill() };
}
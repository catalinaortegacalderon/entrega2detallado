using ConsoleApp1.DataTypes;
using ConsoleApp1.EncapsulatedLists;

namespace ConsoleApp1.GameDataStructures;



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
    
    public SkillsList Skills = new SkillsList();

    public BonusPenaltiesAndNeutralizators ActiveBonus = new BonusPenaltiesAndNeutralizators(0);
    public BonusPenaltiesAndNeutralizators ActivePenalties = new BonusPenaltiesAndNeutralizators(0);
    public BonusPenaltiesAndNeutralizators ActiveBonusNeutralizator = 
        new BonusPenaltiesAndNeutralizators(1);
    public BonusPenaltiesAndNeutralizators ActivePenaltiesNeutralizator = 
        new BonusPenaltiesAndNeutralizators(1);
    public DataStructureDamageEffects DamageEffects = new DataStructureDamageEffects();
    
    public string LastOpponentName = "";
    public bool HasStartedACombat = false;
    public bool HasBeenBeenInACombatStartedByTheOpponent = false;
    public bool StartedTheRound;
    public bool IsAttacking;

}
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

    public Unit()
    {
    }
    
    public Unit(string name, string weapon, string gender, 
        int currentHp,int maxHp, int attk, int spd, int def, int res)
    {
        this.Name = name;
        this.Weapon = ConvertWeaponStringToWeaponType(weapon);
        if (gender == "Male")
        {
            this.Gender = Gender.Male;
        }
        else
        {
            this.Gender = Gender.Female;
        }
        this.HpMax = maxHp;
        this.CurrentHp = currentHp;
        this.Atk = attk;
        this.Spd = spd;
        this.Def = def;
        this.Res = res;
    }

    private static Weapon ConvertWeaponStringToWeaponType(string weapon)
    {
        if (weapon == "Magic")
        {
            return Weapon.Magic;
        }
        if (weapon == "Axe")
        {
            return Weapon.Axe;
        }
        if (weapon == "Lance")
        {
            return Weapon.Lance;
        }
        if (weapon == "Bow")
        {
            return Weapon.Bow;
        }
        if (weapon == "Sword")
        {
            return Weapon.Sword;
        }
        return Weapon.Empty;
    }

}
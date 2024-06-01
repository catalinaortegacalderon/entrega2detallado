using ConsoleApp1.DataTypes;
using ConsoleApp1.EncapsulatedLists;

namespace ConsoleApp1.GameDataStructures;

public class Unit
{
    public readonly BonusPenaltiesAndNeutralizers ActiveBonus = new(0);
    public readonly BonusPenaltiesAndNeutralizers ActiveBonusNeutralizer = new(1);
    public readonly BonusPenaltiesAndNeutralizers ActivePenalties = new(0);
    public readonly BonusPenaltiesAndNeutralizers ActivePenaltiesNeutralizer = new(1);
    public readonly int Atk;
    public readonly DataStructureDamageEffects DamageEffects = new();
    public readonly int Def;
    public readonly Gender Gender;
    public readonly string Name = "";
    public readonly int Res;

    public readonly SkillsList Skills = new();
    public readonly int Spd;
    public readonly Weapon Weapon;
    public int CurrentHp;
    public bool HasBeenBeenInACombatStartedByTheOpponent = false;
    public bool HasStartedACombat = false;
    public int HpMax;
    public bool IsAttacking;

    public string LastOpponentName = "";
    public bool StartedTheRound;

    public Unit()
    {
    }

    public Unit(string name, string weapon, string gender,
        int currentHp, int maxHp, int atk, int spd, int def, int res)
    {
        Name = name;
        Weapon = ConvertWeaponStringToWeaponType(weapon);
        if (gender == "Male")
            Gender = Gender.Male;
        else
            Gender = Gender.Female;
        HpMax = maxHp;
        CurrentHp = currentHp;
        Atk = atk;
        Spd = spd;
        Def = def;
        Res = res;
    }

    private static Weapon ConvertWeaponStringToWeaponType(string weapon)
    {
        return weapon switch
        {
            "Magic" => Weapon.Magic,
            "Axe" => Weapon.Axe,
            "Lance" => Weapon.Lance,
            "Bow" => Weapon.Bow,
            "Sword" => Weapon.Sword,
            _ => Weapon.Empty
        };
    }
}
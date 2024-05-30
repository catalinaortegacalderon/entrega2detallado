using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement;
using Fire_Emblem_View;
using System.Text.Json;

namespace Fire_Emblem;

public class GameAttacksControllerBuilder
{
    public static GameAttacksController BuildGameController(string file, GameView view)
    {
        int[] unitCounters = new int[] {0, 0};
        int currentPlayer = 0;
        var units = new Unit[2][];
        units[0] = new Unit[] { new Unit(), new Unit(), new Unit() };
        units[1] = new Unit[] { new Unit(), new Unit(), new Unit() };
        
        CreateUnitsAndSkills(file, currentPlayer, units, unitCounters);
        
        Player[] players = CreatePlayers(unitCounters, units);
        
        return new GameAttacksController(players[0], players[1], view);
    }

    private static void CreateUnitsAndSkills(string file, int currentPlayer, Unit[][] units, int[] unitCounters)
    {
        // todo: arreglar identacion, hay tres niveles
        string[] allLines = File.ReadAllLines(file);
        foreach (string line in allLines)
        {
            if (line == "Player 1 Team") currentPlayer = 0;
            else if (line == "Player 2 Team") currentPlayer = 1;
            else
            {
                var unitInfo = CreateUnits(line, units, currentPlayer, unitCounters);
                if (unitInfo.Length > 1) CreateSkills(units, currentPlayer, unitCounters,
                    unitInfo[1].Split(new char[] { ',' }, 
                        StringSplitOptions.RemoveEmptyEntries));
                unitCounters[currentPlayer]++;
            }
        }
    }

    private static string[] CreateUnits(string line, Unit[][] units, int currentPlayer, int[] unitCounters)
    {
        string[] unitInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        string unitsName = unitInfo[0].Replace(" ", "");
        string stringOfAllUnits = File.ReadAllText("characters.json");
        var jsonOfAllUnits = JsonSerializer.Deserialize<List<JsonUnit>>(stringOfAllUnits);
        foreach (var unit in jsonOfAllUnits)
        {
            if (unitsName == unit.Name)
            {
                // todo: trainwreck
                SetUnitValues(units[currentPlayer][unitCounters[currentPlayer]], unit.Name,
                    unit.Weapon, unit.Gender, Convert.ToInt32(unit.HP),
                    Convert.ToInt32(unit.HP), Convert.ToInt32(unit.Atk), Convert.ToInt32(unit.Spd),
                    Convert.ToInt32(unit.Def), Convert.ToInt32(unit.Res));
            }
        }
        return unitInfo;
    }

    private static void CreateSkills( Unit[][] listOfThePlayersUnits, int currentPlayer, 
        int[] unitCounters, string[] listOfSkillNames)
    {
        int skillsCounter = 0;
        foreach (string skillName in listOfSkillNames)
        {
            SkillConstructor.Construct(listOfThePlayersUnits, currentPlayer, unitCounters, skillName, skillsCounter);
            skillsCounter++;
        }
    }
    
    private static void SetUnitValues(Unit unit, string name, string weapon, string gender, 
        int currentHp,int maxHp, int attk, int spd, int def, int res)
    {
        unit.Name = name;
        unit.Weapon = ConvertWeaponStringToWeaponType(weapon);
        if (gender == "Male")
        {
            unit.Gender = Gender.Male;
        }
        else
        {
            unit.Gender = Gender.Female;
        }
        unit.HpMax = maxHp;
        unit.CurrentHp = currentHp;
        unit.Atk = attk;
        unit.Spd = spd;
        unit.Def = def;
        unit.Res = res;
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
    
    private static Player[] CreatePlayers(int[] unitCounters, Unit[][] units)
    {
        
        Player player1 = new Player();
        Player player2 = new Player();
        
        player1.AmountOfUnits = unitCounters[0];
        player1.PlayerNumber = 0;
        player2.AmountOfUnits = unitCounters[1];
        player2.PlayerNumber = 1;
        
        int unitCounterplayer1 = 0;
        foreach (var unit in units[0])
        {
            player1.Units.AddUnit(unitCounterplayer1, unit);
            unitCounterplayer1++;
        }
        int unitCounterplayer2 = 0;
        foreach (var unit in units[1])
        {
            player2.Units.AddUnit(unitCounterplayer2, unit);
            unitCounterplayer2++;
        }
        
        return new Player[]{player1, player2};
    }
}
using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement;
using Fire_Emblem_View;
using System.Text.Json;
using ConsoleApp1.EncapsulatedLists;

namespace Fire_Emblem;

public class GameAttacksControllerBuilder
{
    public static GameAttacksController BuildGameController(string file, GameView view)
    {
        int[] unitCounters = new int[] {0, 0};
        var units = new Unit[2][];
        units[0] = new Unit[] { new Unit(), new Unit(), new Unit() };
        units[1] = new Unit[] { new Unit(), new Unit(), new Unit() };
        
        CreateUnitsAndSkills(file, units, unitCounters);
        
        Player[] players = CreatePlayers(unitCounters, units);
        
        return new GameAttacksController(players[0], players[1], view);
    }

    private static void CreateUnitsAndSkills(string file, Unit[][] units, int[] unitCounters)
    {
        int currentPlayerNumber = 0;
        string[] allLines = File.ReadAllLines(file);
        foreach (string line in allLines)
        {
            if (line == "Player 1 Team") currentPlayerNumber = 0;
            else if (line == "Player 2 Team") currentPlayerNumber = 1;
            else
            {
                var unitsOfThePlayer = units[currentPlayerNumber];
                var playersUnitCounter = unitCounters[currentPlayerNumber];
                var unitInfo = CreateUnits(line, unitsOfThePlayer, playersUnitCounter);
                
                var currentPlayersUnit = unitsOfThePlayer[playersUnitCounter];
                var skills = currentPlayersUnit.Skills;
                
                CreateSkills(skills, unitInfo);
                unitCounters[currentPlayerNumber]++;
            }
        }
    }

    private static string[] CreateUnits(string line, Unit[] unitsListOfTheCurrentPlayer, int unitCounter)
    {
        string[] unitInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        string unitsName = unitInfo[0].Replace(" ", "");
        string stringOfAllUnits = File.ReadAllText("characters.json");
        var jsonOfAllUnits = JsonSerializer.Deserialize<List<JsonUnit>>(stringOfAllUnits);
        foreach (var unit in jsonOfAllUnits)
        {
            if (unitsName == unit.Name)
            {
                
                unitsListOfTheCurrentPlayer[unitCounter] = new Unit(unit.Name,
                    unit.Weapon, unit.Gender, Convert.ToInt32(unit.HP),
                    Convert.ToInt32(unit.HP), Convert.ToInt32(unit.Atk), Convert.ToInt32(unit.Spd),
                    Convert.ToInt32(unit.Def), Convert.ToInt32(unit.Res));
            }
        }
        return unitInfo;
    }

    private static void CreateSkills( SkillsList skills, string[] unitInfo)
    {
        bool unitHasSkills = unitInfo.Length > 1;
        if (!unitHasSkills)
            return;

        var listOfSkillNames = unitInfo[1].Split(new char[] { ',' },
            StringSplitOptions.RemoveEmptyEntries);
        
        int skillsCounter = 0;
        foreach (string skillName in listOfSkillNames)
        {
            
            SkillConstructor.Construct(skills, skillName, skillsCounter);
            skillsCounter++;
        }
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
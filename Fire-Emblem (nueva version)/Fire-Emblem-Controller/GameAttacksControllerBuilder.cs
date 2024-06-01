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
        int[] unitCounters = [0, 0];
        var units = new Unit[2][];
        units[0] = [new Unit(), new Unit(), new Unit()];
        units[1] = [new Unit(), new Unit(), new Unit()];
        
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
            // todo: pasar a funcion
            if (line == "Player 1 Team") 
                currentPlayerNumber = 0;
            else if (line == "Player 2 Team") 
                currentPlayerNumber = 1;
            else
            {
                // todo: pasar a funcion
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
                // todo: pasar a funcion
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
        
        for (var i = 0; i < listOfSkillNames.Length; i++)
        {
            var skillName = listOfSkillNames[i];
            SkillConstructor.Construct(skills, skillName, i);
        }
    }
    
    private static Player[] CreatePlayers(IReadOnlyList<int> unitCounters, IReadOnlyList<Unit[]> units)
    {
        Player player1 = new Player();
        Player player2 = new Player();
        
        player1.AmountOfUnits = unitCounters[0];
        player1.PlayerNumber = 0;
        player2.AmountOfUnits = unitCounters[1];
        player2.PlayerNumber = 1;
        
        // todo: nose si dejarlo con i, arriba tambien esta con i
        for (var i = 0; i < units[0].Length; i++)
        {
            var unit = units[0][i];
            player1.Units.AddUnit(i, unit);
        }
        
        for (var i = 0; i < units[1].Length; i++)
        {
            var unit = units[1][i];
            player2.Units.AddUnit(i, unit);
        }

        return [player1, player2];
    }
}
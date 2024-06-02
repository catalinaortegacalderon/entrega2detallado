using System.Text.Json;
using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class GameAttacksControllerBuilder
{
    private readonly int[] _unitCounters = [0, 0];
    private readonly Unit[][] _units;
    private int _currentPlayerNumber;
    private const int IdOfPlayer1 = 0;
    private const int IdOfPlayer2 = 1;

    public GameAttacksControllerBuilder()
    {
        _units = new Unit[2][];
        _units[0] = [new Unit(), new Unit(), new Unit()];
        _units[1] = [new Unit(), new Unit(), new Unit()];
    }
    
    public GameAttacksController BuildGameController(string[] fileLines, GameView view)
    {
        ProcessFileToCreateUnitsAndSkills(fileLines);
        
        var players = CreatePlayers(_unitCounters, _units);
        
        return new GameAttacksController(players[0], players[1], view);
    }

    private void ProcessFileToCreateUnitsAndSkills(string[] fileLines)
    {
        foreach (var line in fileLines)
            if (line == "Player 1 Team")
                SetCurrentPlayerNumber(IdOfPlayer1);
            else if (line == "Player 2 Team")
                SetCurrentPlayerNumber(IdOfPlayer2);
            else
                CreateUnitsAndSkills(line);
    }

    private void SetCurrentPlayerNumber(int id)
    {
        _currentPlayerNumber = id;
    }

    private void CreateUnitsAndSkills(string line)
    {
        var unitsOfThePlayer = _units[_currentPlayerNumber];
        var playersUnitCounter = _unitCounters[_currentPlayerNumber];
        var unitInfo = CreateUnits(line, unitsOfThePlayer, playersUnitCounter);

        var currentPlayersUnit = unitsOfThePlayer[playersUnitCounter];
        var skills = currentPlayersUnit.Skills;

        CreateSkills(skills, unitInfo);
        _unitCounters[_currentPlayerNumber]++;
    }

    private string[] CreateUnits(string line, Unit[] unitsListOfTheCurrentPlayer, int unitCounter)
    {
        var unitInfo = line.Split(new[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        var unitsName = unitInfo[0].Replace(" ", "");
        var stringOfAllUnits = File.ReadAllText("characters.json");
        var jsonOfAllUnits = JsonSerializer.Deserialize<List<JsonUnit>>(stringOfAllUnits);

        foreach (var unit in jsonOfAllUnits)
            if (unitsName == unit.Name)
                // todo: pasar a funcion
                unitsListOfTheCurrentPlayer[unitCounter] = new Unit(unit.Name,
                    unit.Weapon, unit.Gender, Convert.ToInt32(unit.HP),
                    Convert.ToInt32(unit.HP), Convert.ToInt32(unit.Atk), Convert.ToInt32(unit.Spd),
                    Convert.ToInt32(unit.Def), Convert.ToInt32(unit.Res));
        return unitInfo;
    }

    private static void CreateSkills(SkillsList skills, string[] unitInfo)
    {
        var unitHasSkills = unitInfo.Length > 1;
        if (!unitHasSkills)
            return;

        var listOfSkillNames = unitInfo[1].Split(new[] { ',' },
            StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < listOfSkillNames.Length; i++)
        {
            var skillName = listOfSkillNames[i];
            SkillConstructor.Construct(skills, skillName, i);
        }
    }

    private static Player[] CreatePlayers(IReadOnlyList<int> unitCounters, IReadOnlyList<Unit[]> units)
    {
        var player1 = new Player();
        var player2 = new Player();

        player1.AmountOfUnits = unitCounters[0];
        player2.AmountOfUnits = unitCounters[1];

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
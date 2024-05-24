namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;
using Fire_Emblem_Model;

public class Utils
{
    public static bool CheckIfGameIsValid(string file)
    {
        if (!CheckIfThereAreNoEmptyTeams(file)) return false;
        if (!CheckIfThereAreMaximumThreeUnitsPerTeam(file)) return false;
        if (!CheckIfThereAreNoRepeatedUnits(file)) return false;
        if (!CheckIfThereAreMaxTwoSkillsPerUnit(file)) return false;
        if (!CheckIfThereAreNoRepeatedSkills(file)) return false;
        return true;
    }
    
    private static bool CheckIfThereAreNoEmptyTeams(string file)
    {
        int[] unitsCounter = new int[] {0, 0};
        int curentPlayer = 0; 
        foreach (string line in File.ReadAllLines(file))
        {
            if (line == "Player 1 Team") curentPlayer = 0;
            else if (line == "Player 2 Team") curentPlayer = 1;
            else { unitsCounter[curentPlayer]++; }
        }
        if (unitsCounter[0] == 0 || unitsCounter[1] == 0) return false;
        return true;
    }
        
    private static bool CheckIfThereAreMaximumThreeUnitsPerTeam(string file)
    {
        int[] unitsCounter = new int[] {0, 0};
        int curentPlayer = 0; 
        foreach (string line in File.ReadAllLines(file))
        {
            if (line == "Player 1 Team") curentPlayer = 0;
            else if (line == "Player 2 Team") curentPlayer = 1;
            else { unitsCounter[curentPlayer]++; }
        }
        if (unitsCounter[0] > 3 || unitsCounter[1] > 3) return false;
        return true;
    }
    
    private static bool CheckIfThereAreNoRepeatedUnits(string file)
    {
        int[] unitCounter = new int[] {0, 0};
        int currentPlayer = 0; 
        string[][] units = new string[][] { new string[] { "", "", "" }, new string[] { "", "", "" } };
        foreach (string line in File.ReadAllLines(file))
        {
            if (line == "Player 1 Team") currentPlayer = 0;
            else if (line == "Player 2 Team") currentPlayer = 1;
            else
            {
                string[] arraWithUnitsInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string unitName = arraWithUnitsInfo[0]; 
                if(units[currentPlayer].Contains(unitName)) return false;
                units[currentPlayer][unitCounter[currentPlayer]] = unitName;
                unitCounter[currentPlayer]++;
            }
        }
        return true;
    }
    
    private static bool CheckIfThereAreMaxTwoSkillsPerUnit(string file)
    {
        foreach (string line in File.ReadAllLines(file))
        {
            if (ContainsSkills(line))
            {
                string[] listWithUnitsInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string[] listWithSkillsInfo = listWithUnitsInfo[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (listWithSkillsInfo.Length> 2)return false; 
            }
        }
        return true;
    }
    
    private static bool CheckIfThereAreNoRepeatedSkills(string file)
    {
        foreach (string line in File.ReadAllLines(file))
        {
            if (ContainsTwoSkills(line))
            {
                string[] listWithUnitsInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string[] listWithSkillsInfo = listWithUnitsInfo[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (listWithSkillsInfo[0] == listWithSkillsInfo[1]) return false;
            }
        }
        return true;
    }

    private static bool ContainsSkills(string line)
    {
        return line != "Player 1 Team" && line != "Player 2 Team" && line.Contains('(');
    }
    private static bool ContainsTwoSkills(string line)
    {
        if (ContainsSkills(line)) 
        {   
            string[] listWithUnitsInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] listWithSkillsInfo = listWithUnitsInfo[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return  listWithSkillsInfo.Length == 2;
        }   
        return false;
    }
    
    public static GameAttacksController BuildGameController(string file, View view)
    {
        var unitCounters = InitializeParametersToCreateController(out var currentPlayer, out var units);
        CreateUnitsAndSkills(file, currentPlayer, units, unitCounters);
        var firstPlayer = CreatePlayers(unitCounters, units, out var secondPlayer);
        return new GameAttacksController(firstPlayer, secondPlayer);
    }

    private static Player CreatePlayers(int[] unitCounters, Unit[][] units, out Player jugador2)
    {
        Player jugador1 = new Player();
        jugador1.AmountOfUnits = unitCounters[0];
        jugador1.Units = units[0].ToList();
        jugador2 = new Player();
        jugador2.AmountOfUnits = unitCounters[1];
        jugador2.Units = units[1].ToList();
        return jugador1;
    }

    private static void CreateUnitsAndSkills(string file, int currentPlayer, Unit[][] units, int[] unitCounters)
    {
        string[] allLines = File.ReadAllLines(file);
        foreach (string line in allLines)
        {
            if (line == "Player 1 Team") currentPlayer = 0;
            else if (line == "Player 2 Team") currentPlayer = 1;
            else
            {
                var unitInfo = CreateUnits(line, units, currentPlayer, unitCounters);
                if (unitInfo.Length > 1) CreateSkills(units, currentPlayer, unitCounters, unitInfo[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));
                unitCounters[currentPlayer]++;
            }
        }
    }

    private static int[] InitializeParametersToCreateController(out int currentPlayer, out Unit[][] units)
    {
        int[] unitCounters = new int[] {0, 0};
        currentPlayer = 0;
        units = new Unit[2][];
        units[0] = new Unit[] { new Unit(), new Unit(), new Unit() };
        units[1] = new Unit[] { new Unit(), new Unit(), new Unit() };
        return unitCounters;
    }

    private static string[] CreateUnits(string line, Unit[][] units, int currentPlayer, int[] unitCounters)
    {
        string[] unitInfo = line.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
        string unitsName = unitInfo[0].Replace(" ", "");
        string myJson = File.ReadAllText("characters.json");
        var jsonUnits = JsonSerializer.Deserialize<List<JsonUnit>>(myJson);
        foreach (var unit in jsonUnits)
        {
            if (unitsName == unit.Name)
            {
                SetUnitValues(units[currentPlayer][unitCounters[currentPlayer]], unit.Name,
                    unit.Weapon, unit.Gender, Convert.ToInt32(unit.HP),
                    Convert.ToInt32(unit.HP), Convert.ToInt32(unit.Atk), Convert.ToInt32(unit.Spd),
                    Convert.ToInt32(unit.Def), Convert.ToInt32(unit.Res));
            }
        }
        return unitInfo;
    }

    private static void CreateSkills( Unit[][] unitsList, int currentPlayer, int[] unitCounters, string[] listOfSkillNames)
    {
        int skillsCounter = 0;
        foreach (string skillName in listOfSkillNames)
        {
            SkillConstructor.Construct(unitsList, currentPlayer, unitCounters, skillName, skillsCounter);
            skillsCounter++;
        }
    }
    
    private static void SetUnitValues(Unit unit, string name, string weapon, string gender, int currentHp,int maxHp, int attk, int spd, int def, int res)
    {
        unit.Name = name;
        unit.Weapon = weapon;
        unit.Gender = gender;
        unit.HpMax = maxHp;
        unit.CurrentHp = currentHp;
        unit.Atk = attk;
        unit.Spd = spd;
        unit.Def = def;
        unit.Res = res;
    }
}
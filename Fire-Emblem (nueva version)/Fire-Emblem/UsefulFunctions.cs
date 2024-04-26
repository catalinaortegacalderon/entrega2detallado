namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class UsefulFunctions
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
        int[] contadores_unidades = new int[2];
        contadores_unidades[0] = 0;
        contadores_unidades[1] = 0;
        int jugador_actual = 0;
        Unit[][] unidades = new Unit[2][];
        unidades[0] = new Unit[] { new Unit(), new Unit(), new Unit() };
        unidades[1] = new Unit[] { new Unit(), new Unit(), new Unit() };
        string[] lineas = File.ReadAllLines(file);
        foreach (string linea in lineas)
        {
            if (linea == "Player 1 Team") jugador_actual = 0;
            else if (linea == "Player 2 Team") jugador_actual = 1;
            else
            {
                // obtener nombre unidad
                string[] nuevo_string = linea.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string nombre = nuevo_string[0].Replace(" ", "");
                string myJson = File.ReadAllText("characters.json");
                var players = JsonSerializer.Deserialize<List<JsonUnit>>(myJson);
                foreach (var player in players)
                {
                    if (nombre == player.Name)
                    {
                        SetUnitValues(unidades[jugador_actual][contadores_unidades[jugador_actual]], player.Name,
                            player.Weapon, player.Gender, Convert.ToInt32(player.HP),
                            Convert.ToInt32(player.HP), Convert.ToInt32(player.Atk), Convert.ToInt32(player.Spd),
                            Convert.ToInt32(player.Def), Convert.ToInt32(player.Res),
                            view);
                    }
                }
                // agregar habilidades a la unidad
                if (nuevo_string.Length > 1) CreateSkills(view, nuevo_string, unidades, jugador_actual, contadores_unidades);
                contadores_unidades[jugador_actual]++;
            }
        }

        Player jugador1 = new Player();
        jugador1.amountOfUnits = contadores_unidades[0];
        jugador1.units = unidades[0].ToList();
        Player jugador2 = new Player();
        jugador2.amountOfUnits = contadores_unidades[1];
        jugador2.units = unidades[1].ToList();
        GameAttacksController newGameAttacksController = new GameAttacksController(jugador1, jugador2);

        return newGameAttacksController;
    }

    private static void CreateSkills(View view, string[] nuevo_string, Unit[][] unidades, int jugador_actual,
        int[] contadores_unidades)
    {
        string stringHabilidades = nuevo_string[1];
        string[] listadeHabilidades =
            stringHabilidades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int contador_habilidades = 0;
        foreach (string habilidad in listadeHabilidades)
        {
            SkillConstructor.Construct(unidades, jugador_actual, contadores_unidades, habilidad, contador_habilidades);
            contador_habilidades++;
        }
    }
    
    private static void SetUnitValues(Unit unit, string name, string weapon, string gender, int currentHp,int maxHp, int attk, int spd, int def, int res, View view)
    {
        unit.name = name;
        unit.weapon = weapon;
        unit.gender = gender;
        unit.hpMax = maxHp;
        unit.currentHp = currentHp;
        unit.attk = attk;
        unit.spd = spd;
        unit.def = def;
        unit.res = res;
        unit.view = view;
    }
}
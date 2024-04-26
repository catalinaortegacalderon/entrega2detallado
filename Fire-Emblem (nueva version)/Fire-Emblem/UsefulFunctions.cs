namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class UsefulFunctions
{
    public static bool Juego_Valido(string archivo)
    {
        int[] contadores_unidades = new int[] {0, 0};
        int jugador_actual = 0; 
        string[][] unidades = new string[][] { new string[] { "", "", "" }, new string[] { "", "", "" } };
        string[] lineas = File.ReadAllLines(archivo);
        //if (ThereIsAnEmptyTeam(archivo)) return false;
        foreach (string linea in lineas)
        {
            if (linea == "Player 1 Team")
            {
                jugador_actual = 0;
            }
            else if (linea == "Player 2 Team")
            {
                // revisando si hay equipo vacio
                if (contadores_unidades[0] == 0){
                    return false;
                }
                jugador_actual = 1;
            }
            else
            {
                string[] nuevo_string = linea.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string nombre = nuevo_string[0]; 
                // revisar que no hayan mas de tres unidades
                if(contadores_unidades[jugador_actual] == 3){
                    return false;
                }
                // revisar que no hayan unidades repetidas
                if(unidades[jugador_actual].Contains(nombre)){
                    return false;
                }
                unidades[jugador_actual][contadores_unidades[jugador_actual]] = nombre;
                //contadores_unidades[jugador_actual]++;
                // revisar habilidades
                char caracterABuscar = '(';
                if (linea.Contains(caracterABuscar)){ 
                    nuevo_string = nuevo_string[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    // revisando que no hayan mas de dos habilidades o habilidades rep
                    if (nuevo_string.Length> 2){
                        return false; 
                    }
                    if (nuevo_string.Length == 2)
                    {
                        if (nuevo_string[0] == nuevo_string[1])
                        {
                            return false;
                        }
                    }
                }
                contadores_unidades[jugador_actual]++;
            }
        }
        //revisando que no hayan equipos vacios ni con mas de 3 unidades
        if (contadores_unidades[1] == 0 || contadores_unidades[1] > 3 || contadores_unidades[0] > 3)
        {
            return false;
        }
        // valido
        return true;
    }
    
    public static GameAttacksController BuildGameController(string archivo, View view)
    {
        int[] contadores_unidades = new int[2];
        contadores_unidades[0] = 0;
        contadores_unidades[1] = 0;
        int jugador_actual = 0;
        Unit[][] unidades = new Unit[2][];
        unidades[0] = new Unit[] { new Unit(), new Unit(), new Unit() };
        unidades[1] = new Unit[] { new Unit(), new Unit(), new Unit() };
        string[] lineas = File.ReadAllLines(archivo);
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
                        unidades[jugador_actual][contadores_unidades[jugador_actual]].Setear_valores(player.Name,
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

        Player jugador1 = new Player(contadores_unidades[0], unidades[0]);
        Player jugador2 = new Player(contadores_unidades[1], unidades[1]);
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
    
    public void SetUnitValues(Unit unit, string nombre, string arma, string genero, int hp_actual,int hp_max, int attk, int spd, int def, int res, View view)
    {
        unit.name = nombre;
        unit.weapon = arma;
        unit.gender = genero;
        unit.hpMax = hp_max;
        unit.currentHp = hp_actual;
        unit.attk = attk;
        unit.spd = spd;
        unit.def = def;
        unit.res = res;
        unit.view = view;
    }
}
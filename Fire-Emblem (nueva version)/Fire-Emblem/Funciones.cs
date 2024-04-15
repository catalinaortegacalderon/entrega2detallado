namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Funciones
{
    public static bool Juego_Valido(string archivo)
    {
        int[] contadores_unidades = new int[2];
        contadores_unidades[0] = 0;
        contadores_unidades[1] = 0;
        int jugador_actual = 0; 
        string[][] unidades = new string[2][]; 
        unidades[0] = new string[] { "", "", "" };
        unidades[1] = new string[] { "", "", "" };
        string[] lineas = File.ReadAllLines(archivo);
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
    
    public static Juego Construir_Juego(string archivo, View view)
    {
        int[] contadores_unidades = new int[2];
        contadores_unidades[0] = 0;
        contadores_unidades[1] = 0;
        int jugador_actual = 0;
        Unidad[][] unidades = new Unidad[2][];
        unidades[0] = new Unidad[] { new Unidad(), new Unidad(), new Unidad() };
        unidades[1] = new Unidad[] { new Unidad(), new Unidad(), new Unidad() };
        string[] lineas = File.ReadAllLines(archivo);
        foreach (string linea in lineas)
        {
            if (linea == "Player 1 Team")
            {
                jugador_actual = 0;
            }
            else if (linea == "Player 2 Team")
            {
                jugador_actual = 1;
            }
            else
            {
                // obtener nombre unidad
                string[] nuevo_string = linea.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string nombre = nuevo_string[0].Replace(" ", "");
                string myJson = File.ReadAllText("characters.json");
                var players = JsonSerializer.Deserialize<List<Unidad_Json>>(myJson);
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
                if (nuevo_string.Length > 1)
                {
                    InstanciarHabilidades(view, nuevo_string, unidades, jugador_actual, contadores_unidades);
                }

                contadores_unidades[jugador_actual]++;
            }
        }

        Jugador jugador1 = new Jugador(contadores_unidades[0], unidades[0]);
        Jugador jugador2 = new Jugador(contadores_unidades[1], unidades[1]);
        Juego nuevo_juego = new Juego(jugador1, jugador2);

        return nuevo_juego;
    }

    private static void InstanciarHabilidades(View view, string[] nuevo_string, Unidad[][] unidades, int jugador_actual,
        int[] contadores_unidades)
    {
        string stringHabilidades = nuevo_string[1];
        string[] listadeHabilidades =
            stringHabilidades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int contador_habilidades = 0;
        foreach (string habilidad in listadeHabilidades)
        {
            //buscar una forma mas eficiente de hacer esto
            if (habilidad == "Speed +5")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new SpeedMas5(view);
            }

            else if (habilidad == "Resolve")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new Resolve(view);
            }
            else if (habilidad == "Armored Blow")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new ArmoredBlow(view);
            }

            else if (habilidad == "Fair Fight")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new FairFight(view);
            }
            else if (habilidad == "Atk/Def +5")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new AtkAndDefMas5(view);
            }
            else if (habilidad == "Atk/Res +5")
            {
                unidades[jugador_actual][contadores_unidades[jugador_actual]]
                    .habilidades[contador_habilidades] = new AtkAndResMas5(view);
            }
            contador_habilidades++;
        }
    }
}
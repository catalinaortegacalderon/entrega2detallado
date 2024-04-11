namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;
public class Game
{
    private View _view;
    private string _teamsFolder;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        //throw new NotImplementedException();
        
        _view.WriteLine("Elige un archivo para cargar los equipos");
        
        // para imprimir los file names se usó la ayuda de la ia "gemini"
        string[] archivos = Directory.GetFiles(_teamsFolder);
        int contador = 0;
        foreach (string archivo in archivos)
        {
            _view.WriteLine( contador + ": " + Path.GetFileName(archivo));
            contador++;
        }
        //string inpu2 = _view.ReadLine();
        int input = Convert.ToInt32(_view.ReadLine());
        if( Juego_Valido(archivos[input]) == false)
        {
            _view.WriteLine( "Archivo de equipos no válido");
            return;
        }
        // construir juego
        Juego juego_actual = Construir_Juego(archivos[input], _view);
        
        while (juego_actual.terminado == false)
        {
            // si pierde el jugador 0
            string nombre_perdedor1 = "";
            // si pierde el jugador 1
            string nombre_perdedor2 = "";
            if (juego_actual.jugador_actual == 0)
            {
                //ESTA ATACANDO EL PRIMER JUGADOR
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[0].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 +": "+ unidad.nombre);
                        contador1++;
                    }
                }
                int valor1 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[1].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " +unidad.nombre);
                        contador2++;
                    }
                }
                int valor2 = Convert.ToInt32(_view.ReadLine());

                _view.WriteLine("Round " + juego_actual.ronda_actual + ": " + juego_actual.jugadores[0].unidades[valor1].nombre
                                +" (Player 1) comienza");
                
                //ataque
                nombre_perdedor2 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.jugador_actual = 1;
                //contraataque
                nombre_perdedor1 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" && juego_actual.jugadores[1].unidades[valor2].spd  >= 5 + juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" && juego_actual.jugadores[1].unidades[valor2].spd + 5 <=  juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" )
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.jugadores[0].unidades[valor1].nombre+
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual+
                                    ") : " + juego_actual.jugadores[1].unidades[valor2].nombre+ 
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual+ 
                                    ")");
                }
                else if (nombre_perdedor1 != "")
                {
                    _view.WriteLine(nombre_perdedor1+
                                    " (0) : " + juego_actual.jugadores[1].unidades[valor2].nombre+ 
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual+ 
                                    ")");
                    
                }
                else
                {
                    _view.WriteLine(juego_actual.jugadores[0].unidades[valor1].nombre+
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual+
                                    ") : " + nombre_perdedor2+ 
                                    " (0)");
                    
                }
                    
                juego_actual.jugador_actual = 1;
            }
            else
            {
                //ESTA ATACANDO EL SEGUNDO JUGADOR
                int contador1 = 0;
                int contador2 = 0;
                _view.WriteLine("Player 2 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[1].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador2 + ": " +unidad.nombre);
                        contador2++;
                    }
                }
                int valor2 = Convert.ToInt32(_view.ReadLine());
                _view.WriteLine("Player 1 selecciona una opción");
                foreach (Unidad unidad in juego_actual.jugadores[0].unidades)
                {
                    if (unidad.nombre != "")
                    {
                        _view.WriteLine(contador1 + ": " + unidad.nombre);
                        contador1++;
                    }
                }
                int valor1 = Convert.ToInt32(_view.ReadLine());
                
                _view.WriteLine("Round " + juego_actual.ronda_actual + ": " + juego_actual.jugadores[1].unidades[valor2].nombre
                                +" (Player 2) comienza");
                //ataque
                nombre_perdedor1 = juego_actual.atacar(1, _view, valor1, valor2);
                juego_actual.jugador_actual = 0;
                //contraataque
                nombre_perdedor2 = juego_actual.atacar(2, _view, valor1, valor2);
                // followup
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "" && juego_actual.jugadores[1].unidades[valor2].spd  >= 5 + juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 1;
                    nombre_perdedor1 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" && juego_actual.jugadores[1].unidades[valor2].spd + 5 <=  juego_actual.jugadores[0].unidades[valor1].spd)
                {
                    juego_actual.jugador_actual = 0;
                    nombre_perdedor2 = juego_actual.atacar(3, _view, valor1, valor2);
                }
                else if (nombre_perdedor1 == "" && nombre_perdedor2 == "" )
                {
                    _view.WriteLine("Ninguna unidad puede hacer un follow up");
                }
                //mostrar hp restante de cada unidad
                if (nombre_perdedor1 == "" && nombre_perdedor2 == "")
                {
                    _view.WriteLine(juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ") : " + juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ")");
                }
                else if (nombre_perdedor1 != "")
                {
                    // aca perdio el jug 1
                    _view.WriteLine(juego_actual.jugadores[1].unidades[valor2].nombre +
                                    " (" + juego_actual.jugadores[1].unidades[valor2].hp_actual +
                                    ") : " + nombre_perdedor1 +
                                    " (0)");
                }
                else
                {
                    // aca perdio el jug 2
                    _view.WriteLine(nombre_perdedor2 +
                                    " (0) : " + juego_actual.jugadores[0].unidades[valor1].nombre +
                                    " (" + juego_actual.jugadores[0].unidades[valor1].hp_actual +
                                    ")");
                }
                juego_actual.jugador_actual = 0;
            }
            juego_actual.ronda_actual++;
            juego_actual.ronda_terminada = false;
        }
        _view.WriteLine("Player " + (juego_actual.ganador +1) + " ganó");
    }
    
    // ojo que hay harto codigo repetido en juego valido y consruir juego tal vez dejar solo una funcion
    
    public Juego Construir_Juego(string archivo, View view)
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
                string nombre = nuevo_string[0];
                string myJson = File.ReadAllText("characters.json");
                var players = JsonSerializer.Deserialize<List<Unidad_Json>>(myJson);
                foreach(var player in players)
                    if (nombre == player.Name)
                    {
                        unidades[jugador_actual][contadores_unidades[jugador_actual]].Setear_valores(player.Name,
                            player.Weapon, player.Gender, Convert.ToInt32(player.HP),
                            Convert.ToInt32(player.HP), Convert.ToInt32(player.Atk), Convert.ToInt32(player.Spd), Convert.ToInt32(player.Def), Convert.ToInt32(player.Res));
                    }
                // agregar habilidades a la unidad
                if (nuevo_string.Length > 1)
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
                        if (habilidad == "Resolve")
                        {
                            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                                .habilidades[contador_habilidades] = new Resolve(view);
                        }
                        Console.WriteLine(habilidad);
                        Console.WriteLine("imprimiendo habilidades");
                        contador_habilidades++;

                    }
                }
                contadores_unidades[jugador_actual]++;
            }
        }
        Jugador jugador1 = new Jugador(contadores_unidades[0], unidades[0]);
        Jugador jugador2 = new Jugador(contadores_unidades[1], unidades[1]);
        Juego nuevo_juego = new Juego(jugador1, jugador2);
        
        return nuevo_juego;
    }
    
    public bool Juego_Valido(string archivo)
    {
        //un equipo es inv´alido cuando tiene unidades repetidas, LISTO
        //una unidad con m´as de dos habilidades, LISTO
        // una unidad con habilidades repetidas,
        // equipos vac´ ıos o LISTO 
        // equipos de m´as de 3 unidades. LISTO
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
}
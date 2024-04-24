namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Text.Json;

public class Functions
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
    
    public static GameController BuildGameController(string archivo, View view)
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
                if (nuevo_string.Length > 1)
                {
                    InstanciarHabilidades(view, nuevo_string, unidades, jugador_actual, contadores_unidades);
                }

                contadores_unidades[jugador_actual]++;
            }
        }

        Player jugador1 = new Player(contadores_unidades[0], unidades[0]);
        Player jugador2 = new Player(contadores_unidades[1], unidades[1]);
        GameController newGameController = new GameController(jugador1, jugador2);

        return newGameController;
    }

    private static void InstanciarHabilidades(View view, string[] nuevo_string, Unit[][] unidades, int jugador_actual,
        int[] contadores_unidades)
    {
        string stringHabilidades = nuevo_string[1];
        string[] listadeHabilidades =
            stringHabilidades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int contador_habilidades = 0;
        foreach (string habilidad in listadeHabilidades)
        {
            //buscar una forma mas eficiente de hacer esto
            HabilityConstructor(view, unidades, jugador_actual, contadores_unidades, habilidad, contador_habilidades);
            contador_habilidades++;
        }
    }

    private static void HabilityConstructor(View view, Unit[][] unidades, int jugador_actual, int[] contadores_unidades,
        string habilidad, int contador_habilidades)
    {
        
        if (habilidad == "HP +15")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new HpMas15(view);
        }
        else if (habilidad == "Speed +5")
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
        else if (habilidad == "Spd/Res +5")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SpdAndResMas5(view);
        }
        else if (habilidad == "Attack +6")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new AttackMas6(view);
        }
        else if (habilidad == "Bracing Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BracingBlow(view);
        }
            
        else if (habilidad == "Will to Win")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new WillToWin(view);
        }
        else if (habilidad == "Tome Precision")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new TomePrecision(view);
        }
        else if (habilidad == "Defense +5")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DefenseMas5(view);
        }
        else if (habilidad == "Resistance +5")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new ResistanceMas5(view);
        }
        else if (habilidad == "Deadly Blade")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DeadlyBlade(view);
        }
        else if (habilidad == "Death Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DeathBlow(view);
        }
        else if (habilidad == "Darting Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DartingBlow(view);
        }
        else if (habilidad == "Warding Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new WardingBlow(view);
        }
        else if (habilidad == "Swift Sparrow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SwiftSparrow(view);
        }
        else if (habilidad == "Sturdy Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SturdyBlow(view);
        }
        else if (habilidad == "Mirror Strike")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new MirrorStrike(view);
        }
        else if (habilidad == "Steady Blow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SteadyBlow(view);
        }
        else if (habilidad == "Swift Strike")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SwiftStrike(view);
        }
        else if (habilidad == "Brazen Atk/Spd")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenAtkSpd(view);
        }
        else if (habilidad == "Brazen Atk/Def")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenAtkDef(view);
        }
        else if (habilidad == "Brazen Atk/Res")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenAtkRes(view);
        }
        else if (habilidad == "Brazen Spd/Def")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenSpdDef(view);
        }
        else if (habilidad == "Brazen Spd/Res")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenSpdRes(view);
        }
        else if (habilidad == "Brazen Def/Res")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BrazenDefRes(view);
        }
        else if (habilidad == "Fire Boost")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new FireBoost(view);
        }
        else if (habilidad == "Wind Boost")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new WindBoost(view);
        }
        else if (habilidad == "Earth Boost")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new EarthBoost(view);
        }
        else if (habilidad == "Water Boost")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new WaterBoost(view);
        }
        else if (habilidad == "Chaos Style")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new ChaosStyle(view);
        }
        else if (habilidad == "Blinding Flash")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BlindingFlash(view);
        }
        else if (habilidad == "Not *Quite*")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new NotQuite(view);
        }
        else if (habilidad == "Stunning Smile")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new StunningSmile(view);
        }
        else if (habilidad == "Disarming Sigh")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DisarmingSigh(view);
        }
        else if (habilidad == "Charmer")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Charmer(view);
        }
        else if (habilidad == "Luna:")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Luna(view);
        }
        else if (habilidad == "Belief in Love")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BeliefInLove(view);
        }
        else if (habilidad == "Beorc's Blessing")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new BeorcsBlessing(view);
        }
        else if (habilidad == "Agnea’s Arrow")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new AgneasArrow(view);
        }
        else if (habilidad == "Sword Agility")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Agility(view, "Sword");
        }
        else if (habilidad == "Lance Power")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Power(view, "Lance");
        }
        else if (habilidad == "Sword Power")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Power(view, "Sword");
        }
        else if (habilidad == "Bow Focus")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Focus(view, "Bow");
        }
        else if (habilidad == "Lance Agility")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Agility(view, "Lance");
        }
        else if (habilidad == "Axe Power")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Power(view, "Axe");
        }
        else if (habilidad == "Bow Agility")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Agility(view, "Bow");
        }
        else if (habilidad == "Sword Focus")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Focus(view, "Sword");
        }
        else if (habilidad == "Close Def")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new CloseDef(view);
        }
        else if (habilidad == "Distant Def")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DistantDef(view);
        }
        else if (habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Lull")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Lull(view,habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Fort")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Fort(view,habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (habilidad == "Life and Death")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new LifeAndDeath(view);
        }
        else if (habilidad == "Solid Ground")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SolidGround(view);
        }
        else if (habilidad == "Still Water")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new StillWater(view);
        }
        else if (habilidad == "Dragonskin")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new DragonSkin(view);
        }
        else if (habilidad == "Light and Dark")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new LightAndDark(view);
        }
        else if (habilidad == "Single-Minded")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new SingleMinded(view);
        }
        else if (habilidad == "Ignis")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Ignis(view);
        }
        else if (habilidad == "Perceptive")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Perceptive(view);
        }
        else if (habilidad == "Wrath")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Wrath(view);
        }
        else if (habilidad == "Soulblade")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Soulblade(view);
        }
        else if (habilidad == "Sandstorm")
        {
            unidades[jugador_actual][contadores_unidades[jugador_actual]]
                .habilidades[contador_habilidades] = new Sandstorm(view);
        }
    }
}
namespace Fire_Emblem;


public class Unidad
{
    public string nombre;
    public string arma;
    public string genero;
    // Stats
    public int hp_max;
    public int hp_actual;
    public int attk;
    public int spd;
    public int def;
    public int res;
    // Habilidaddes de la unidad
    public Habilidades[] habilidades = new Habilidades[2];
    
    public  void Setear_valores(string nombre, string arma, string genero, int hp_actual,int hp_max, int attk, int spd, int def, int res)
    {
        this.nombre = nombre;
        this.arma = arma;
        this.genero = genero;
        // Stats
        this.hp_max = hp_max;
        this.hp_actual = hp_actual;
        this.attk = attk;
        this.spd = spd;
        this.def = def;
        this.res = res;
    }

    public Unidad()
    {
        this.nombre = "";
    }
}
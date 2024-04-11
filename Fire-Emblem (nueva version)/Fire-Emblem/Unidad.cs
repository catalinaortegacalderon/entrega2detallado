using System.Runtime.CompilerServices;
using Fire_Emblem_View;

namespace Fire_Emblem;


public class Unidad
{
    public View view;
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
    // esto es para poder recorrer todas las habiliades sin que se caiga el programa
    // seran reemplazadas por instancias reales

    
    public  void Setear_valores(string nombre, string arma, string genero, int hp_actual,int hp_max, int attk, int spd, int def, int res, View view)
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
        this.view = view;
        this.habilidades[0] = new Habilidades(this.view);
        this.habilidades[0] = new Habilidades(this.view);
    }

    public Unidad()
    {
        this.nombre = "";
    }
}
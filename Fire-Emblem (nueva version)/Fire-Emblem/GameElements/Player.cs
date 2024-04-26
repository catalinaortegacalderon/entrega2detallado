namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Collections.Generic;
public class Player
{
    // eliminar metodo
    public int amountOfUnits;
    //public Unit[] units = new Unit[3];
    public List<Unit> units = new List<Unit>(3);
    public String lastLooserUnit = "";
    public Unit currentUnit;
    public Player(int amountOfUnits, Unit[] units)
    {
        //arreglar a que se pasen lists y no arrays
        this.amountOfUnits = amountOfUnits;
        this.units = units.ToList();
    }
}
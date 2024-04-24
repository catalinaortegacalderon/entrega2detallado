namespace Fire_Emblem;
using Fire_Emblem_View;
public class Player
{
    // eliminar metodo
    public int amountOfUnits;
    public Unit[] units = new Unit[3];
    public String lastLooserUnit = "";
    public Unit currentUnit;
    public Player(int amountOfUnits, Unit[] units)
    {
        this.amountOfUnits = amountOfUnits;
        this.units = units;
    }
}
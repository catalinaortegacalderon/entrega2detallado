namespace Fire_Emblem;
using Fire_Emblem_View;
using System.Collections.Generic;
public class Player
{
    public int amountOfUnits;
    public List<Unit> units = new List<Unit>(3);
    
    public Player(int amountOfUnits, Unit[] units)
    {
        this.amountOfUnits = amountOfUnits;
        this.units = units.ToList();
    }
}
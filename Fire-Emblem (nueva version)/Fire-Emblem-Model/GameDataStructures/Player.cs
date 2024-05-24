namespace Fire_Emblem_Model;
using System.Collections.Generic;
using Fire_Emblem_Model.GameDataStructures.Lists;
public class Player
{
    public int AmountOfUnits;
    public List<Unit> Units = new List<Unit>(3);
    
    // abajo es la lista encapsulada
    public UnitsList UnitsList = new UnitsList();
}
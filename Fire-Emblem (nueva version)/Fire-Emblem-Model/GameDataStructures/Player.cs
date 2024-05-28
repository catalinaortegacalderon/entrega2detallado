namespace Fire_Emblem_Model;
using System.Collections.Generic;
using Fire_Emblem_Model.GameDataStructures.Lists;
public class Player
{
    public int PlayerNumber;
    public int AmountOfUnits;
    public readonly UnitsList Units = new UnitsList();
}
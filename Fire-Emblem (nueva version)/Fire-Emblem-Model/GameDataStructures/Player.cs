namespace Fire_Emblem_Model;
using System.Collections.Generic;
using Fire_Emblem_Model.GameDataStructures.Lists;
public class Player
{
    public int PlayerNumber;
    // tal vez un parametro de is attacking o algo
    public int AmountOfUnits;
    public UnitsList Units = new UnitsList();
}
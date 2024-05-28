using ConsoleApp1.EncapsulatedLists;

namespace ConsoleApp1.GameDataStructures;

public class Player
{
    public int PlayerNumber;
    public int AmountOfUnits;
    public readonly UnitsList Units = new UnitsList();
}
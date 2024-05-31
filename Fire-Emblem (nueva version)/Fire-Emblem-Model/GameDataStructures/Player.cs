using ConsoleApp1.EncapsulatedLists;

namespace ConsoleApp1.GameDataStructures;

public class Player
{
    // todo: ver si sacar playernumber
    public int PlayerNumber;
    public int AmountOfUnits;
    public readonly UnitsList Units = new UnitsList();
}
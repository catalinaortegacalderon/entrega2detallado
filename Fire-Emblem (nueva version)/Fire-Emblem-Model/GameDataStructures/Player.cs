using ConsoleApp1.EncapsulatedLists;

namespace ConsoleApp1.GameDataStructures;

public class Player
{
    public readonly UnitsList Units = new();

    public int AmountOfUnits;

    // todo: ver si sacar playernumber
    public int PlayerNumber;
}
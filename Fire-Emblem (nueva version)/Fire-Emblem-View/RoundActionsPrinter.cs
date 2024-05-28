using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;

public class RoundActionsPrinter
{

    // tal vez no hacer estatico
    public static void Print(PrinterCommands command, View view, Unit unit)
    {
        if (command == PrinterCommands.ShowWinner)
            view.WriteLine("ganadorr");
        else if (command == PrinterCommands.ShowLeftOverHp)
            view.WriteLine("ganadorr");
        else if (command == PrinterCommands.ShowWinner)
            view.WriteLine("ganadorr");
    }

}
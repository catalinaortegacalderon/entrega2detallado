using Fire_Emblem_Model;
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_View;

public class RoundActionsPrinter
{

    // tal vez no hacer estatico
    public static void Print(PrinterCommands command, View view, Unit unit)
    {
        if (command == PrinterCommands.ShowWinner)
            view.WriteLine("ganadorr");
        //else if command == P

    }

}
using System.Data;
using ConsoleApp1.DataTypes;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;

public class View
{
    private readonly AbstractView _view;
    //private RoundActionsPrinter _roundActionsPrinter;
    //private SkillsPrinter SkillsPrinter;

    public static View BuildConsoleView()
        => new View(new ConsoleView());
    
    public static View BuildTestingView(string pathTestScript)
        => new View(new TestingView(pathTestScript));

    public static View BuildManualTestingView(string pathTestScript)
        => new View(new ManualTestingView(pathTestScript));

    private View(AbstractView newView)
    {
        _view = newView;
    }

    public string ReadLine() => _view.ReadLine();

    public void WriteLine(string message)
    {
        _view.WriteLine(message);
    }
    
    public string[] GetScript()
        => _view.GetScript();
    
    public void RoundInfoPrinter(PrinterCommands command, Unit unit)
    {
        // editar esto
        //SkillsPrinter.Print(this,  unit, command);
    }
    
    public void ShowAttack(String attackersName, String defensorsName, int damage)
    {
        _view.WriteLine(attackersName + " ataca a " + defensorsName + " con " + damage + " de daño");
    }
    
    public void ShowAllSkills(Unit unit)
    {
        SkillsPrinter.PrintAll(this,  unit);
    }
    
    
    
    
}
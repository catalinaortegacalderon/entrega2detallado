using System.Runtime.CompilerServices;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;

public class GameView : IView
{
    private View _view;

    public GameView(View view)
    {
        _view = view;
    }
    
    public string ReadLine() => _view.ReadLine();

    public void WriteLine(string message)
    {
        _view.WriteLine(message);
    }
    
    public void ShowAttack(String attackersName, String defensorsName, int damage)
    {
        _view.WriteLine(attackersName + " ataca a " + defensorsName + " con " + damage + " de daño");
    }

    public void AnnounceTeamsAreNotValid()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

    public void ShowAllSkills(Unit unit)
    {
        SkillsPrinter.PrintAll(_view,  unit);
    }

    public void AnnounceWinner(int winnersNumber)
    {
        _view.WriteLine("Player " + (winnersNumber) + " ganó");
    }

    public void ShowHp(Unit roundStarterUnit, Unit opponentsUnit)
    {
        _view.WriteLine(roundStarterUnit.Name +
                        " (" + roundStarterUnit.CurrentHp +
                        ") : " + opponentsUnit.Name +
                        " (" + opponentsUnit.CurrentHp +
                        ")");
    }

    public string AskUserForOption(string prompt, string[] options)
    {
        throw new NotImplementedException();
    }
}
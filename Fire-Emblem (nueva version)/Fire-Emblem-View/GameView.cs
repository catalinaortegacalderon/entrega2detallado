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

    public void AnounceWinner()
    {
        _view.WriteLine("ganador essss");
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
    
    public void ShowAllSkills(Unit unit)
    {
        SkillsPrinter.PrintAll(_view,  unit);
    }

    public void AnnounceWinner(string winnerName)
    {
        throw new NotImplementedException();
    }

    public void ShowHp(string characterName, int hp)
    {
        throw new NotImplementedException();
    }

    public void ShowSkills(string characterName, string[] skills)
    {
        throw new NotImplementedException();
    }

    public string AskUserForOption(string prompt, string[] options)
    {
        throw new NotImplementedException();
    }
}
using System.Runtime.CompilerServices;

namespace Fire_Emblem_View;

public class GameView
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
}
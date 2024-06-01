namespace Fire_Emblem_View;

public class View
{
    private readonly AbstractView _view;

    private View(AbstractView newView)
    {
        _view = newView;
    }

    public static View BuildConsoleView()
    {
        return new View(new ConsoleView());
    }

    public static View BuildTestingView(string pathTestScript)
    {
        return new View(new TestingView(pathTestScript));
    }

    public static View BuildManualTestingView(string pathTestScript)
    {
        return new View(new ManualTestingView(pathTestScript));
    }

    public string ReadLine()
    {
        return _view.ReadLine();
    }

    public void WriteLine(string message)
    {
        _view.WriteLine(message);
    }

    public string[] GetScript()
    {
        return _view.GetScript();
    }
}
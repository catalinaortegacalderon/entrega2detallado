using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;

namespace Fire_Emblem_View;

public class GameView : IView
{
    private readonly View _view;

    public GameView(View view)
    {
        _view = view;
    }

    public int AskPlayerForTheChosenFile(string[] files)
    {
        ShowTeamFilesToUser(files);
        return Convert.ToInt32(_view.ReadLine());
    }

    public void AnnounceTeamsAreNotValid()
    {
        _view.WriteLine("Archivo de equipos no válido");
    }

    public int AskAPlayerForTheChosenUnit(int playerNumber, UnitsList units)
    {
        PrintUnitOptions(playerNumber, units);
        var chosenUnitNumber = Convert.ToInt32(_view.ReadLine());
        return chosenUnitNumber;
    }

    public void ShowRoundInformation(int currentRound, string attackersName, int playersNumber)
    {
        _view.WriteLine("Round " + currentRound + ": "
                        + attackersName + " (Player " + playersNumber + ") comienza");
    }

    public void AnnounceAdvantage(Unit unitWithAdvantage, Unit unitWithoutAdvantage)
    {
        // todo: queda mejor $"{unit} ({weapon}) tiene ...}"
        _view.WriteLine(unitWithAdvantage.Name + " (" + unitWithAdvantage.Weapon +
                        ") tiene ventaja con respecto a " + unitWithoutAdvantage.Name + " ("
                        + unitWithoutAdvantage.Weapon + ")");
    }

    public void AnnounceThereIsNoAdvantage()
    {
        _view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
    }

    public void ShowAllSkills(Unit unit)
    {
        SkillsPrinter.PrintAll(_view, unit);
    }

    public void ShowAttack(string attackersName, string defensorsName, int damage)
    {
        _view.WriteLine(attackersName + " ataca a " + defensorsName + " con " + damage + " de daño");
    }

    public void AnnounceNoUnitCanDoAFollowup()
    {
        _view.WriteLine("Ninguna unidad puede hacer un follow up");
    }

    public void ShowHp(Unit roundStarterUnit, Unit opponentsUnit)
    {
        _view.WriteLine(roundStarterUnit.Name +
                        " (" + roundStarterUnit.CurrentHp +
                        ") : " + opponentsUnit.Name +
                        " (" + opponentsUnit.CurrentHp +
                        ")");
    }

    public void AnnounceWinner(int winnersNumber)
    {
        _view.WriteLine("Player " + winnersNumber + " ganó");
    }

    private void ShowTeamFilesToUser(string[] files)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        var filesCounter = 0;
        foreach (var file in files)
        {
            _view.WriteLine(filesCounter + ": " + Path.GetFileName(file));
            filesCounter++;
        }
    }

    private void PrintUnitOptions(int playerNumber, UnitsList units)
    {
        var unitNumberCounter = 0;
        var playerNumberString = playerNumber == 0 ? "1" : "2";
        _view.WriteLine("Player " + playerNumberString + " selecciona una opción");
        foreach (var unit in units)
        {
            if (unit.Name != "") _view.WriteLine(unitNumberCounter + ": " + unit.Name);
            unitNumberCounter++;
        }
    }
}
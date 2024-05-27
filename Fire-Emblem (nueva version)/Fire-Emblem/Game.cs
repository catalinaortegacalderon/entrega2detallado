namespace Fire_Emblem;
using Fire_Emblem_View;
using Fire_Emblem_Model;
using System.Text.Json;

public class Game
{
    private readonly View _view;
    private readonly string _teamsFolder;
    private GameAttacksController _attackController;
    private string _currentRoundsPlayer1LooserUnitsName;
    private string _currentRoundsPlayer2LooserUnitsName;
    private int _currentUnitNumberOfPlayer1;
    private int _currentUnitNumberOfPlayer2;
    private int _currentRound;
    
    public Game(View view, string teamsFolder)
    {
        _view = view;
        _teamsFolder = teamsFolder;
        _currentRound = 1;
    }

    public void Play()
    {
        try
        {
            TryToPlay();
        }
        catch (InvalidTeamException)
        {
            _view.WriteLine("Archivo de equipos no válido");
        }
    }

    private void TryToPlay()
    {
        string teamFile = GetTeamFile();
        _attackController = Utils.BuildGameController(teamFile, _view);
        while (_attackController.IsGameTerminated() == false)
        {
            PlayOneRound();
        }
        _view.WriteLine("Player " + (_attackController.GetWinner()) + " ganó");
    }
    
    private string GetTeamFile() 
    { 
        string[] files = ReadTeamsFiles();
        ShowTeamFilesToUser(files);
        int fileNumInput = Convert.ToInt32(_view.ReadLine());
        if (!Utils.CheckIfGameIsValid(files[fileNumInput]))
        { 
            throw new InvalidTeamException();
        }
        return files[fileNumInput]; 
    }
    
    private void PlayOneRound()
    {
        SetRoundsParameters();
        if (_attackController.GetCurrentAttacker() == 0) Player1StartsRound();
        else if (_attackController.GetCurrentAttacker() == 1) Player2StartsRound();
        _currentRound++;
    }

    private void SetRoundsParameters()
    {
        _attackController.RestartRound();
        _currentRoundsPlayer1LooserUnitsName = "";
        _currentRoundsPlayer2LooserUnitsName = "";
    }

    private void Player1StartsRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _currentRoundsPlayer2LooserUnitsName = _attackController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _attackController.SetCurrentAttacker(1);
        _currentRoundsPlayer1LooserUnitsName = _attackController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        Console.WriteLine("paso por antes de llamar a eliminate player 1 starst round");
        EliminateLooserUnit();
        //player.Units.EliminateUnit(unitIndex);
        // aca vamos a eliminar las unidades
        //_attackController.ChangeAttacker();
        _attackController.SetCurrentAttacker(1);
    }
    
    private void Player2StartsRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _currentRoundsPlayer1LooserUnitsName = _attackController.Attack(1, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        _attackController.SetCurrentAttacker(0);
        _currentRoundsPlayer2LooserUnitsName = _attackController.Attack(2, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        Followup();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        EliminateLooserUnit();
        // aca vamos a eliminar las unidades
        _attackController.SetCurrentAttacker(0);
    }

    private void AskBothPlayersForTheChosenUnit()
    {
        if (_attackController.GetCurrentAttacker() == 0)
        {
            _currentUnitNumberOfPlayer1 = AskAPlayerForTheChosenUnit(0);
            _currentUnitNumberOfPlayer2 = AskAPlayerForTheChosenUnit(1);
        }
        else
        {
            _currentUnitNumberOfPlayer2 = AskAPlayerForTheChosenUnit(1);
            _currentUnitNumberOfPlayer1 = AskAPlayerForTheChosenUnit(0);
        }
    }

    private void ShowTeamFilesToUser(string[] files)
    {
        _view.WriteLine("Elige un archivo para cargar los equipos");
        int filesCounter = 0;
        foreach (string file in files)
        {
            _view.WriteLine(filesCounter + ": " + Path.GetFileName(file));
            filesCounter++;
        }
    }

    private string[] ReadTeamsFiles()
    {
        string[] files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void PrintRound()
    {
        string playerNumberString  = (_attackController.GetCurrentAttacker() == 0) ? "1" :  "2";
        int numberOfThePlayersUnit  = (_attackController.GetCurrentAttacker() == 0) ? _currentUnitNumberOfPlayer1 :  _currentUnitNumberOfPlayer2;
        // train wrecks: poner variables entregmedio
        _view.WriteLine("Round " + _currentRound + ": " + _attackController.GetPlayers()[_attackController.GetCurrentAttacker()].Units.GetUnitByIndex(numberOfThePlayersUnit).Name + " (Player " + playerNumberString + ") comienza");
    }

    private int AskAPlayerForTheChosenUnit(int playerNumber)
    {
        PrintUnitOptions(playerNumber);
        int chosenUnitNumber = Convert.ToInt32(_view.ReadLine());
        return chosenUnitNumber;
    }
    private void PrintUnitOptions(int playerNumber)
    {
        int unitNumberCounter = 0;
        string playerNumberString  = (playerNumber == 0) ? "1" :  "2";
        _view.WriteLine("Player "+ playerNumberString+ " selecciona una opción");
        foreach (Unit unit in _attackController.GetPlayers()[playerNumber].Units)
        {
            if (unit.Name != "") _view.WriteLine(unitNumberCounter + ": " + unit.Name);
            unitNumberCounter++;
        }
    }
    
    private void Followup()
    {
        if (SecondPlayerCanDoAFollowup())
        {
            _attackController.SetCurrentAttacker(1);
            _currentRoundsPlayer1LooserUnitsName = _attackController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (FirstPlayerCanDoAFollowup())
        {
            _attackController.SetCurrentAttacker(0);
            _currentRoundsPlayer2LooserUnitsName = _attackController.Attack(3, _view, _currentUnitNumberOfPlayer1, _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }

    private bool FirstPlayerCanDoAFollowup()
    {
        return ThereAreNoLoosers() &&
               _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).Spd 
               + _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActiveBonus.Spd * _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActiveBonusNeutralization.Spd
               + _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActivePenalties.Spd * _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActivePenaltiesNeutralization.Spd + 5 <=
               _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).Spd 
               + _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActiveBonus.Spd * _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActiveBonusNeutralization.Spd
               +_attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActivePenalties.Spd * _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActivePenaltiesNeutralization.Spd;
    }

    private bool SecondPlayerCanDoAFollowup()
    {
        return ThereAreNoLoosers() &&
               _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).Spd 
               + _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActiveBonus.Spd * _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActiveBonusNeutralization.Spd
               + _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActivePenalties.Spd * _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).ActivePenaltiesNeutralization.Spd >=
               5 + _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).Spd 
                 + _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActiveBonus.Spd * _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActiveBonusNeutralization.Spd
                 +_attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActivePenalties.Spd * _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).ActivePenaltiesNeutralization.Spd;
    }

    private void ResetUnitsBonus()
    {
        _attackController.ResetAllSkills();
    }
    
    private void UpdateGameLogs()
    {
        UpdateLastOponent();
        UpdateAttacks();
    }

    private void UpdateAttacks()
    {
        _attackController.UpdateAttacks();
    }

    private void EliminateLooserUnit()
    {
        Console.WriteLine("paso por eliminate loosers unit");
        Console.WriteLine(_attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).CurrentHp);
        Console.WriteLine(_attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).CurrentHp);
        if (_attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).CurrentHp == 0)
        {
            Console.WriteLine("elimino");
            _attackController.GetPlayers()[0].Units.EliminateUnit(_currentUnitNumberOfPlayer1);
        }
        if (_attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).CurrentHp == 0)
        {
            Console.WriteLine("elimino");
            _attackController.GetPlayers()[1].Units.EliminateUnit(_currentUnitNumberOfPlayer2);
        }
        return;
    }
    

    private bool ThereAreNoLoosers()
    {
        return _attackController.GetPlayers()[1].Units.GetUnitByIndex(_currentUnitNumberOfPlayer2).CurrentHp != 0 && 
               _attackController.GetPlayers()[0].Units.GetUnitByIndex(_currentUnitNumberOfPlayer1).CurrentHp != 0;
    }

    private void UpdateLastOponent()
    {
        _attackController.UpdateLastOpponents();
    }

    private void ShowLeftoverHp()
    {
        if (IsPlayer1TheRoundStarter())
        {
            _view.WriteLine(GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ") : " + GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ")");
        }
        else
        {
            _view.WriteLine(GetPlayersName(1) +
                            " (" + GetPlayersHp(1) +
                            ") : " + GetPlayersName(0) +
                            " (" + GetPlayersHp(0) +
                            ")");
        }
    }

    private bool IsPlayer1TheRoundStarter()
    {
        return _currentRound % 2 == 1;
    }

    private string GetPlayersName(int player)
    {
        int unitNumber;
        if (player == 0) unitNumber = _currentUnitNumberOfPlayer1;
        else
        {
            unitNumber = _currentUnitNumberOfPlayer2;
        }
        return _attackController.GetPlayers()[player].Units.GetUnitByIndex(unitNumber).Name;
    }
    
    private int GetPlayersHp(int player)
    {
        int unitNumber;
        if (player == 0) unitNumber = _currentUnitNumberOfPlayer1;
        else
        {
            unitNumber = _currentUnitNumberOfPlayer2;
        }
        return _attackController.GetPlayers()[player].Units.GetUnitByIndex(unitNumber).CurrentHp;
    }
    
    
}
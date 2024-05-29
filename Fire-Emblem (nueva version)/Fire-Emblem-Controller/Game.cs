using ConsoleApp1.DataTypes;
using ConsoleApp1.Exceptions;
using ConsoleApp1.GameDataStructures;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private readonly GameView _view;
    private readonly string _teamsFolder;
    private GameAttacksController _attackController;
    private int _currentUnitNumberOfPlayer1;
    private int _currentUnitNumberOfPlayer2;
    private Unit _currentUnitOfPlayer1;
    private Unit _currentUnitOfPlayer2;
    private int _currentRound;
    
    public Game(GameView view, string teamsFolder)
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
            _view.AnnounceTeamsAreNotValid();
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
        _view.AnnounceWinner(_attackController.GetWinner());
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
        _attackController.RestartRound();
        if (IsPlayer1TheRoundStarter())
        {
            _attackController.SetCurrentAttacker(0);
            StartRound();
        }
        else 
        {
            _attackController.SetCurrentAttacker(1);
            StartRound();
        }
        _currentRound++;
    }

    private void StartRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _attackController.Attack(AttackType.FirstAttack , 
            _currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        _attackController.ChangeAttacker();
        _attackController.Attack(AttackType.SecondAttack, 
            _currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        FollowUp();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        EliminateLooserUnit();
        _attackController.SetCurrentAttacker(1);
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
        SetUnits();
    }

    private void SetUnits()
    {
        // todo: ARREGLAR ESTE TRAINWRECJ
        _currentUnitOfPlayer1 = _attackController.GetPlayers()[0].Units
            .GetUnitByIndex(_currentUnitNumberOfPlayer1);
        _currentUnitOfPlayer2 = _attackController.GetPlayers()[1].Units
            .GetUnitByIndex(_currentUnitNumberOfPlayer2);

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

    private string[] ReadTeamsFiles()
    {
        var files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void PrintRound()
    {
        // todo: arreglar este train wreck
        string playerNumberString  = 
            (_attackController.GetCurrentAttacker() == 0) ? "1" :  "2";
        int numberOfThePlayersUnit  = 
            (_attackController.GetCurrentAttacker() == 0) ? _currentUnitNumberOfPlayer1 :  
                _currentUnitNumberOfPlayer2;
        
        // todo: train wrecks: poner variables entregmedio
        _view.WriteLine("Round " + _currentRound + ": " 
                        + _attackController.GetPlayers()[_attackController.GetCurrentAttacker()].
                            Units.GetUnitByIndex(numberOfThePlayersUnit).Name 
                        + " (Player " + playerNumberString + ") comienza");
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
    
    private void FollowUp()
    {
        if (CanDoAFollowup( _currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            _attackController.SetCurrentAttacker(1);
            _attackController.Attack(AttackType.FollowUp, _currentUnitNumberOfPlayer1,
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup( _currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            _attackController.SetCurrentAttacker(0);
            _attackController.Attack(AttackType.FollowUp, _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.WriteLine("Ninguna unidad puede hacer un follow up");
        }
    }

    private bool CanDoAFollowup( Unit attackingUnit, Unit defensiveUnit)
    {
        const int additionValueForFollowupCondition = 5;
        return ThereAreNoLoosers() &&
               defensiveUnit.Spd 
               + defensiveUnit.ActiveBonus.Spd * defensiveUnit.ActiveBonusNeutralization.Spd
               + defensiveUnit.ActivePenalties.Spd * defensiveUnit.ActivePenaltiesNeutralization.Spd 
               + additionValueForFollowupCondition 
               <= attackingUnit.Spd 
               + attackingUnit.ActiveBonus.Spd * attackingUnit.ActiveBonusNeutralization.Spd
               + attackingUnit.ActivePenalties.Spd * attackingUnit.ActivePenaltiesNeutralization.Spd;
    }
    

    private void ResetUnitsBonus()
    {
        _attackController.ResetAllSkills();
    }

    private void EliminateLooserUnit()
    {
        // todo: trainwrecks
        if (_currentUnitOfPlayer1.CurrentHp == 0)
        {
            _attackController.GetPlayers()[0].Units.EliminateUnit(_currentUnitNumberOfPlayer1);
        }
        if (_currentUnitOfPlayer2.CurrentHp == 0)
        {
            _attackController.GetPlayers()[1].Units.EliminateUnit(_currentUnitNumberOfPlayer2);
        }
    }
    

    private bool ThereAreNoLoosers()
    {
        return _currentUnitOfPlayer2.CurrentHp != 0 && 
               _currentUnitOfPlayer1.CurrentHp != 0;
    }

    private void UpdateGameLogs()
    {
        // todo: PONER ESTO MAS BONITO, CAMBIAR NOMBRE
        _attackController.UpdateLastOpponents();
        if (IsPlayer1TheRoundStarter())
        {
            _currentUnitOfPlayer2.HasBeenBeenInACombatStartedByTheOpponent = true;
            _currentUnitOfPlayer1.HasStartedACombat= true;
        }
        else
        {
            _currentUnitOfPlayer2.HasStartedACombat = true;
            _currentUnitOfPlayer1.HasBeenBeenInACombatStartedByTheOpponent = true;
        }
    }

    private void ShowLeftoverHp()
    {
        if (IsPlayer1TheRoundStarter())
            _view.ShowHp( _currentUnitOfPlayer1, _currentUnitOfPlayer2 );
        else
            _view.ShowHp(_currentUnitOfPlayer2, _currentUnitOfPlayer1 );
    }

    private bool IsPlayer1TheRoundStarter()
    {
        return _currentRound % 2 == 1;
    }
}
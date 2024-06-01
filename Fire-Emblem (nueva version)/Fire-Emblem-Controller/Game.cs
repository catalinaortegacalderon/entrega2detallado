using ConsoleApp1;
using ConsoleApp1.DataTypes;
using ConsoleApp1.Exceptions;
using ConsoleApp1.GameDataStructures;
using Fire_Emblem_View;

namespace Fire_Emblem;

public class Game
{
    private const int IdOfPlayer1 = 0;
    private const int IdOfPlayer2 = 1;
    private readonly string _teamsFolder;
    private readonly GameView _view;
    private GameAttacksController _attackController;
    private int _currentRound;
    private int _currentUnitNumberOfPlayer1;
    private int _currentUnitNumberOfPlayer2;
    private Unit _currentUnitOfPlayer1;
    private Unit _currentUnitOfPlayer2;

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
        var teamFile = GetTeamFile();
        _attackController = GameAttacksControllerBuilder.BuildGameController(teamFile, _view);
        while (IsGameNotTerminated()) PlayOneRound();
        _view.AnnounceWinner(_attackController.GetWinner());
    }

    private bool IsGameNotTerminated()
    {
        return _attackController.IsGameTerminated() == false;
    }

    private string GetTeamFile()
    {
        var files = ReadTeamsFiles();
        var fileNumInput = _view.AskPlayerForTheChosenFile(files);
        if (!FileChecker.IsGameValid(files[fileNumInput])) throw new InvalidTeamException();
        return files[fileNumInput];
    }

    private void PlayOneRound()
    {
        _attackController.RestartRound();
        if (IsPlayer1TheRoundStarter())
        {
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            StartRound();
        }
        else
        {
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            StartRound();
        }

        _currentRound++;
    }

    private bool IsPlayer1TheRoundStarter()
    {
        return _currentRound % 2 == 1;
    }

    // todo: pasar a una clase separada
    private void StartRound()
    {
        AskBothPlayersForTheChosenUnit();
        PrintRound();
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FirstAttack,
            _currentUnitNumberOfPlayer1,
            _currentUnitNumberOfPlayer2);
        _attackController.ChangeAttacker();
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.SecondAttack,
            _currentUnitNumberOfPlayer1,
            _currentUnitNumberOfPlayer2);
        FollowUp();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        EliminateLooserUnit();
    }

    // todo: pasar a view
    private void AskBothPlayersForTheChosenUnit()
    {
        var players = _attackController.GetPlayers();
        var player1 = players[IdOfPlayer1];
        var player2 = players[IdOfPlayer2];

        if (_attackController.GetCurrentAttacker() == IdOfPlayer1)
        {
            // todo: pasar a funcion y quitar codigo repetido
            // todo: el if debe tener una linea
            _currentUnitNumberOfPlayer1 = _view.AskAPlayerForTheChosenUnit(IdOfPlayer1,
                player1.Units);
            _currentUnitNumberOfPlayer2 = _view.AskAPlayerForTheChosenUnit(IdOfPlayer2,
                player2.Units);
        }
        else
        {
            _currentUnitNumberOfPlayer2 = _view.AskAPlayerForTheChosenUnit(IdOfPlayer2,
                player2.Units);
            _currentUnitNumberOfPlayer1 = _view.AskAPlayerForTheChosenUnit(IdOfPlayer1,
                player1.Units);
        }

        SetUnits();
    }

    private void SetUnits()
    {
        var players = _attackController.GetPlayers();

        var player1 = players[IdOfPlayer1];
        var player2 = players[IdOfPlayer2];

        var unitsOfPlayer1 = player1.Units;
        var unitsOfPlayer2 = player2.Units;

        _currentUnitOfPlayer1 = unitsOfPlayer1.GetUnitByIndex(_currentUnitNumberOfPlayer1);
        _currentUnitOfPlayer2 = unitsOfPlayer2.GetUnitByIndex(_currentUnitNumberOfPlayer2);
    }

    private string[] ReadTeamsFiles()
    {
        var files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void PrintRound()
    {
        var playersNumber =
            _attackController.GetCurrentAttacker() == IdOfPlayer1 ? 1 : 2;
        _view.ShowRoundInformation(_currentRound, GetCurrentAttackersName(), playersNumber);
    }

    private string GetCurrentAttackersName()
    {
        if (IsPlayer1TheRoundStarter())
            return _currentUnitOfPlayer1.Name;
        return _currentUnitOfPlayer2.Name;
    }

    private void FollowUp()
    {
        if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp,
                _currentUnitNumberOfPlayer1,
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp,
                _currentUnitNumberOfPlayer1,
                _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.AnnounceNoUnitCanDoAFollowup();
        }
    }

    private bool CanDoAFollowup(Unit attackingUnit, Unit defensiveUnit)
    {
        // todo: pasar a funcion
        const int additionValueForFollowupCondition = 5;
        var doesTheFollowupConditionHold =
            defensiveUnit.Spd
            + defensiveUnit.ActiveBonus.Spd * defensiveUnit.ActiveBonusNeutralizer.Spd
            + defensiveUnit.ActivePenalties.Spd * defensiveUnit.ActivePenaltiesNeutralizer.Spd
            + additionValueForFollowupCondition
            <= attackingUnit.Spd
            + attackingUnit.ActiveBonus.Spd * attackingUnit.ActiveBonusNeutralizer.Spd
            + attackingUnit.ActivePenalties.Spd * attackingUnit.ActivePenaltiesNeutralizer.Spd;

        return ThereAreNoLoosers() && doesTheFollowupConditionHold;
    }

    private bool ThereAreNoLoosers()
    {
        return _currentUnitOfPlayer2.CurrentHp != 0 &&
               _currentUnitOfPlayer1.CurrentHp != 0;
    }

    private void ResetUnitsBonus()
    {
        _attackController.ResetAllSkills();
    }

    private void ShowLeftoverHp()
    {
        if (IsPlayer1TheRoundStarter())
            _view.ShowHp(_currentUnitOfPlayer1, _currentUnitOfPlayer2);
        else
            _view.ShowHp(_currentUnitOfPlayer2, _currentUnitOfPlayer1);
    }

    private void UpdateGameLogs()
    {
        _attackController.UpdateLastOpponents();

        if (IsPlayer1TheRoundStarter())
        {
            _currentUnitOfPlayer2.HasBeenBeenInACombatStartedByTheOpponent = true;
            _currentUnitOfPlayer1.HasStartedACombat = true;
        }
        else
        {
            _currentUnitOfPlayer2.HasStartedACombat = true;
            _currentUnitOfPlayer1.HasBeenBeenInACombatStartedByTheOpponent = true;
        }
    }

    private void EliminateLooserUnit()
    {
        var players = _attackController.GetPlayers();

        if (IsUnitDead(_currentUnitOfPlayer1))
        {
            var player1 = players[IdOfPlayer1];
            var unitsOfPlayer1 = player1.Units;
            unitsOfPlayer1.EliminateUnit(_currentUnitNumberOfPlayer1);
        }

        if (IsUnitDead(_currentUnitOfPlayer2))
        {
            var player2 = players[IdOfPlayer2];
            var unitsOfPlayer2 = player2.Units;
            unitsOfPlayer2.EliminateUnit(_currentUnitNumberOfPlayer2);
        }
    }

    private bool IsUnitDead(Unit unit)
    {
        return unit.CurrentHp == 0;
    }
}
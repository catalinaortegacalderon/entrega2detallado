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
        var gameAttacksControllerBuilder = new GameAttacksControllerBuilder();
        _attackController = gameAttacksControllerBuilder.BuildGameController(File.ReadAllLines(teamFile), _view);

        while (IsGameNotTerminated())
        {
            PlayOneRound();
        }

        _view.AnnounceWinner(_attackController.GetWinner());
    }

    private bool IsGameNotTerminated()
    {
        return !_attackController.IsGameTerminated();
    }

    private string GetTeamFile()
    {
        var files = ReadTeamsFiles();
        var fileNumInput = _view.AskPlayerForTheChosenFile(files);

        if (!FileChecker.IsGameValid(files[fileNumInput]))
        {
            throw new InvalidTeamException();
        }

        return files[fileNumInput];
    }

    private string[] ReadTeamsFiles()
    {
        var files = Directory.GetFiles(_teamsFolder);
        Array.Sort(files);
        return files;
    }

    private void PlayOneRound()
    {
        _attackController.RestartRound();

        _attackController.SetCurrentAttacker(IsPlayer1TheRoundStarter() ? IdOfPlayer1 : IdOfPlayer2);
        StartRound();
        _currentRound++;
    }

    private bool IsPlayer1TheRoundStarter()
    {
        return _currentRound % 2 == 1;
    }

    private void StartRound()
    {
        GetAndSetPlayersChosenUnit();
        PrintRound();
        ExecuteAttacks();
        //FollowUp();
        FollowUpProbando();
        // todo: arreglar esto de abajo
        //FollowUpForGuaranteedFollowup();
        ResetUnitsBonus();
        ShowLeftoverHp();
        UpdateGameLogs();
        EliminateLooserUnit();
    }

    private void GetAndSetPlayersChosenUnit()
    {
        GetChosenUnits();
        SetUnits();
    }

    private void GetChosenUnits()
    {
        int[] unitsNumber = _view.AskBothPlayersForTheChosenUnit(_attackController.GetPlayers(),
            _attackController.GetCurrentAttacker());

        _currentUnitNumberOfPlayer1 = unitsNumber[IdOfPlayer1];
        _currentUnitNumberOfPlayer2 = unitsNumber[IdOfPlayer2];
    }


    private void SetUnits()
    {
        var players = _attackController.GetPlayers();

        var player1 = players.GetPlayerById(IdOfPlayer1);
        var unitsOfPlayer1 = player1.Units;
        
        var player2 = players.GetPlayerById(IdOfPlayer2);
        var unitsOfPlayer2 = player2.Units;
        
        _currentUnitOfPlayer1 = unitsOfPlayer1.GetUnitByIndex(_currentUnitNumberOfPlayer1);
        _currentUnitOfPlayer2 = unitsOfPlayer2.GetUnitByIndex(_currentUnitNumberOfPlayer2);
    }

    private void PrintRound()
    {
        int playerNumber = _attackController.GetCurrentAttacker() == IdOfPlayer1 ? 1 : 2;
        _view.ShowRoundInformation(_currentRound, GetCurrentAttackersName(), playerNumber);
    }

    private string GetCurrentAttackersName()
    {
        return IsPlayer1TheRoundStarter() ? _currentUnitOfPlayer1.Name : _currentUnitOfPlayer2.Name;
    }

    private void ExecuteAttacks()
    {
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FirstAttack, 
            _currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        _attackController.ChangeAttacker();
        _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.SecondAttack, 
            _currentUnitNumberOfPlayer1,
            _currentUnitNumberOfPlayer2);
    }
    
    private void FollowUp()
    {
        //todo: AL PARECER, HASTA AHORA, SOLO UNO PODIA HACER FOLLOWUP, CAMBIE EL ORDEN DEL IF Y ELIF Y NO PASO NADA
        
        // SI AMBOS PUEDEN HACER FOLLOWUP, PARTE EL QUE ESTA ATACANDO (EL QUE INICIA COMBATE)
        
        if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.AnnounceNoUnitCanDoAFollowup();
        }
    }
    
    // todo: hacer clase extra que maneje followup
    
    private void FollowUpProbando()
    {
        Unit unitThatStartedTheRound = _currentUnitOfPlayer1;
        Unit unitThatDidNotStartTheRound = _currentUnitOfPlayer2;
        
        if (IsPlayer1TheRoundStarter())
        {
            unitThatStartedTheRound = _currentUnitOfPlayer1;
            unitThatDidNotStartTheRound = _currentUnitOfPlayer2;
        }
        //todo: AL PARECER, HASTA AHORA, SOLO UNO PODIA HACER FOLLOWUP, CAMBIE EL ORDEN DEL IF Y ELIF Y NO PASO NADA
        
        // SI AMBOS PUEDEN HACER FOLLOWUP, PARTE EL QUE ESTA ATACANDO (EL QUE INICIA COMBATE)
        
        if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (ThereAreNoLoosers())
        {
            _view.AnnounceNoUnitCanDoAFollowup();
        }
    }

    private void FollowUp2()
    {
        // todo: arreglar esto, ver orden de followups
        
        // flujo: antes, solo un followup. este followup lo hacía

        if (!IsPlayer1TheRoundStarter())
        { 
            Console.WriteLine("paso por 1");
            
        if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1))
        {
            Console.WriteLine("paso por a");
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2))
        {
            Console.WriteLine("paso por b");
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (_currentUnitOfPlayer1.CombatEffects.HasGuaranteedFollowUp)
        {
            Console.WriteLine("paso por b");
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
        else
        {
            Console.WriteLine("paso por 2");
            if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2))
            {
                Console.WriteLine("paso por 2a");
                _attackController.SetCurrentAttacker(IdOfPlayer1);
                _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                    _currentUnitNumberOfPlayer1, 
                    _currentUnitNumberOfPlayer2);
            }
            else if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1))
            {
                Console.WriteLine("paso por 2b");
                _attackController.SetCurrentAttacker(IdOfPlayer2);
                _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                    _currentUnitNumberOfPlayer1, 
                    _currentUnitNumberOfPlayer2);
            }
            else if (_currentUnitOfPlayer2.CombatEffects.HasGuaranteedFollowUp)
            {
                Console.WriteLine("paso por 2b");
                _attackController.SetCurrentAttacker(IdOfPlayer2);
                _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                    _currentUnitNumberOfPlayer1, 
                    _currentUnitNumberOfPlayer2);
            }
            else if (ThereAreNoLoosers())
            {
                _view.AnnounceNoUnitCanDoAFollowup();
            }
            
        }
        
        
    }

    private void FollowUpForGuaranteedFollowup()
    {
        // todo: arreglar esto, ver orden de followups

        if (!IsPlayer1TheRoundStarter())
        {
            if (_currentUnitOfPlayer1.CombatEffects.HasGuaranteedFollowUp)
            {
                Console.WriteLine("paso por b");
                _attackController.SetCurrentAttacker(IdOfPlayer1);
                _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp,
                    _currentUnitNumberOfPlayer1,
                    _currentUnitNumberOfPlayer2);
            }
        }
        else
        {
            if (_currentUnitOfPlayer2.CombatEffects.HasGuaranteedFollowUp)
            {
                Console.WriteLine("paso por 2b");
                _attackController.SetCurrentAttacker(IdOfPlayer2);
                _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp,
                    _currentUnitNumberOfPlayer1,
                    _currentUnitNumberOfPlayer2);
            }
        }
    }

    private bool CanDoAFollowup(Unit attackingUnit, Unit defensiveUnit)
    {
        const int additionValueForFollowupCondition = 5;
        bool doesFollowupConditionHold =
            defensiveUnit.Spd
            + defensiveUnit.ActiveBonus.Spd * defensiveUnit.ActiveBonusNeutralizer.Spd
            + defensiveUnit.ActivePenalties.Spd * defensiveUnit.ActivePenaltiesNeutralizer.Spd
            + additionValueForFollowupCondition
            <= attackingUnit.Spd
            + attackingUnit.ActiveBonus.Spd * attackingUnit.ActiveBonusNeutralizer.Spd
            + attackingUnit.ActivePenalties.Spd * attackingUnit.ActivePenaltiesNeutralizer.Spd;
        
        // todo: poner el statement de abajo mejor encapsulado
        
        Console.WriteLine("imprimiendo lo que quieroo");
        Console.WriteLine(attackingUnit.Name);
        Console.WriteLine(attackingUnit.CombatEffects.HasGuaranteedFollowUp);
        Console.WriteLine(ThereAreNoLoosers());
        Console.WriteLine(doesFollowupConditionHold);
        Console.WriteLine(ThereAreNoLoosers() && (doesFollowupConditionHold || attackingUnit.CombatEffects.HasGuaranteedFollowUp));
        

        return ThereAreNoLoosers() && (doesFollowupConditionHold || attackingUnit.CombatEffects.HasGuaranteedFollowUp);
    }

    private bool ThereAreNoLoosers()
    {
        return _currentUnitOfPlayer2.CurrentHp != 0 && _currentUnitOfPlayer1.CurrentHp != 0;
    }

    private void ResetUnitsBonus()
    {
        _attackController.ResetAllSkills();
    }

    private void ShowLeftoverHp()
    {
        if (IsPlayer1TheRoundStarter())
        {
            _view.ShowHp(_currentUnitOfPlayer1, _currentUnitOfPlayer2);
        }
        else
        {
            _view.ShowHp(_currentUnitOfPlayer2, _currentUnitOfPlayer1);
        }
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
            var player1 = players.GetPlayerById(IdOfPlayer1);
            var unitsOfPlayer1 = player1.Units;
                
            unitsOfPlayer1.EliminateUnit(_currentUnitNumberOfPlayer1);
        }

        if (IsUnitDead(_currentUnitOfPlayer2))
        {
            var player2 = players.GetPlayerById(IdOfPlayer2);
            var unitsOfPlayer2 = player2.Units;
                
            unitsOfPlayer2.EliminateUnit(_currentUnitNumberOfPlayer2);
        }
    }

    private bool IsUnitDead(Unit unit)
    {
        return unit.CurrentHp == 0;
    }
}
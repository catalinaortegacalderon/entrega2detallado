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
        // todo: ACA SE MANEJA TODO
        GetAndSetPlayersChosenUnit();
        PrintRound();
        //ManageCurationAtTheBeginningOfTheCombat(); tal vez esta y la de abajo pueden ir juntas en una funcion
        //ManageDamageAtTheBeginningOfTheCombat(); tal vez parecida al end of combat
        ExecuteAttacks();
        FollowUp();
        ManageCurationAtTheEndOfTheCombat();
        ManageDamageAtTheEndOfTheCombat();
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
        if (IsTheDefensorAbleToCounterAttack())
        {
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.SecondAttack, 
                _currentUnitNumberOfPlayer1,
                _currentUnitNumberOfPlayer2);
        }
    }

    private bool IsTheDefensorAbleToCounterAttack()
    {
        if (IsPlayer1TheRoundStarter())
        {
            return !_currentUnitOfPlayer2.CombatEffects.HasCounterAttackDenial || 
                   _currentUnitOfPlayer2.CombatEffects.HasNeutralizationOfCounterattackDenial;
        }
        return !_currentUnitOfPlayer1.CombatEffects.HasCounterAttackDenial || 
               _currentUnitOfPlayer1.CombatEffects.HasNeutralizationOfCounterattackDenial;
    }
    
    private void FollowUp()
    { 
        if (CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2) &&
            CanASpecificPlayerCounterAttack(IdOfPlayer1))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer1);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if (CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1) &&
                 CanASpecificPlayerCounterAttack(IdOfPlayer2))
        {
            _attackController.SetCurrentAttacker(IdOfPlayer2);
            _attackController.GenerateAnAttackBetweenTwoUnits(AttackType.FollowUp, 
                _currentUnitNumberOfPlayer1, 
                _currentUnitNumberOfPlayer2);
        }
        else if ( AttackerCantDoFollowup() && !IsTheDefensorAbleToCounterAttack())
        {
            Console.WriteLine("IMPRIMIENDO");
            Console.WriteLine(IsTheDefensorAbleToCounterAttack());
            if (IsPlayer1TheRoundStarter())
            {
                _view.AnnounceASpecificUnitCantDoAFollowup(_currentUnitOfPlayer1.Name);
            }
            else
            {
                _view.AnnounceASpecificUnitCantDoAFollowup(_currentUnitOfPlayer2.Name);
            }
        }
        else if (ThereAreNoLoosers())
        {
            _view.AnnounceNoUnitCanDoAFollowup();
        }
    }

    private bool AttackerCantDoFollowup()
    {
        if (IsPlayer1TheRoundStarter())
        {
            return !CanDoAFollowup(_currentUnitOfPlayer1, _currentUnitOfPlayer2);
        }
        return !CanDoAFollowup(_currentUnitOfPlayer2, _currentUnitOfPlayer1);
    }

    private bool CanASpecificPlayerCounterAttack(int playerId)
    {
        // todo: este metodo y can defensor counteratack son muy similares
        if (playerId == IdOfPlayer1)
        {
            return !_currentUnitOfPlayer1.CombatEffects.HasCounterAttackDenial || 
                   _currentUnitOfPlayer1.CombatEffects.HasNeutralizationOfCounterattackDenial;;
        }
        return !_currentUnitOfPlayer2.CombatEffects.HasCounterAttackDenial || 
               _currentUnitOfPlayer2.CombatEffects.HasNeutralizationOfCounterattackDenial;;
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
        return ThereAreNoLoosers() && (doesFollowupConditionHold || attackingUnit.CombatEffects.HasGuaranteedFollowUp);
    }

    private bool ThereAreNoLoosers()
    {
        return _currentUnitOfPlayer2.CurrentHp != 0 && _currentUnitOfPlayer1.CurrentHp != 0;
    }
    
    private void ManageCurationAtTheEndOfTheCombat()
    {
        // todo: revisar orden de esto, separar en funciones
        // nota: sobrevive con al menos 1 hp    OJO SE IMPRIME EL ORIGINAL NO EL EFECTIVO
        
        // NO PUEDE REVIVIR NI DEJARLA CON MAS DE HP MAXIMO
        
        if (_currentUnitOfPlayer1.CombatEffects.HpRecuperationAtTheEndOfTheCombat > 0 
            && _currentUnitOfPlayer1.CurrentHp > 0)
        { 
            if (_currentUnitOfPlayer1.CurrentHp + _currentUnitOfPlayer1.CombatEffects.HpRecuperationAtTheEndOfTheCombat
                > _currentUnitOfPlayer1.HpMax)
                _currentUnitOfPlayer1.CurrentHp = _currentUnitOfPlayer1.HpMax;
            else
            {
                _currentUnitOfPlayer1.CurrentHp += _currentUnitOfPlayer1.CombatEffects.HpRecuperationAtTheEndOfTheCombat;
            }
            _view.AnnounceCurationAfterCombat(_currentUnitOfPlayer1, 
                _currentUnitOfPlayer1.CombatEffects.HpRecuperationAtTheEndOfTheCombat);
        }
        if (_currentUnitOfPlayer2.CombatEffects.HpRecuperationAtTheEndOfTheCombat > 0 
            && _currentUnitOfPlayer2.CurrentHp > 0)
        {
            if (_currentUnitOfPlayer2.CurrentHp + _currentUnitOfPlayer2.CombatEffects.HpRecuperationAtTheEndOfTheCombat
                > _currentUnitOfPlayer2.HpMax)
                _currentUnitOfPlayer2.CurrentHp = _currentUnitOfPlayer2.HpMax;
            else
            {
                _currentUnitOfPlayer2.CurrentHp +=
                    _currentUnitOfPlayer2.CombatEffects.HpRecuperationAtTheEndOfTheCombat;
            }
            _view.AnnounceCurationAfterCombat(_currentUnitOfPlayer2, 
                _currentUnitOfPlayer2.CombatEffects.HpRecuperationAtTheEndOfTheCombat);
        }
            
    }


    private void ManageDamageAtTheEndOfTheCombat()
    {
        // todo: revisar orden de esto, separar en funciones
        // nota: sobrevive con al menos 1 hp    OJO SE IMPRIME EL ORIGINAL NO EL EFECTIVO
        // si la unidad muere no se anuncia
        
        if (_currentUnitOfPlayer1.HasAttackedThisRound)
            _currentUnitOfPlayer1.CombatEffects.DamageAfterCombat
                += _currentUnitOfPlayer1.CombatEffects.DamageAfterCombatIfUnitAttacks;
        if (_currentUnitOfPlayer2.HasAttackedThisRound)
            _currentUnitOfPlayer2.CombatEffects.DamageAfterCombat
                += _currentUnitOfPlayer2.CombatEffects.DamageAfterCombatIfUnitAttacks;
        
        if (_currentUnitOfPlayer1.CombatEffects.DamageAfterCombat > 0 && _currentUnitOfPlayer1.CurrentHp > 0)
        {
            if (_currentUnitOfPlayer1.CurrentHp <= _currentUnitOfPlayer1.CombatEffects.DamageAfterCombat)
            {
                _currentUnitOfPlayer1.CurrentHp = 1;
            }
            else
            {
                _currentUnitOfPlayer1.CurrentHp -= _currentUnitOfPlayer1.CombatEffects.DamageAfterCombat;
            }
            _view.AnnounceDamageAfterCombat(_currentUnitOfPlayer1, 
                _currentUnitOfPlayer1.CombatEffects.DamageAfterCombat);
        }
        if (_currentUnitOfPlayer2.CombatEffects.DamageAfterCombat > 0 && _currentUnitOfPlayer2.CurrentHp > 0)
        {
            if (_currentUnitOfPlayer2.CurrentHp <= _currentUnitOfPlayer2.CombatEffects.DamageAfterCombat)
            {
                _currentUnitOfPlayer2.CurrentHp = 1;
            }
            else
            {
                _currentUnitOfPlayer2.CurrentHp -= _currentUnitOfPlayer2.CombatEffects.DamageAfterCombat;
            }
            _view.AnnounceDamageAfterCombat(_currentUnitOfPlayer2, 
                _currentUnitOfPlayer2.CombatEffects.DamageAfterCombat);
        }
            
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
            // todo: separar en tres funciones
            _attackController.UpdateLastOpponents();

            _currentUnitOfPlayer1.HasAttackedThisRound = false;
            _currentUnitOfPlayer2.HasAttackedThisRound = false;

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
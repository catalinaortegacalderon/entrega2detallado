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
    
    // todo: hacer un manager
    // se encarga de cosas de game y de game attacks controler, todos los manage

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
        CheckAlliesConditionsForSkills();
        //ManageCurationAtTheBeginningOfTheCombat(); tal vez esta y la de abajo pueden ir juntas en una funcion, tal vez parecida al end of combat
        InitializeRound();
        ManageDamageAtTheBeginningOfTheCombat(); 
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

    private void CheckAlliesConditionsForSkills()
    {
        CheckPlayerAlliesConditions(IdOfPlayer1);
        CheckPlayerAlliesConditions(IdOfPlayer1);
    }

    private void CheckPlayerAlliesConditions(int playerId)
    {
        var players = _attackController.GetPlayers();
        var player = players.GetPlayerById(playerId);
        var unitsOfThePlayer = player.Units;
        bool hasAllyWithMagic = false;
        foreach (var unit in unitsOfThePlayer)
        {
            if (unit.CurrentHp > 0 && unit.Weapon == Weapon.Magic)
                hasAllyWithMagic = true;
        }
        foreach (var unit in unitsOfThePlayer)
        {
            unit.HasAnAllyWithMagic = hasAllyWithMagic;
        }
    }

    private void InitializeRound()
    {
        _attackController.InitializeRound(_currentUnitNumberOfPlayer1, 
            _currentUnitNumberOfPlayer2);
        // TODO: MEJOR NOMBRE, nose si llamar a controller para que lo haga o hacerlo afuera
        //el cacho era currentatackingunit y currentdefensiveunit que son cosas de controller
        // SE IMRPIMEN LOS STARTING PARAMS, SE ACTIVAN LAS SKILLS, IMPRIME VENTAJAS Y SKILLS
        // ACA LLAMAR A GAME ATTACKS CONTROLLER
        //_currentAttackingUnit.StartedTheRound = true;
        //_currentDefensiveUnit.StartedTheRound = false;
        //ActivateSkills();
        //PrintStartingParameters();
    }
   

    private void ManageDamageAtTheBeginningOfTheCombat()
    { 
        // todo: hago esto mucho, pensar manera mas eficiente
        if (IsPlayer1TheRoundStarter())
        {
            ApplyDamageAtTheBeginningOfTheCombat(_currentUnitOfPlayer1);
            ApplyDamageAtTheBeginningOfTheCombat(_currentUnitOfPlayer2);
        }
        else
        {
            ApplyDamageAtTheBeginningOfTheCombat(_currentUnitOfPlayer2);
            ApplyDamageAtTheBeginningOfTheCombat(_currentUnitOfPlayer1);
        }
        
    }

    private void ApplyDamageAtTheBeginningOfTheCombat(Unit unit)
    {
        if (unit.CombatEffects.DamageBeforeCombat > 0 )
        {
            if (unit.CurrentHp <= unit.CombatEffects.DamageBeforeCombat)
            {
                _currentUnitOfPlayer1.CurrentHp = 1;
            }
            else
            {
                unit.CurrentHp -= unit.CombatEffects.DamageBeforeCombat;
            }
            _view.AnnounceDamageBeforeCombat(unit, 
                unit.CombatEffects.DamageBeforeCombat);
        }
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
        if (IsPlayer1TheRoundStarter())
        {
            ApplyCurationAtTheEndOfTheCombat(_currentUnitOfPlayer1);
            ApplyCurationAtTheEndOfTheCombat(_currentUnitOfPlayer2);
        }
        else
        {
            ApplyCurationAtTheEndOfTheCombat(_currentUnitOfPlayer2);
            ApplyCurationAtTheEndOfTheCombat(_currentUnitOfPlayer1);
        }
        
    }

    private void ApplyCurationAtTheEndOfTheCombat(Unit unit)
    {
        if (unit.CombatEffects.HpRecuperationAtTheEndOfTheCombat > 0 
            && unit.CurrentHp > 0)
        { 
            if (unit.CurrentHp + unit.CombatEffects.HpRecuperationAtTheEndOfTheCombat
                > unit.HpMax)
                unit.CurrentHp = unit.HpMax;
            else
            {
                unit.CurrentHp += unit.CombatEffects.HpRecuperationAtTheEndOfTheCombat;
            }
            _view.AnnounceCurationAfterCombat(unit, 
                unit.CombatEffects.HpRecuperationAtTheEndOfTheCombat);
        }
    }


    private void ManageDamageAtTheEndOfTheCombat()
    {
        if (IsPlayer1TheRoundStarter())
        {
            ApplyDamageAfterCombat(_currentUnitOfPlayer1);
            ApplyDamageAfterCombat(_currentUnitOfPlayer2);
        }
        else
        {
            ApplyDamageAfterCombat(_currentUnitOfPlayer2);
            ApplyDamageAfterCombat(_currentUnitOfPlayer1);
        }
        
 
    }

    private void ApplyDamageAfterCombat(Unit unit)
    {
        if (unit.HasAttackedThisRound)
            unit.CombatEffects.DamageAfterCombat
                += unit.CombatEffects.DamageAfterCombatIfUnitAttacks;
        
        if (unit.CombatEffects.DamageAfterCombat > 0 && unit.CurrentHp > 0)
        {
            if (unit.CurrentHp <= unit.CombatEffects.DamageAfterCombat)
            {
                unit.CurrentHp = 1;
            }
            else
            {
                unit.CurrentHp -= unit.CombatEffects.DamageAfterCombat;
            }
            _view.AnnounceDamageAfterCombat(unit, 
                unit.CombatEffects.DamageAfterCombat);
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
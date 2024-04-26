namespace Fire_Emblem;
using Fire_Emblem_View;

public class GameAttacksController
{
    public Player[] players = new Player[2];
    public int currentAttacker;
    public bool gameIsTerminated;
    public int currentRound = 1;
    public int winner = -1;
    public bool roundIsTerminated = false;
    private Unit _currentAttackingUnit;
    private Unit _currentDefensiveUnit;
    private int _numberOfThisRoundsCurrentAttack;
    private int _firstPlayersCurrentUnitNumber;
    private int _secondPlayersCurrentUnitNumber;
    public string NameOfPlayer1sLoosingUnit = "";
    public string NameOfPlayer2sLoosingUnit = "";
    private int _attackValue;
    
    
    // arreglos: hacer variables privadas (setter y getter)
    //  ver si dejo lineas largas o con enter
    //me faltan las anulaciones de habilidades
    //eliminar que se retorne el nombre del jugador
    // PONER UNIDADES COMO LIST Y NO ARRAY PARA HACER POP
    // ver si dejo view como atributo o solo como param de funcion
    // IDEA: SOLO ATACKING PLAYER Y DEFENSE PLAYER, SACAR CURRENT PLAYER

    public GameAttacksController(Player firstPlayer, Player secondPlayer)
    {
        this.currentAttacker = 0;
        this.players[0] = firstPlayer;
        this.players[1] = secondPlayer;
        this.gameIsTerminated = false;
    }

    public string Attack(int numberOfCurrentAttack, View view, int firstPlayersCurrentUnitNumber, int secondPlayersCurrentUnitNumber)
    {
        if (this.gameIsTerminated || this.roundIsTerminated) return "";
        SetAttacksParameters(numberOfCurrentAttack, firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        if (_numberOfThisRoundsCurrentAttack == 1) PrintStartingParameters(view);
        _attackValue = CalculateAttack();
        PrintWhoAttacksWho(view);
        if (currentAttacker == 0) return Player1Attacks(_attackValue);
        else { return Player2Attacks(); }
    }

    private void PrintStartingParameters(View view)
    {
        PrintAdvantages(view);
        ActivateSkills();
        PrintSkillsInfo(view);
    }

    private void PrintWhoAttacksWho(View view)
    {
        view.WriteLine(_currentAttackingUnit.Name + " ataca a " + _currentDefensiveUnit.Name + " con " + _attackValue + " de daño");
    }

    private void SetAttacksParameters(int numberOfCurrentAttack, int firstPlayersCurrentUnitNumber,
        int secondPlayersCurrentUnitNumber)
    {
        _numberOfThisRoundsCurrentAttack = numberOfCurrentAttack;
        this._firstPlayersCurrentUnitNumber = firstPlayersCurrentUnitNumber;
        this._secondPlayersCurrentUnitNumber = secondPlayersCurrentUnitNumber;
        SetAttackingAndDefensiveUnits();
    }

    private string Player2Attacks()
    {
        string loosersName;
        if (_attackValue >= players[0].units[_firstPlayersCurrentUnitNumber].CurrentHp)
        {
            loosersName = players[0].units[_firstPlayersCurrentUnitNumber].Name;
            this.roundIsTerminated = true;
            EliminateLooserUnitOfPlayer1();
            return loosersName;
        }
        else
        {
            players[0].units[_firstPlayersCurrentUnitNumber].CurrentHp = players[0].units[_firstPlayersCurrentUnitNumber].CurrentHp - _attackValue;
        }
        return "";
    }

    private string Player1Attacks(int attackValue)
    {
        string loosersName;
        if (attackValue >= players[1].units[_secondPlayersCurrentUnitNumber].CurrentHp)
        {
            loosersName = players[1].units[_secondPlayersCurrentUnitNumber].Name;
            this.roundIsTerminated = true;
            EliminateLooserUnitOfPlayer2(_secondPlayersCurrentUnitNumber);
            return loosersName;
        }
        else
        {
            SetDefendorsNewHp(_secondPlayersCurrentUnitNumber, attackValue);
            return "";
        }
    }

    private void ActivateSkills()
    {
        ActivateAttackersUnitSkills();
        ActivateDefensorsUnitSkills();
    }

    private void EliminateLooserUnitOfPlayer1()
    {
        players[0].units.RemoveAt(_firstPlayersCurrentUnitNumber);
        players[0].amountOfUnits = players[0].amountOfUnits - 1;
        if (players[0].amountOfUnits == 0)
        {
            this.gameIsTerminated = true;
            this.winner = 1;
        }
    }

    private void SetAttackingAndDefensiveUnits()
    {
        if (currentAttacker == 0)
        {
            _currentAttackingUnit = players[0].units[_firstPlayersCurrentUnitNumber];
            _currentDefensiveUnit = players[1].units[_secondPlayersCurrentUnitNumber];
        }
        else
        {
            _currentAttackingUnit = players[1].units[_secondPlayersCurrentUnitNumber];
            _currentDefensiveUnit = players[0].units[_firstPlayersCurrentUnitNumber];

        }
    }

    private void SetDefendorsNewHp(int secondPlayersCurrentUnitNumber, int attackValue)
    {
        _currentDefensiveUnit.CurrentHp -= attackValue;
    }

    private void EliminateLooserUnitOfPlayer2(int secondPlayersCurrentUnitNumber)
    {
        players[1].units.RemoveAt(_secondPlayersCurrentUnitNumber);
        players[1].amountOfUnits = players[1].amountOfUnits - 1;
        if (players[1].amountOfUnits == 0)
        {
            this.gameIsTerminated = true;
            this.winner = 0;
        }
    }

    private void ActivateAttackersUnitSkills()
    {
        foreach (Skill habilidad in _currentAttackingUnit.Skills)
        {
            habilidad.ApplySkills(_currentAttackingUnit, _currentDefensiveUnit, true);
        }
    }
    private void ActivateDefensorsUnitSkills()
    {
        foreach (Skill habilidad in _currentDefensiveUnit.Skills)
        {
            habilidad.ApplySkills(_currentDefensiveUnit, _currentAttackingUnit, false);
        }
    }

    private void PrintSkillsInfo(View view)
    {
        SkillsPrinter.PrintBonus(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenalties(view, _currentAttackingUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentAttackingUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentAttackingUnit);

        SkillsPrinter.PrintBonus(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenalties(view, _currentDefensiveUnit);
        SkillsPrinter.PrintBonusNetralization(view, _currentDefensiveUnit);
        SkillsPrinter.PrintPenaltyNetralization(view, _currentDefensiveUnit);
    }

    public int CalculateAttack()
    {
        string attackingWeapon = _currentAttackingUnit.Weapon;
        string defensiveWeapon = _currentDefensiveUnit.Weapon;
        int rivalsDefOrRes = CalculateRivalsDefOrRes(attackingWeapon);
        double wtb = CalculateWtb(defensiveWeapon, attackingWeapon);
        int unitsAtk = CalculateUnitsAtk();
        _currentAttackingUnit.GameLogs.AmountOfAttacks++;
        if ((unitsAtk * wtb - rivalsDefOrRes) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(unitsAtk * wtb - rivalsDefOrRes));
    }

    private int CalculateUnitsAtk()
    {
        int unitsAtk = _currentAttackingUnit.Attk + _currentAttackingUnit.ActiveBonus.attk * _currentAttackingUnit.ActiveBonusNeutralization.attk + _currentAttackingUnit.ActivePenalties.attk * _currentAttackingUnit.ActivePenaltiesNeutralization.attk;
        if (_numberOfThisRoundsCurrentAttack == 1 || _numberOfThisRoundsCurrentAttack == 2)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.atkFirstAttack * _currentAttackingUnit.ActiveBonusNeutralization.attk +
                          _currentAttackingUnit.ActivePenalties.atkFirstAttack * _currentAttackingUnit.ActivePenaltiesNeutralization.attk;
        }
        if (_numberOfThisRoundsCurrentAttack == 3)
        {
            unitsAtk += _currentAttackingUnit.ActiveBonus.atkFollowup * _currentAttackingUnit.ActiveBonusNeutralization.attk
                          + _currentAttackingUnit.ActivePenalties.atkFollowup * _currentAttackingUnit.ActivePenaltiesNeutralization.attk;
        }
        return unitsAtk;
    }

    private static double CalculateWtb(string defensiveWeapon, string attackingWeapon)
    {
        double wtb;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) wtb = 1;
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) wtb = 1.2;
        else
        {
            wtb = 0.8;
        }
        return wtb;
    }

    private int CalculateRivalsDefOrRes(string attackingWeapon)
    {
        int rivalsDefOrRes;
        if (attackingWeapon == "Magic")
        {
            rivalsDefOrRes = _currentDefensiveUnit.Res + _currentDefensiveUnit.ActiveBonus.res * _currentDefensiveUnit.ActiveBonusNeutralization.res + _currentDefensiveUnit.ActivePenalties.res *_currentDefensiveUnit.ActivePenaltiesNeutralization.res;
            if (_numberOfThisRoundsCurrentAttack == 2) rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.resFirstAttack * _currentDefensiveUnit.ActiveBonusNeutralization.res + _currentDefensiveUnit.ActivePenalties.resFirstAttack *_currentDefensiveUnit.ActivePenaltiesNeutralization.res;
        }
        else
        {
            rivalsDefOrRes = _currentDefensiveUnit.Def + _currentDefensiveUnit.ActiveBonus.def * _currentDefensiveUnit.ActiveBonusNeutralization.def + _currentDefensiveUnit.ActivePenalties.def *_currentDefensiveUnit.ActivePenaltiesNeutralization.def;
            if (_numberOfThisRoundsCurrentAttack == 2) rivalsDefOrRes += _currentDefensiveUnit.ActiveBonus.defFirstAttack * _currentDefensiveUnit.ActiveBonusNeutralization.def + _currentDefensiveUnit.ActivePenalties.defFirstAttack *_currentDefensiveUnit.ActivePenaltiesNeutralization.def;
        }

        return rivalsDefOrRes;
    }

    public void PrintAdvantages(View view)
    {
        string attackingWeapon = _currentAttackingUnit.Weapon;
        string defensiveWeapon = _currentDefensiveUnit.Weapon;
        if (ThereIsNoAdvantage(defensiveWeapon, attackingWeapon)) view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        else if (AttackerHasAdvantage(attackingWeapon, defensiveWeapon)) view.WriteLine(_currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ") tiene ventaja con respecto a " + _currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ")");
        else
        {
            view.WriteLine(_currentDefensiveUnit.Name + " (" + _currentDefensiveUnit.Weapon + ") tiene ventaja con respecto a " + _currentAttackingUnit.Name + " (" + _currentAttackingUnit.Weapon + ")");
        }
    }

    private static bool AttackerHasAdvantage(string attackingWeapon, string defensiveWeapon)
    {
        return (attackingWeapon == "Sword" & defensiveWeapon == "Axe") || (attackingWeapon == "Lance" & defensiveWeapon == "Sword") || (attackingWeapon == "Axe" & defensiveWeapon == "Lance");
    }

    private static bool ThereIsNoAdvantage(string defensiveWeapon, string attackingWeapon)
    {
        return defensiveWeapon == attackingWeapon || attackingWeapon == "Magic" || defensiveWeapon == "Magic" || defensiveWeapon == "Bow" || attackingWeapon == "Bow";
    }

    public void ResetAllSkills()
    {
        ResetAttackersSkills();
        ResetDefensorsSkills();
    }

    private void ResetDefensorsSkills()
    {
        _currentAttackingUnit.ActiveBonus.ResetStructureToZero();
        _currentAttackingUnit.ActivePenalties.ResetStructureToZero();
        _currentAttackingUnit.ActiveBonusNeutralization.ResetStructureToOne();
        _currentAttackingUnit.ActivePenaltiesNeutralization.ResetStructureToOne();
    }

    private void ResetAttackersSkills()
    {
        _currentDefensiveUnit.ActiveBonus.ResetStructureToZero();
        _currentDefensiveUnit.ActivePenalties.ResetStructureToZero();
        _currentDefensiveUnit.ActiveBonusNeutralization.ResetStructureToOne();
        _currentDefensiveUnit.ActivePenaltiesNeutralization.ResetStructureToOne();
    }
}
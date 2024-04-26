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
        //PONER LOOSERS NAME COMO PARAMETRO de clase
        if (this.gameIsTerminated || this.roundIsTerminated) return "";
        SetAttacksParameters(numberOfCurrentAttack, firstPlayersCurrentUnitNumber, secondPlayersCurrentUnitNumber);
        SetAttackingAndDefensiveUnits();
        if (_numberOfThisRoundsCurrentAttack == 1)
        {
            PrintAdvantages(view);
            ActivateSkills();
            PrintSkillsInfo(view);
        }
        _attackValue = CalculateAttack();
        view.WriteLine(_currentAttackingUnit.name + " ataca a " + _currentDefensiveUnit.name + " con " + _attackValue + " de daño");
        if (currentAttacker == 0) return Player1Attacks(_attackValue);
        else
        {
            return Player2Attacks();
        }
    }

    private void SetAttacksParameters(int numberOfCurrentAttack, int firstPlayersCurrentUnitNumber,
        int secondPlayersCurrentUnitNumber)
    {
        _numberOfThisRoundsCurrentAttack = numberOfCurrentAttack;
        this._firstPlayersCurrentUnitNumber = firstPlayersCurrentUnitNumber;
        this._secondPlayersCurrentUnitNumber = secondPlayersCurrentUnitNumber;
        NameOfPlayer1sLoosingUnit = "";
        NameOfPlayer2sLoosingUnit = "";
        string loosersName = "";
    }

    private string Player2Attacks()
    {
        string loosersName;
        if (_attackValue >= players[0].units[_firstPlayersCurrentUnitNumber].currentHp)
        {
            loosersName = players[0].units[_firstPlayersCurrentUnitNumber].name;
            
            this.roundIsTerminated = true;
            EliminateLooserUnitOfPlayer1();
            return loosersName;
        }
        else
        {
            players[0].units[_firstPlayersCurrentUnitNumber].currentHp = players[0].units[_firstPlayersCurrentUnitNumber].currentHp - _attackValue;
        }
        return "";
    }

    private string Player1Attacks(int attackValue)
    {
        string loosersName;
        if (attackValue >= players[1].units[_secondPlayersCurrentUnitNumber].currentHp)
        {
            loosersName = players[1].units[_secondPlayersCurrentUnitNumber].name;
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
        _currentDefensiveUnit.currentHp -= attackValue;
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
        foreach (Skill habilidad in _currentAttackingUnit.skills)
        {
            habilidad.ApplySkills(_currentAttackingUnit, _currentDefensiveUnit, true);
        }
    }
    private void ActivateDefensorsUnitSkills()
    {
        foreach (Skill habilidad in _currentDefensiveUnit.skills)
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
        string arma_atac = _currentAttackingUnit.weapon;
        string arma_def = _currentDefensiveUnit.weapon;
        int def_o_res_rival;
        if (arma_atac == "Magic")
        {
            def_o_res_rival = _currentDefensiveUnit.res + _currentDefensiveUnit.activeBonus.res * _currentDefensiveUnit.activeBonusNeutralization.res + _currentDefensiveUnit.activePenalties.res *_currentDefensiveUnit.activePenaltiesNeutralization.res;
            if (_numberOfThisRoundsCurrentAttack == 2) def_o_res_rival += _currentDefensiveUnit.activeBonus.resFirstAttack * _currentDefensiveUnit.activeBonusNeutralization.res + _currentDefensiveUnit.activePenalties.resFirstAttack *_currentDefensiveUnit.activePenaltiesNeutralization.res;
        }
        else
        {
            def_o_res_rival = _currentDefensiveUnit.def + _currentDefensiveUnit.activeBonus.def * _currentDefensiveUnit.activeBonusNeutralization.def + _currentDefensiveUnit.activePenalties.def *_currentDefensiveUnit.activePenaltiesNeutralization.def;
            if (_numberOfThisRoundsCurrentAttack == 2) def_o_res_rival += _currentDefensiveUnit.activeBonus.defFirstAttack * _currentDefensiveUnit.activeBonusNeutralization.def + _currentDefensiveUnit.activePenalties.defFirstAttack *_currentDefensiveUnit.activePenaltiesNeutralization.def;
        }
        double wtb;
        if (arma_def == arma_atac || arma_atac == "Magic" || arma_def == "Magic" || arma_def == "Bow" || arma_atac == "Bow") wtb = 1;
        else if ((arma_atac == "Sword" & arma_def == "Axe") || (arma_atac == "Lance" & arma_def == "Sword") || (arma_atac == "Axe" & arma_def == "Lance")) wtb = 1.2;
        else
        {
            wtb = 0.8;
        }
        int atk_unidad = _currentAttackingUnit.attk + _currentAttackingUnit.activeBonus.attk * _currentAttackingUnit.activeBonusNeutralization.attk + _currentAttackingUnit.activePenalties.attk * _currentAttackingUnit.activePenaltiesNeutralization.attk;
        // revisar si pongo * neutralizador ataque o * neutralizador de atk first attack...
        if (_numberOfThisRoundsCurrentAttack == 1 || _numberOfThisRoundsCurrentAttack == 2)
        {
            atk_unidad += _currentAttackingUnit.activeBonus.atkFirstAttack * _currentAttackingUnit.activeBonusNeutralization.attk +
                          _currentAttackingUnit.activePenalties.atkFirstAttack * _currentAttackingUnit.activePenaltiesNeutralization.attk;
            // revisar esto de abajo
            _currentAttackingUnit.gameLogs.AmountOfAttacks++;
        }
        if (_numberOfThisRoundsCurrentAttack == 3)
        {
            atk_unidad += _currentAttackingUnit.activeBonus.atkFollowup * _currentAttackingUnit.activeBonusNeutralization.attk
                + _currentAttackingUnit.activePenalties.atkFollowup * _currentAttackingUnit.activePenaltiesNeutralization.attk;
            // revisar esto de abajo
            _currentAttackingUnit.gameLogs.AmountOfAttacks++;
        }
        if ((atk_unidad * wtb - def_o_res_rival) < 0) return 0;
        return Convert.ToInt32(Math.Truncate(atk_unidad * wtb - def_o_res_rival));
    }

    public void PrintAdvantages(View view)
    {
        string arma_atac = _currentAttackingUnit.weapon;
        string arma_def = _currentDefensiveUnit.weapon;
        if (arma_def == arma_atac || arma_atac == "Magic" || arma_def == "Magic" || arma_def == "Bow" || arma_atac == "Bow") view.WriteLine("Ninguna unidad tiene ventaja con respecto a la otra");
        else if ((arma_atac == "Sword" & arma_def == "Axe") || (arma_atac == "Lance" & arma_def == "Sword") || (arma_atac == "Axe" & arma_def == "Lance")) view.WriteLine(_currentAttackingUnit.name + " (" + _currentAttackingUnit.weapon + ") tiene ventaja con respecto a " + _currentDefensiveUnit.name + " (" + _currentDefensiveUnit.weapon + ")");
        else
        {
            view.WriteLine(_currentDefensiveUnit.name + " (" + _currentDefensiveUnit.weapon + ") tiene ventaja con respecto a " + _currentAttackingUnit.name + " (" + _currentAttackingUnit.weapon + ")");
        }
    }

    public void ResetAllSkills()
    {
        ResetAttackersSkills();
        ResetDefensorsSkills();
    }

    private void ResetDefensorsSkills()
    {
        _currentAttackingUnit.activeBonus.ResetStructureToZero();
        _currentAttackingUnit.activePenalties.ResetStructureToZero();
        _currentAttackingUnit.activeBonusNeutralization.ResetStructureToOne();
        _currentAttackingUnit.activePenaltiesNeutralization.ResetStructureToOne();
    }

    private void ResetAttackersSkills()
    {
        _currentDefensiveUnit.activeBonus.ResetStructureToZero();
        _currentDefensiveUnit.activePenalties.ResetStructureToZero();
        _currentDefensiveUnit.activeBonusNeutralization.ResetStructureToOne();
        _currentDefensiveUnit.activePenaltiesNeutralization.ResetStructureToOne();
    }
}
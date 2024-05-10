namespace Fire_Emblem;

public class DataStructureDamageEffects
{
    public int BonusDamageInflictedInEveryAttack = 0; //realizara ABSOLUTO
    public int BonusDamageInflictedInFirstAttack = 0; //realizara ABSOLUTO
    public int BonusDamageInflictedInFollowup = 0; //realizara ABSOLUTO
    public int DamageReductionInRivalsAttack = 1; //reduce PORCENTUAL
    public int DamageReductionInRivalsFirstAttack = 1; //reduce PORCENTUAL
    public int DamageReductionInRivalsFollowup = 1; //reduce PORCENTUAL
    public int DamageReductionInEveryAttack = 0; // REVISAR ESTO recibira ABSOLUTO
}

// LOS REDUCE (PORCENTAJE) SERAN GUARADOS PARA MULTIPLCIAR
// EJEMPLO: REDUCE EN 30%, SE GUARDA EL NÚMERO 0.7
// inicialmente es 1
//ver si inicializo aca en cero o hago funcion
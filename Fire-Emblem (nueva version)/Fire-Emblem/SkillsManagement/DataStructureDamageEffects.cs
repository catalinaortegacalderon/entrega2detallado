namespace Fire_Emblem;

public class DataStructureDamageEffects
{
    public int ExtraDamage = 0; //realizara ABSOLUTO   //daño extra
    public int ExtraDamageFirstAttack = 0; //realizara ABSOLUTO   //daño extra
    public int ExtraDamageFollowup = 0; //realizara ABSOLUTO   //daño extra
    public int PorcentualReduction = 1; //reduce PORCENTUAL    // daño porcental
    public int PorcentualReductionRivalsFirstAttack = 1; //reduce PORCENTUAL  // daño porcental
    public int PorcentualReductionRivalsFollowup = 1; //reduce PORCENTUAL  // daño porcental
    public int AbsolutDamageReduction = 0; // REVISAR ESTO recibira ABSOLUTO // daño absoluto // esto se guardara en negativo
}

// LOS REDUCE (PORCENTAJE) SERAN GUARADOS PARA MULTIPLCIAR
// EJEMPLO: REDUCE EN 30%, SE GUARDA EL NÚMERO 0.7
// inicialmente es 1
//ver si inicializo aca en cero o hago funcion
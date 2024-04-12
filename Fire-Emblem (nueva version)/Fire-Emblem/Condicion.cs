namespace Fire_Emblem;

public class Condicion
{
    public virtual bool Verificar()
    {
        return true;
    }
}

public class SiempreVerdad : Condicion
{
    public virtual bool Verificar()
    {
        return true;
    }
}



using System.Net.Mail;
using System.Runtime.CompilerServices;
using Fire_Emblem_View;
//using Fire_Emblem.Fire_Emblem;

namespace Fire_Emblem;


    // clase base, las otras heredarán de esta
    public abstract class Habilidad
    {
        protected View view;
        // si se cumple la condición, se aplica el efecto
        protected Condicion[] condiciones;
        protected Efecto[] efectos;

        public Habilidad(View view)
        {
            this.view = view;
        }

        public void AplicarHabilidades(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            // si no hay condiciones (habilidad vacia)
            if (this.condiciones.Length == 0) return;
            for (int i = 0; i < this.condiciones.Length; i++)
            {
                if (this.condiciones[i].Verificar(unidadPropia, unidadRival, atacando))
                {
                    this.efectos[i].Aplicar(unidadPropia, unidadRival, atacando);
                }
            }
        }
    }

    public class HabilidadVacia : Habilidad
    {
        public HabilidadVacia(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new EfectoVacio(view) };
        }

        public void AplicarHabilidades(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            return;
        }
    }

    // preocuparse de definir condiciones y efectos

    public class FairFight : Habilidad
    {
        public FairFight(View view) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.efectos = new Efecto[3];
        }
    }

    public class Resolve : Habilidad
    {
        public Resolve(View view) : base(view)
        {
        }
    }

    public class SpeedMas5 : Habilidad
    {
        public SpeedMas5(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new AumentarSpd(view, 5) };
        }
    }

    public class ArmoredBlow : Habilidad
    {
        public ArmoredBlow(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new UnidadIniciaCombate() };
            this.efectos = new Efecto[] { new AumentarDef(view, 8) };
        }
    }




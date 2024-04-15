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
            Console.WriteLine("pasando por aplicar hab");
            if (this.condiciones.Length == 0) return;
            for (int i = 0; i < this.condiciones.Length; i++)
            {
                
                Console.WriteLine("pasando por aplicar hab1");
                Console.WriteLine("atacando" + atacando);
                if (this.condiciones[i].Verificar(unidadPropia, unidadRival, atacando))
                {
                    Console.WriteLine("pasando por aplicar hab2");
                    Console.WriteLine("atacando" + atacando);
                    this.efectos[i].Aplicar(unidadPropia, unidadRival, atacando);
                }
            }
        }
    }

    public class HabilidadVacia : Habilidad
    {
        public HabilidadVacia(View view) : base(view)
        {
            
            //this.view = view; parece que es innecesario
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
            this.condiciones = new Condicion[2];
            // revisar esto
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();

            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 6);
            this.efectos[1] = new AumentarAtkRival(this.view, 6);
        }
    }

    public class Resolve : Habilidad
    {
        public Resolve(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.75); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.75);

            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarDefEn(this.view, 7); 
            this.efectos[1] = new CambiarResEn(this.view, 7); 
        }
    }

    public class SpeedMas5 : Habilidad
    {
        public SpeedMas5(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new CambiarSpdEn(view, 5) };
        }
    }

    public class ArmoredBlow : Habilidad
    {
        public ArmoredBlow(View view) : base(view)
        {
            Console.WriteLine("pase por constructor de armored blow");
            this.view = view;
            this.condiciones = new Condicion[] { new UnidadIniciaCombate() };
            this.efectos = new Efecto[] { new CambiarDefEn(view, 8) };
        }
    }


    public class AtkAndDefMas5 : Habilidad
    {
        public AtkAndDefMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 5); 
            this.efectos[1] = new CambiarDefEn(this.view, 5);
            
        }
    }

    public class AtkAndResMas5 : Habilidad
    {
        public AtkAndResMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 5); 
            this.efectos[1] = new CambiarResEn(this.view, 5);
            
        }
    }

    public class SpdAndResMas5 : Habilidad
    {
        public SpdAndResMas5 (View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarSpdEn(this.view, 5); 
            this.efectos[1] = new CambiarResEn(this.view, 5);
            
        }
    }


    public class AttackMas6 : Habilidad
    {
        public AttackMas6(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new CambiarAtkEn(view, 6) };
        }
    }

    public class BracingBlow : Habilidad
    {
        public BracingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarDefEn(this.view, 6);
            this.efectos[1] = new CambiarResEn(this.view, 6);
        }
    }

    public class WillToWin : Habilidad
    {
        public WillToWin(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.5); 
            this.efectos = new Efecto[1];
            this.efectos[0] = new CambiarAtkEn(this.view, 8); 
        }
    }

    public class TomePrecision : Habilidad
    {
        public TomePrecision(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UsarCiertaArma("magia");
            this.condiciones[1] = new UsarCiertaArma("magia");
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 6); 
            this.efectos[1] = new CambiarSpdEn(this.view, 6); 
        }
    }

    public class DefenseMas5 : Habilidad
    {
        public DefenseMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new CambiarDefEn(view, 5) };
        }
    }

    public class ResistanceMas5 : Habilidad
    {
        public ResistanceMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Efecto[] { new CambiarResEn(view, 5) };
        }
    }

    public class DeadlyBlade : Habilidad
    {
        public DeadlyBlade(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UsarCiertaArmaEIniciarCombate("espada");
            this.condiciones[1] = new UsarCiertaArmaEIniciarCombate("espada");
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 8); 
            this.efectos[1] = new CambiarSpdEn(this.view, 8); 
        }
    }

    public class DeathBlow : Habilidad
    {
        public DeathBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Efecto[1];
            this.efectos[0] = new CambiarAtkEn(this.view, 8);
        }
    }

    public class DartingBlow : Habilidad
    {
        public DartingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Efecto[1];
            this.efectos[0] = new CambiarSpdEn(this.view, 8);
        }
    }

    public class WardingBlow : Habilidad
    {
        public WardingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Efecto[1];
            this.efectos[0] = new CambiarResEn(this.view, 8);
        }
    }

    public class SwiftSparrow : Habilidad
    {
        public SwiftSparrow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 6);
            this.efectos[1] = new CambiarSpdEn(this.view, 6);
        }
    }

    public class SturdyBlow : Habilidad
    {
        public SturdyBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 6);
            this.efectos[1] = new CambiarDefEn(this.view, 6);
        }
    }

    public class MirrorStrike : Habilidad
    {
        public MirrorStrike(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarAtkEn(this.view, 6);
            this.efectos[1] = new CambiarResEn(this.view, 6);
        }
    }

    public class SteadyBlow : Habilidad
    {
        public SteadyBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarSpdEn(this.view, 6);
            this.efectos[1] = new CambiarDefEn(this.view, 6);
        }
    }

    public class SwiftStrike : Habilidad
    {
        public SwiftStrike(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Efecto[2];
            this.efectos[0] = new CambiarSpdEn(this.view, 6);
            this.efectos[1] = new CambiarResEn(this.view, 6);
        }
    }


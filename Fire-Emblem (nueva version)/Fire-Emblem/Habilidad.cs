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


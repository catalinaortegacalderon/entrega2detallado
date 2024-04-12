namespace Fire_Emblem;
using System; 
using Fire_Emblem_View;


    public class Efecto
    {
        protected View view; // permitir acceso desde clases hijas
        public Efecto(View view)
        {
            this.view = view;
        }

        public virtual void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            return;
        }
    }

    public class AumentarSpdEn5 : Efecto
    {
        public AumentarSpdEn5(View view) : base(view)
        {
        }

        public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            unidadPropia.spd = unidadPropia.BonusActivos.spd + 5;
            Console.WriteLine("paso por donde quiero");
            this.view.WriteLine(unidadPropia.nombre + " obtiene Spd+5");
        }
    }

    public class AumentarDefEn7 : Efecto
    {
        public AumentarDefEn7(View view) : base(view)
        {
        }

        public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            unidadPropia.def = unidadPropia.BonusActivos.def + 7;
            this.view.WriteLine(unidadPropia.nombre + " obtiene Def+7");
        }
    }

    public class AumentarResEn7 : Efecto
    {
        public AumentarResEn7(View view) : base(view)
        {
        }

        public override void Aplicar(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            unidadPropia.res = unidadPropia.BonusActivos.res + 7;
            this.view.WriteLine(unidadPropia.nombre + " obtiene Res+7");
        }
    }

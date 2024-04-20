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
        protected Effect[] efectos;

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
                if (this.condiciones[i].Verify(unidadPropia, unidadRival, atacando))
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
            
            //this.view = view; parece que es innecesario
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new EmptyEffect(view) };
        }

        public void AplicarHabilidades(Unidad unidadPropia, Unidad unidadRival, bool atacando)
        {
            return;
        }
    }

    public class HpMas15 : Habilidad
    {
        public HpMas15(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeHPIn(this.view, 15);
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

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 6);
            this.efectos[1] = new ChangeRivalsAtkIn(this.view, 6);
        }
    }

    public class Resolve : Habilidad
    {
        public Resolve(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.75); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.75);

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeDefIn(this.view, 7); 
            this.efectos[1] = new ChangeResIn(this.view, 7); 
        }
    }

    public class SpeedMas5 : Habilidad
    {
        public SpeedMas5(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeSpdIn(view, 5) };
        }
    }

    public class ArmoredBlow : Habilidad
    {
        public ArmoredBlow(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condicion[] { new UnidadIniciaCombate() };
            this.efectos = new Effect[] { new ChangeDefIn(view, 8) };
        }
    }


    public class AtkAndDefMas5 : Habilidad
    {
        public AtkAndDefMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 5); 
            this.efectos[1] = new ChangeDefIn(this.view, 5);
            
        }
    }

    public class AtkAndResMas5 : Habilidad
    {
        public AtkAndResMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 5); 
            this.efectos[1] = new ChangeResIn(this.view, 5);
            
        }
    }

    public class SpdAndResMas5 : Habilidad
    {
        public SpdAndResMas5 (View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 5); 
            this.efectos[1] = new ChangeResIn(this.view, 5);
            
        }
    }


    public class AttackMas6 : Habilidad
    {
        public AttackMas6(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeAtkIn(view, 6) };
        }
    }

    public class BracingBlow : Habilidad
    {
        public BracingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeDefIn(this.view, 6);
            this.efectos[1] = new ChangeResIn(this.view, 6);
        }
    }

    public class WillToWin : Habilidad
    {
        public WillToWin(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.5); 
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeAtkIn(this.view, 8); 
        }
    }

    public class TomePrecision : Habilidad
    {
        public TomePrecision(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UseCertainWeapon("Magic");
            this.condiciones[1] = new UseCertainWeapon("Magic");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 6); 
            this.efectos[1] = new ChangeSpdIn(this.view, 6); 
        }
    }

    public class DefenseMas5 : Habilidad
    {
        public DefenseMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeDefIn(view, 5) };
        }
    }

    public class ResistanceMas5 : Habilidad
    {
        public ResistanceMas5(View view) : base(view)
        {
            this.condiciones = new Condicion[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeResIn(view, 5) };
        }
    }

    public class DeadlyBlade : Habilidad
    {
        public DeadlyBlade(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UseCertainWeaponAndStartCombat("Sword");
            this.condiciones[1] = new UseCertainWeaponAndStartCombat("Sword");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 8); 
            this.efectos[1] = new ChangeSpdIn(this.view, 8); 
        }
    }

    public class DeathBlow : Habilidad
    {
        public DeathBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeAtkIn(this.view, 8);
        }
    }

    public class DartingBlow : Habilidad
    {
        public DartingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeSpdIn(this.view, 8);
        }
    }

    public class WardingBlow : Habilidad
    {
        public WardingBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeResIn(this.view, 8);
        }
    }

    public class SwiftSparrow : Habilidad
    {
        public SwiftSparrow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 6);
            this.efectos[1] = new ChangeSpdIn(this.view, 6);
        }
    }

    public class SturdyBlow : Habilidad
    {
        public SturdyBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 6);
            this.efectos[1] = new ChangeDefIn(this.view, 6);
        }
    }

    public class MirrorStrike : Habilidad
    {
        public MirrorStrike(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 6);
            this.efectos[1] = new ChangeResIn(this.view, 6);
        }
    }

    public class SteadyBlow : Habilidad
    {
        public SteadyBlow(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 6);
            this.efectos[1] = new ChangeDefIn(this.view, 6);
        }
    }

    public class SwiftStrike : Habilidad
    {
        public SwiftStrike(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 6);
            this.efectos[1] = new ChangeResIn(this.view, 6);
        }
    }

    public class BrazenAtkSpd : Habilidad
    {
        public BrazenAtkSpd(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 10); 
            this.efectos[1] = new ChangeSpdIn(this.view, 10); 
        }
    }

    public class BrazenAtkDef : Habilidad
    {
        public BrazenAtkDef(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 10); 
            this.efectos[1] = new ChangeDefIn(this.view, 10); 
        }
    }

    public class BrazenAtkRes : Habilidad
    {
        public BrazenAtkRes(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 10); 
            this.efectos[1] = new ChangeResIn(this.view, 10); 
        }
    }

    public class BrazenSpdDef : Habilidad
    {
        public BrazenSpdDef(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 10); 
            this.efectos[1] = new ChangeDefIn(this.view, 10); 
        }
    }

    public class BrazenSpdRes : Habilidad
    {
        public BrazenSpdRes(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 10); 
            this.efectos[1] = new ChangeResIn(this.view, 10); 
        }
    }

    public class BrazenDefRes : Habilidad
    {
        public BrazenDefRes(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeDefIn(this.view, 10); 
            this.efectos[1] = new ChangeResIn(this.view, 10); 
        }
    }

// boost heredarán de esta clase
    public class Boost : Habilidad
    {
        public Boost(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new TenerHpPropioMayorAlDelRivalAumentadoEn(3); 
            this.efectos = new Effect[1];
        }
    }


    public class FireBoost : Boost
    {
        public FireBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeAtkIn(this.view, 6); 
        }
    }

    public class WindBoost : Boost
    {
        public WindBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeSpdIn(this.view, 6); 
        }
    }

    public class EarthBoost : Boost
    {
        public EarthBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeDefIn(this.view, 6); 
        }
    }

    public class WaterBoost : Boost
    {
        public WaterBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeResIn(this.view, 6); 
        }
    }

    public class ChaosStyle : Habilidad
    {
        public ChaosStyle(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new AtaqueEntreArmasEspecificas("fisica", "magia");
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeSpdIn(this.view, 3); 
        }
    }

// penalties

    public class BlindingFlash : Habilidad
    {
        public BlindingFlash(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsSpdIn(this.view, -4); 
        }
    }

    public class NotQuite : Habilidad
    {
        public NotQuite(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new RivalIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsAtkIn(this.view, -4); 
        }
    }

    public class StunningSmile : Habilidad
    {
        public StunningSmile(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsSpdIn(this.view, -8); 
        }
    }

    public class DisarmingSigh : Habilidad
    {
        public DisarmingSigh(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsAtkIn(this.view, -8); 
        }
    }

    public class Charmer : Habilidad
    {
        public Charmer(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.condiciones[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeRivalsAtkIn(this.view, -3); 
            this.efectos[0] = new ChangeRivalsSpdIn(this.view, -3); 
        }
    }

    public class Luna : Habilidad
    {
        public Luna(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.condiciones[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[2];
            this.efectos[0] = new ReduceRivalsSpdInPercentaje(this.view, 0.5); 
            this.efectos[0] = new ReduceRivalsDefInPercentaje(this.view, 0.5); 
        }
    }

    public class BeliefInLove : Habilidad
    {
        public BeliefInLove(View view) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new StartCombatOrFullHP();
            this.condiciones[1] = new StartCombatOrFullHP();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, -5); 
            this.efectos[0] = new ChangeRivalsDefIn(this.view, -5); 
        }
    }


    public class BeorcsBlessing : Habilidad
    {
        public BeorcsBlessing(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class AgneasArrow : Habilidad
    {
        public AgneasArrow(View view) : base(view)
        {
            this.condiciones = new Condicion[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizePenalties(this.view); 
        }
    }

// hibridas

    public class Agility : Habilidad
    {
        public Agility(View view, String weapon) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeSpdIn(this.view, 12); 
            this.efectos[1] = new ChangeAtkIn(this.view, -6); 
        }
    }

    public class Power : Habilidad
    {
        public Power(View view, String weapon) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 10); 
            this.efectos[1] = new ChangeDefIn(this.view, -10); 
        }
    }

    public class Focus : Habilidad
    {
        public Focus(View view, String weapon) : base(view)
        {
            this.condiciones = new Condicion[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeAtkIn(this.view, 10); 
            this.efectos[1] = new ChangeResIn(this.view, -10); 
        }
    }

    public class CloseDef : Habilidad
    {
        public CloseDef(View view) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeDefIn(this.view, 8); 
            this.efectos[1] = new ChangeResIn(this.view, 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class DistantDef : Habilidad
    {
        public DistantDef(View view) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeDefIn(this.view, 8); 
            this.efectos[1] = new ChangeResIn(this.view, 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class Lull : Habilidad
    {
        public Lull(View view, String firstStat, String secondStat) : base(view)
        {
            this.condiciones = new Condicion[4];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.efectos = new Effect[4];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, firstStat, -3); 
            this.efectos[1] = new ChangeRivalsStatsIn(this.view, secondStat, -3); 
            this.efectos[2] = new NeutralizeOneOfOponentsBonus(this.view, firstStat); 
            this.efectos[3] = new NeutralizeOneOfOponentsBonus(this.view, secondStat); 
        }
    }

    public class Fort : Habilidad
    {
        public Fort(View view, String firstStat, String secondStat) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, firstStat, 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, secondStat, 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Atk", -2); ; 
        }
    }

//coigo repetido con el siempre verdad

    public class LifeAndDeath : Habilidad
    {
        public LifeAndDeath(View view) : base(view)
        {
            this.condiciones = new Condicion[4];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.efectos = new Effect[4];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Def", -5);
            this.efectos[3] = new ChangeStatsIn(this.view, "Res", -5);
        }
    }

    public class SolidGround : Habilidad
    {
        public SolidGround(View view) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Res", -5);
        }
    }

    public class StillWater : Habilidad
    {
        public StillWater(View view) : base(view)
        {
            this.condiciones = new Condicion[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Def", -5);
        }
    }

// solo revisar una condicion, cambiar esto
    public class DragonSkin : Habilidad
    {
        public DragonSkin(View view) : base(view)
        {
            this.condiciones = new Condicion[5];
            this.condiciones[0] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[1] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[2] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[3] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[4] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.efectos = new Effect[5];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Def", 6);
            this.efectos[3] = new ChangeStatsIn(this.view, "Res", 6);
            this.efectos[4] = new NeutralizeOponentsBonus(view);
        }
    }

    public class LightAndDark : Habilidad
    {
        public LightAndDark(View view) : base(view)
        {
            this.condiciones = new Condicion[6];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.condiciones[4] = new SiempreVerdad();
            this.condiciones[5] = new SiempreVerdad();
            this.efectos = new Effect[6];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Atk", -5); 
            this.efectos[1] = new ChangeRivalsStatsIn(this.view, "Spd", -5); 
            this.efectos[2] = new ChangeRivalsStatsIn(this.view, "Def", -5);
            this.efectos[3] = new ChangeRivalsStatsIn(this.view, "Res", -5);
            this.efectos[4] = new NeutralizePenalties(this.view);
            this.efectos[5] = new NeutralizeOponentsBonus(this.view);
        }
    }

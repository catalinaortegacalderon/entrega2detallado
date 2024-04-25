using System.Net.Mail;
using System.Runtime.CompilerServices;
using Fire_Emblem_View;
//using Fire_Emblem.Fire_Emblem;

namespace Fire_Emblem;


    // clase base, las otras heredarán de esta
    public abstract class Skill
    {
        protected View view;
        // si se cumple la condición, se aplica el efecto
        protected Condition[] condiciones;
        protected Effect[] efectos;

        public Skill(View view)
        {
            this.view = view;
        }

        public void AplicarHabilidades(Unit unitPropia, Unit OponentsUnit, bool atacando)
        {
            
            // si no hay condiciones (habilidad vacia)
            if (this.condiciones.Length == 0) return;
            for (int i = 0; i < this.condiciones.Length; i++)
            {
                if (this.condiciones[i].Verify(unitPropia, OponentsUnit, atacando))
                {
                    this.efectos[i].Aplicar(unitPropia, OponentsUnit, atacando);
                }
            }
        }
    }

    public class EmptySkill : Skill
    {
        public EmptySkill(View view) : base(view)
        {
            
            //this.view = view; parece que es innecesario
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new EmptyEffect(view) };
        }

        public void AplicarHabilidades(Unit unitPropia, Unit OponentsUnit, bool atacando)
        {
            return;
        }
    }

    public class HpMas15 : Skill
    {
        public HpMas15(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeHPIn(this.view, 15);
        }
    }

    // preocuparse de definir condiciones y efectos

    public class FairFight : Skill
    {
        public FairFight(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            // revisar esto
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",6);
            this.efectos[1] = new ChangeRivalsStatsIn(this.view, "Atk", 6);
        }
    }

    public class Resolve : Skill
    {
        public Resolve(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.75); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.75);

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Def", 7); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 7); 
        }
    }

    public class SpeedMas5 : Skill
    {
        public SpeedMas5(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn(this.view, "Spd", 5) };
        }
    }

    public class ArmoredBlow : Skill
    {
        public ArmoredBlow(View view) : base(view)
        {
            this.view = view;
            this.condiciones = new Condition[] { new UnidadIniciaCombate() };
            this.efectos = new Effect[] { new ChangeStatsIn(this.view, "Def", 8) };
        }
    }


    public class AtkAndDefMas5 : Skill
    {
        public AtkAndDefMas5(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 5); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 5);
            
        }
    }

    public class AtkAndResMas5 : Skill
    {
        public AtkAndResMas5(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 5); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 5);
            
        }
    }

    public class SpdAndResMas5 : Skill
    {
        public SpdAndResMas5 (View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 5); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 5);
            
        }
    }


    public class AttackMas6 : Skill
    {
        public AttackMas6(View view) : base(view)
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn(this.view, "Atk", 6) };
        }
    }

    public class BracingBlow : Skill
    {
        public BracingBlow(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Def", 6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Res",6);
        }
    }

    public class WillToWin : Skill
    {
        public WillToWin(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.5); 
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 8); 
        }
    }

    public class TomePrecision : Skill
    {
        public TomePrecision(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon("Magic");
            this.condiciones[1] = new UseCertainWeapon("Magic");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 6); 
        }
    }

    public class DefenseMas5 : Skill
    {
        public DefenseMas5(View view) : base(view)
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn(this.view, "Def", 5) };
        }
    }

    public class ResistanceMas5 : Skill
    {
        public ResistanceMas5(View view) : base(view)
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn(this.view, "Res", 5) };
        }
    }

    public class DeadlyBlade : Skill
    {
        public DeadlyBlade(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeaponAndStartCombat("Sword");
            this.condiciones[1] = new UseCertainWeaponAndStartCombat("Sword");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 8); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 8); 
        }
    }

    public class DeathBlow : Skill
    {
        public DeathBlow(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 8);
        }
    }

    public class DartingBlow : Skill
    {
        public DartingBlow(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 8);
        }
    }

    public class WardingBlow : Skill
    {
        public WardingBlow(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Res", 8);
        }
    }

    public class SwiftSparrow : Skill
    {
        public SwiftSparrow(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 6);
        }
    }

    public class SturdyBlow : Skill
    {
        public SturdyBlow(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Def",6);
        }
    }

    public class MirrorStrike : Skill
    {
        public MirrorStrike(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 6);
        }
    }

    public class SteadyBlow : Skill
    {
        public SteadyBlow(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 6);
        }
    }

    public class SwiftStrike : Skill
    {
        public SwiftStrike(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 6);
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 6);
        }
    }

    public class BrazenAtkSpd : Skill
    {
        public BrazenAtkSpd(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Spd", 10); 
        }
    }

    public class BrazenAtkDef : Skill
    {
        public BrazenAtkDef(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 10); 
        }
    }

    public class BrazenAtkRes : Skill
    {
        public BrazenAtkRes(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res",10); 
        }
    }

    public class BrazenSpdDef : Skill
    {
        public BrazenSpdDef(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 10); 
        }
    }

    public class BrazenSpdRes : Skill
    {
        public BrazenSpdRes(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res",10); 
        }
    }

    public class BrazenDefRes : Skill
    {
        public BrazenDefRes(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Def",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 10); 
        }
    }

// boost heredarán de esta clase
    public class Boost : Skill
    {
        public Boost(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new TenerHpPropioMayorAlDelRivalAumentadoEn(3); 
            this.efectos = new Effect[1];
        }
    }


    public class FireBoost : Boost
    {
        public FireBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
        }
    }

    public class WindBoost : Boost
    {
        public WindBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 6); 
        }
    }

    public class EarthBoost : Boost
    {
        public EarthBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeStatsIn(this.view, "Def", 6); 
        }
    }

    public class WaterBoost : Boost
    {
        public WaterBoost(View view) : base(view)
        {
            this.efectos[0] = new ChangeStatsIn(this.view, "Res", 6); 
        }
    }

    public class ChaosStyle : Skill
    {
        public ChaosStyle(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new AtaqueEntreArmasEspecificas("fisica", "magia");
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 3); 
        }
    }

// penalties

    public class BlindingFlash : Skill
    {
        public BlindingFlash(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Atk", -4); 
        }
    }

    public class NotQuite : Skill
    {
        public NotQuite(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new RivalIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Atk", -4); 
        }
    }

    public class StunningSmile : Skill
    {
        public StunningSmile(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Spd", -8); 
        }
    }

    public class DisarmingSigh : Skill
    {
        public DisarmingSigh(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Atk", -8); 
        }
    }

    public class Charmer : Skill
    {
        public Charmer(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.condiciones[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeRivalsStatsIn(this.view, "Atk", -3); 
            this.efectos[1] = new ChangeRivalsStatsIn(this.view, "Spd", -3); 
        }
    }

    public class Luna : Skill
    {
        public Luna(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.condiciones[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[2];
            this.efectos[0] = new ReduceRivalsSpdInPercentaje(this.view, 0.5); 
            this.efectos[1] = new ReduceRivalsDefInPercentaje(this.view, 0.5); 
        }
    }

    public class BeliefInLove : Skill
    {
        public BeliefInLove(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new StartCombatOrFullHP();
            this.condiciones[1] = new StartCombatOrFullHP();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", -5); 
            this.efectos[1] = new ChangeRivalsStatsIn(this.view, "Def", -5); 
        }
    }


    public class BeorcsBlessing : Skill
    {
        public BeorcsBlessing(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class AgneasArrow : Skill
    {
        public AgneasArrow(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizePenalties(this.view); 
        }
    }

// hibridas

    public class Agility : Skill
    {
        public Agility(View view, String weapon) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 12); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Atk", -6); 
        }
    }

    public class Power : Skill
    {
        public Power(View view, String weapon) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", -10); 
        }
    }

    public class Focus : Skill
    {
        public Focus(View view, String weapon) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk",10); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", -10); 
        }
    }

    public class CloseDef : Skill
    {
        public CloseDef(View view) : base(view)
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, "Def",8); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class DistantDef : Skill
    {
        public DistantDef(View view) : base(view)
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, "Def", 8); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Res", 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(this.view); 
        }
    }

    public class Lull : Skill
    {
        public Lull(View view, String firstStat, String secondStat) : base(view)
        {
            this.condiciones = new Condition[4];
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

    public class Fort : Skill
    {
        public Fort(View view, String firstStat, String secondStat) : base(view)
        {
            this.condiciones = new Condition[3];
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

    public class LifeAndDeath : Skill
    {
        public LifeAndDeath(View view) : base(view)
        {
            this.condiciones = new Condition[4];
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

    public class SolidGround : Skill
    {
        public SolidGround(View view) : base(view)
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn(this.view, "Def", 6); 
            this.efectos[2] = new ChangeStatsIn(this.view, "Res", -5);
        }
    }

    public class StillWater : Skill
    {
        public StillWater(View view) : base(view)
        {
            this.condiciones = new Condition[3];
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
    public class DragonSkin : Skill
    {
        public DragonSkin(View view) : base(view)
        {
            this.condiciones = new Condition[5];
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

    public class LightAndDark : Skill
    {
        public LightAndDark(View view) : base(view)
        {
            this.condiciones = new Condition[6];
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

    public class SingleMinded : Skill
    {
        public SingleMinded(View view) : base(view)
        {
            Console.WriteLine("creador single minded");
            this.condiciones = new Condition[1];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn(this.view, "Atk", 8); 
        }
    }

    public class Ignis : Skill
    {
        public Ignis(View view) : base(view)
        {
            // no es en el primer ataque sino que es en ataque
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatInPercentageOnlyForFirstAttack(this.view, "Atk", 0.5); 
        }
    }

    public class Perceptive : Skill
    {
        public Perceptive(View view) : base(view)
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn(this.view, "Spd", 12);
            this.efectos[1] = new ChangeStatsOnePointForEvery(this.view, "Spd", 4);
        }
    }

    public class Wrath : Skill
    {
        public Wrath(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new WrathEffect(this.view);
        }
    }

    public class Soulblade : Skill
    {
        public Soulblade(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UseCertainWeapon("Sword");
            this.efectos = new Effect[1];
            this.efectos[0] = new SoulbladeEffect(this.view);
        }
    }

    public class Sandstorm : Skill
    {
        public Sandstorm(View view) : base(view)
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new SandstormEffect(this.view);
        }
    }



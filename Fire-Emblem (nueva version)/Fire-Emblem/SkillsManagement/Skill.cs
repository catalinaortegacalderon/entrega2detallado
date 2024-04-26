using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Fire_Emblem;


    public abstract class Skill
    {
        protected Condition[] condiciones;
        protected Effect[] efectos;

        public Skill()
        {
        }

        public void AplicarHabilidades(Unit unitPropia, Unit OponentsUnit, bool atacando)
        {
            if (this.condiciones.Length == 0) return;
            for (int i = 0; i < this.condiciones.Length; i++)
            {
                if (this.condiciones[i].Verify(unitPropia, OponentsUnit, atacando))
                {
                    this.efectos[i].ApplyEffect(unitPropia, OponentsUnit, atacando);
                }
            }
        }
    }

    public class EmptySkill : Skill
    {
        public EmptySkill() : base()
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new EmptyEffect() };
        }
        public void AplicarHabilidades(Unit unitPropia, Unit OponentsUnit, bool atacando)
        {
            return;
        }
    }

    public class HpMas15 : Skill
    {
        public HpMas15() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeHPIn(15);
        }
    }

    public class FairFight : Skill
    {
        public FairFight() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",6);
            this.efectos[1] = new ChangeRivalsStatsIn("Atk", 6);
        }
    }

    public class Resolve : Skill
    {
        public Resolve() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.75); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.75);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn("Def", 7); 
            this.efectos[1] = new ChangeStatsIn( "Res", 7); 
        }
    }

    public class SpeedMas5 : Skill
    {
        public SpeedMas5() : base()
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn( "Spd", 5) };
        }
    }

    public class ArmoredBlow : Skill
    {
        public ArmoredBlow() : base()
        {
            this.condiciones = new Condition[] { new UnidadIniciaCombate() };
            this.efectos = new Effect[] { new ChangeStatsIn("Def", 8) };
        }
    }


    public class AtkAndDefMas5 : Skill
    {
        public AtkAndDefMas5() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn("Atk", 5); 
            this.efectos[1] = new ChangeStatsIn( "Def", 5);
            
        }
    }

    public class AtkAndResMas5 : Skill
    {
        public AtkAndResMas5() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 

            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 5); 
            this.efectos[1] = new ChangeStatsIn( "Res", 5);
            
        }
    }

    public class SpdAndResMas5 : Skill
    {
        public SpdAndResMas5 () : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad(); 
            this.condiciones[1] = new SiempreVerdad(); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd", 5); 
            this.efectos[1] = new ChangeStatsIn("Res", 5);
            
        }
    }


    public class AttackMas6 : Skill
    {
        public AttackMas6() : base()
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn( "Atk", 6) };
        }
    }

    public class BracingBlow : Skill
    {
        public BracingBlow() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Def", 6);
            this.efectos[1] = new ChangeStatsIn( "Res",6);
        }
    }

    public class WillToWin : Skill
    {
        public WillToWin() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.5); 
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn( "Atk", 8); 
        }
    }

    public class TomePrecision : Skill
    {
        public TomePrecision() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon("Magic");
            this.condiciones[1] = new UseCertainWeapon("Magic");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",6); 
            this.efectos[1] = new ChangeStatsIn("Spd", 6); 
        }
    }

    public class DefenseMas5 : Skill
    {
        public DefenseMas5() : base()
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn( "Def", 5) };
        }
    }

    public class ResistanceMas5 : Skill
    {
        public ResistanceMas5() : base()
        {
            this.condiciones = new Condition[] { new SiempreVerdad() };
            this.efectos = new Effect[] { new ChangeStatsIn( "Res", 5) };
        }
    }

    public class DeadlyBlade : Skill
    {
        public DeadlyBlade() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeaponAndStartCombat("Sword");
            this.condiciones[1] = new UseCertainWeaponAndStartCombat("Sword");
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 8); 
            this.efectos[1] = new ChangeStatsIn("Spd", 8); 
        }
    }

    public class DeathBlow : Skill
    {
        public DeathBlow() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn("Atk", 8);
        }
    }

    public class DartingBlow : Skill
    {
        public DartingBlow() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn( "Spd", 8);
        }
    }

    public class WardingBlow : Skill
    {
        public WardingBlow() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn("Res", 8);
        }
    }

    public class SwiftSparrow : Skill
    {
        public SwiftSparrow() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6);
            this.efectos[1] = new ChangeStatsIn( "Spd", 6);
        }
    }

    public class SturdyBlow : Skill
    {
        public SturdyBlow() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6);
            this.efectos[1] = new ChangeStatsIn( "Def",6);
        }
    }

    public class MirrorStrike : Skill
    {
        public MirrorStrike() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",6);
            this.efectos[1] = new ChangeStatsIn( "Res", 6);
        }
    }

    public class SteadyBlow : Skill
    {
        public SteadyBlow() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd", 6);
            this.efectos[1] = new ChangeStatsIn( "Def", 6);
        }
    }

    public class SwiftStrike : Skill
    {
        public SwiftStrike() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.condiciones[1] = new UnidadIniciaCombate();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd", 6);
            this.efectos[1] = new ChangeStatsIn( "Res", 6);
        }
    }

    public class BrazenAtkSpd : Skill
    {
        public BrazenAtkSpd() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",10); 
            this.efectos[1] = new ChangeStatsIn( "Spd", 10); 
        }
    }

    public class BrazenAtkDef : Skill
    {
        public BrazenAtkDef() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",10); 
            this.efectos[1] = new ChangeStatsIn( "Def", 10); 
        }
    }

    public class BrazenAtkRes : Skill
    {
        public BrazenAtkRes() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 10); 
            this.efectos[1] = new ChangeStatsIn( "Res",10); 
        }
    }

    public class BrazenSpdDef : Skill
    {
        public BrazenSpdDef() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd",10); 
            this.efectos[1] = new ChangeStatsIn( "Def", 10); 
        }
    }

    public class BrazenSpdRes : Skill
    {
        public BrazenSpdRes() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd",10); 
            this.efectos[1] = new ChangeStatsIn( "Res",10); 
        }
    }

    public class BrazenDefRes : Skill
    {
        public BrazenDefRes() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new HpPropioMenorAUnValor(0.8); 
            this.condiciones[1] = new HpPropioMenorAUnValor(0.8); 
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Def",10); 
            this.efectos[1] = new ChangeStatsIn( "Res", 10); 
        }
    }

// boost heredarán de esta clase
    public class Boost : Skill
    {
        public Boost() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new TenerHpPropioMayorAlDelRivalAumentadoEn(3); 
            this.efectos = new Effect[1];
        }
    }


    public class FireBoost : Boost
    {
        public FireBoost() : base()
        {
            this.efectos[0] = new ChangeStatsIn( "Atk", 6); 
        }
    }

    public class WindBoost : Boost
    {
        public WindBoost() : base()
        {
            this.efectos[0] = new ChangeStatsIn( "Spd", 6); 
        }
    }

    public class EarthBoost : Boost
    {
        public EarthBoost() : base()
        {
            this.efectos[0] = new ChangeStatsIn( "Def", 6); 
        }
    }

    public class WaterBoost : Boost
    {
        public WaterBoost() : base()
        {
            this.efectos[0] = new ChangeStatsIn( "Res", 6); 
        }
    }

    public class ChaosStyle : Skill
    {
        public ChaosStyle() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new AtaqueEntreArmasEspecificas("fisica", "magia");
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn( "Spd", 3); 
        }
    }

    public class BlindingFlash : Skill
    {
        public BlindingFlash() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn( "Spd", -4); 
        }
    }

    public class NotQuite : Skill
    {
        public NotQuite() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new RivalIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn( "Atk", -4); 
        }
    }

    public class StunningSmile : Skill
    {
        public StunningSmile() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn( "Spd", -8); 
        }
    }

    public class DisarmingSigh : Skill
    {
        public DisarmingSigh() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new OponentIsAMan();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeRivalsStatsIn( "Atk", -8); 
        }
    }

    public class Charmer : Skill
    {
        public Charmer() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.condiciones[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeRivalsStatsIn( "Atk", -3); 
            this.efectos[1] = new ChangeRivalsStatsIn( "Spd", -3); 
        }
    }

    public class Luna : Skill
    {
        public Luna() : base()
        {
            Console.WriteLine("creando luna");
            this.condiciones = new Condition[2];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.efectos = new Effect[2];
            this.efectos[0] = new ReduceRivalsDefInPercentajeForFirstAttack( 0.5); 
            this.efectos[1] = new ReduceRivalsResInPercentajeForFirstAttack( 0.5); 
        }
    }

    public class BeliefInLove : Skill
    {
        public BeliefInLove() : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new RivalStartCombatOrFullHP();
            this.condiciones[1] = new RivalStartCombatOrFullHP();
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeRivalsStatsIn( "Atk", -5); 
            this.efectos[1] = new ChangeRivalsStatsIn( "Def", -5); 
        }
    }


    public class BeorcsBlessing : Skill
    {
        public BeorcsBlessing() : base()
        {
            Console.WriteLine("creando boercs blessing");
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizeOponentsBonus(); 
        }
    }

    public class AgneasArrow : Skill
    {
        public AgneasArrow() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new NeutralizePenalties(); 
        }
    }
    public class Agility : Skill
    {
        public Agility(String weapon) : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Spd", 12); 
            this.efectos[1] = new ChangeStatsIn( "Atk", -6); 
        }
    }

    public class Power : Skill
    {
        public Power(String weapon) : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk", 10); 
            this.efectos[1] = new ChangeStatsIn( "Def", -10); 
        }
    }

    public class Focus : Skill
    {
        public Focus(String weapon) : base()
        {
            this.condiciones = new Condition[2];
            this.condiciones[0] = new UseCertainWeapon(weapon);
            this.condiciones[1] = new UseCertainWeapon(weapon);
            this.efectos = new Effect[2];
            this.efectos[0] = new ChangeStatsIn( "Atk",10); 
            this.efectos[1] = new ChangeStatsIn( "Res", -10); 
        }
    }

    public class CloseDef : Skill
    {
        public CloseDef() : base()
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Sword", "Lance", "Axe"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn( "Def",8); 
            this.efectos[1] = new ChangeStatsIn( "Res", 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(); 
        }
    }

    public class DistantDef : Skill
    {
        public DistantDef() : base()
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[1] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.condiciones[2] = new OponentStartsCombatWhithWeapon(["Magic", "Bow"]);
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn( "Def", 8); 
            this.efectos[1] = new ChangeStatsIn( "Res", 8); 
            this.efectos[2] = new NeutralizeOponentsBonus(); 
        }
    }

    public class Lull : Skill
    {
        public Lull(String firstStat, String secondStat) : base()
        {
            this.condiciones = new Condition[4];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.efectos = new Effect[4];
            this.efectos[0] = new ChangeRivalsStatsIn( firstStat, -3); 
            this.efectos[1] = new ChangeRivalsStatsIn( secondStat, -3); 
            this.efectos[2] = new NeutralizeOneOfOponentsBonus( firstStat); 
            this.efectos[3] = new NeutralizeOneOfOponentsBonus( secondStat); 
        }
    }

    public class Fort : Skill
    {
        public Fort(String firstStat, String secondStat) : base()
        {
            Console.WriteLine("creando fort, sats son" + firstStat + secondStat);
            this.condiciones = new Condition[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn( firstStat, 6); 
            this.efectos[1] = new ChangeStatsIn( secondStat, 6); 
            this.efectos[2] = new ChangeStatsIn( "Atk", -2); ; 
        }
    }

    public class LifeAndDeath : Skill
    {
        public LifeAndDeath() : base()
        {
            this.condiciones = new Condition[4];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.efectos = new Effect[4];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn( "Spd", 6); 
            this.efectos[2] = new ChangeStatsIn( "Def", -5);
            this.efectos[3] = new ChangeStatsIn( "Res", -5);
        }
    }

    public class SolidGround : Skill
    {
        public SolidGround() : base()
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn( "Def", 6); 
            this.efectos[2] = new ChangeStatsIn( "Res", -5);
        }
    }

    public class StillWater : Skill
    {
        public StillWater() : base()
        {
            this.condiciones = new Condition[3];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.efectos = new Effect[3];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn( "Res", 6); 
            this.efectos[2] = new ChangeStatsIn( "Def", -5);
        }
    }
    public class DragonSkin : Skill
    {
        public DragonSkin() : base()
        {
            this.condiciones = new Condition[5];
            this.condiciones[0] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[1] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[2] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[3] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.condiciones[4] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.efectos = new Effect[5];
            this.efectos[0] = new ChangeStatsIn( "Atk", 6); 
            this.efectos[1] = new ChangeStatsIn( "Spd", 6); 
            this.efectos[2] = new ChangeStatsIn( "Def", 6);
            this.efectos[3] = new ChangeStatsIn( "Res", 6);
            this.efectos[4] = new NeutralizeOponentsBonus();
        }
    }

    public class LightAndDark : Skill
    {
        public LightAndDark() : base()
        {
            this.condiciones = new Condition[6];
            this.condiciones[0] = new SiempreVerdad();
            this.condiciones[1] = new SiempreVerdad();
            this.condiciones[2] = new SiempreVerdad();
            this.condiciones[3] = new SiempreVerdad();
            this.condiciones[4] = new SiempreVerdad();
            this.condiciones[5] = new SiempreVerdad();
            this.efectos = new Effect[6];
            this.efectos[0] = new ChangeRivalsStatsIn( "Atk", -5); 
            this.efectos[1] = new ChangeRivalsStatsIn( "Spd", -5); 
            this.efectos[2] = new ChangeRivalsStatsIn( "Def", -5);
            this.efectos[3] = new ChangeRivalsStatsIn( "Res", -5);
            this.efectos[4] = new NeutralizePenalties();
            this.efectos[5] = new NeutralizeOponentsBonus();
        }
    }

    public class SingleMinded : Skill
    {
        public SingleMinded() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsIn( "Atk", 8); 
        }
    }

    public class Ignis : Skill
    {
        public Ignis() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatInPercentageOnlyForFirstAttack( "Atk", 0.5); 
        }
    }

    public class Perceptive : Skill
    {
        public Perceptive() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UnidadIniciaCombate();
            this.efectos = new Effect[1];
            this.efectos[0] = new ChangeStatsInBasePlusOnePointForEvery( "Spd", 12, 4);
        }
    }

    public class Wrath : Skill
    {
        public Wrath() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new WrathEffect();
        }
    }

    public class Soulblade : Skill
    {
        public Soulblade() : base()
        {
            this.condiciones = new Condition[1];
            this.condiciones[0] = new UseCertainWeapon("Sword");
            this.efectos = new Effect[1];
            this.efectos[0] = new SoulbladeEffect();
        }
    }

    public class Sandstorm : Skill
    {
        public Sandstorm() : base()
        {
            Console.WriteLine("crear sandstorm");
            this.condiciones = new Condition[1];
            this.condiciones[0] = new SiempreVerdad();
            this.efectos = new Effect[1];
            this.efectos[0] = new SandstormEffect();
        }
    }
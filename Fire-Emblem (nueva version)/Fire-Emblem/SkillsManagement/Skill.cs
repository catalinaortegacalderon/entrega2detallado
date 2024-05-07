using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Fire_Emblem;


    public abstract class Skill
    {
        protected Condition[] Conditions;
        protected Effect[] Effects;

        public void ApplySkills(Unit myUnit, Unit oponentsUnit, bool attacking)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, oponentsUnit,attacking))
                {
                    this.Effects[i].ApplyEffect(myUnit, oponentsUnit,attacking);
                }
            }
        }
    }

    public class EmptySkill : Skill
    {
        public EmptySkill() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrue() };
            this.Effects = new Effect[] { new EmptyEffect() };
        }
    }

    public class HpPlus15 : Skill
    {
        public HpPlus15() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeHpIn(15);
        }
    }

    public class FairFight : Skill
    {
        public FairFight() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",6);
            this.Effects[1] = new ChangeRivalsStatsIn("Atk", 6);
        }
    }

    public class Resolve : Skill
    {
        public Resolve() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.75); 
            this.Conditions[1] = new OwnHpLessThan(0.75);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn("Def", 7); 
            this.Effects[1] = new ChangeStatsIn( "Res", 7); 
        }
    }

    public class SpeedPlus5 : Skill
    {
        public SpeedPlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrue() };
            this.Effects = new Effect[] { new ChangeStatsIn( "Spd", 5) };
        }
    }

    public class ArmoredBlow : Skill
    {
        public ArmoredBlow() : base()
        {
            this.Conditions = new Condition[] { new UnitStartsCombat() };
            this.Effects = new Effect[] { new ChangeStatsIn("Def", 8) };
        }
    }


    public class AtkAndDefPlus5 : Skill
    {
        public AtkAndDefPlus5() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrue(); 
            this.Conditions[1] = new AlwaysTrue(); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn("Atk", 5); 
            this.Effects[1] = new ChangeStatsIn( "Def", 5);
            
        }
    }

    public class AtkAndResPlus5 : Skill
    {
        public AtkAndResPlus5() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrue(); 
            this.Conditions[1] = new AlwaysTrue(); 

            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 5); 
            this.Effects[1] = new ChangeStatsIn( "Res", 5);
            
        }
    }

    public class SpdAndResPlus5 : Skill
    {
        public SpdAndResPlus5 () : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrue(); 
            this.Conditions[1] = new AlwaysTrue(); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd", 5); 
            this.Effects[1] = new ChangeStatsIn("Res", 5);
            
        }
    }


    public class AttackPlus6 : Skill
    {
        public AttackPlus6() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrue() };
            this.Effects = new Effect[] { new ChangeStatsIn( "Atk", 6) };
        }
    }

    public class BracingBlow : Skill
    {
        public BracingBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Def", 6);
            this.Effects[1] = new ChangeStatsIn( "Res",6);
        }
    }

    public class WillToWin : Skill
    {
        public WillToWin() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OwnHpLessThan(0.5); 
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn( "Atk", 8); 
        }
    }

    public class TomePrecision : Skill
    {
        public TomePrecision() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UseCertainWeapon("Magic");
            this.Conditions[1] = new UseCertainWeapon("Magic");
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",6); 
            this.Effects[1] = new ChangeStatsIn("Spd", 6); 
        }
    }

    public class DefensePlus5 : Skill
    {
        public DefensePlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrue() };
            this.Effects = new Effect[] { new ChangeStatsIn( "Def", 5) };
        }
    }

    public class ResistancePlus5 : Skill
    {
        public ResistancePlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrue() };
            this.Effects = new Effect[] { new ChangeStatsIn( "Res", 5) };
        }
    }

    public class DeadlyBlade : Skill
    {
        public DeadlyBlade() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UseCertainWeaponAndStartCombat("Sword");
            this.Conditions[1] = new UseCertainWeaponAndStartCombat("Sword");
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 8); 
            this.Effects[1] = new ChangeStatsIn("Spd", 8); 
        }
    }

    public class DeathBlow : Skill
    {
        public DeathBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UnitStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn("Atk", 8);
        }
    }

    public class DartingBlow : Skill
    {
        public DartingBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UnitStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn( "Spd", 8);
        }
    }

    public class WardingBlow : Skill
    {
        public WardingBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UnitStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn("Res", 8);
        }
    }

    public class SwiftSparrow : Skill
    {
        public SwiftSparrow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6);
            this.Effects[1] = new ChangeStatsIn( "Spd", 6);
        }
    }

    public class SturdyBlow : Skill
    {
        public SturdyBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6);
            this.Effects[1] = new ChangeStatsIn( "Def",6);
        }
    }

    public class MirrorStrike : Skill
    {
        public MirrorStrike() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",6);
            this.Effects[1] = new ChangeStatsIn( "Res", 6);
        }
    }

    public class SteadyBlow : Skill
    {
        public SteadyBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd", 6);
            this.Effects[1] = new ChangeStatsIn( "Def", 6);
        }
    }

    public class SwiftStrike : Skill
    {
        public SwiftStrike() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UnitStartsCombat();
            this.Conditions[1] = new UnitStartsCombat();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd", 6);
            this.Effects[1] = new ChangeStatsIn( "Res", 6);
        }
    }

    public class BrazenAtkSpd : Skill
    {
        public BrazenAtkSpd() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",10); 
            this.Effects[1] = new ChangeStatsIn( "Spd", 10); 
        }
    }

    public class BrazenAtkDef : Skill
    {
        public BrazenAtkDef() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",10); 
            this.Effects[1] = new ChangeStatsIn( "Def", 10); 
        }
    }

    public class BrazenAtkRes : Skill
    {
        public BrazenAtkRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 10); 
            this.Effects[1] = new ChangeStatsIn( "Res",10); 
        }
    }

    public class BrazenSpdDef : Skill
    {
        public BrazenSpdDef() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd",10); 
            this.Effects[1] = new ChangeStatsIn( "Def", 10); 
        }
    }

    public class BrazenSpdRes : Skill
    {
        public BrazenSpdRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd",10); 
            this.Effects[1] = new ChangeStatsIn( "Res",10); 
        }
    }

    public class BrazenDefRes : Skill
    {
        public BrazenDefRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OwnHpLessThan(0.8); 
            this.Conditions[1] = new OwnHpLessThan(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Def",10); 
            this.Effects[1] = new ChangeStatsIn( "Res", 10); 
        }
    }

// boost heredarán de esta clase
    public class Boost : Skill
    {
        public Boost() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyHpIsLessThanRivalsHpPlus(3); 
            this.Effects = new Effect[1];
        }
    }


    public class FireBoost : Boost
    {
        public FireBoost() : base()
        {
            this.Effects[0] = new ChangeStatsIn( "Atk", 6); 
        }
    }

    public class WindBoost : Boost
    {
        public WindBoost() : base()
        {
            this.Effects[0] = new ChangeStatsIn( "Spd", 6); 
        }
    }

    public class EarthBoost : Boost
    {
        public EarthBoost() : base()
        {
            this.Effects[0] = new ChangeStatsIn( "Def", 6); 
        }
    }

    public class WaterBoost : Boost
    {
        public WaterBoost() : base()
        {
            this.Effects[0] = new ChangeStatsIn( "Res", 6); 
        }
    }

    public class ChaosStyle : Skill
    {
        public ChaosStyle() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AttackBetweenSpecificWeapons("fisica", "magia");
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn( "Spd", 3); 
        }
    }

    public class BlindingFlash : Skill
    {
        public BlindingFlash() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UnitStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsIn( "Spd", -4); 
        }
    }

    public class NotQuite : Skill
    {
        public NotQuite() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsIn( "Atk", -4); 
        }
    }

    public class StunningSmile : Skill
    {
        public StunningSmile() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OponentIsAMan();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsIn( "Spd", -8); 
        }
    }

    public class DisarmingSigh : Skill
    {
        public DisarmingSigh() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OponentIsAMan();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsIn( "Atk", -8); 
        }
    }

    public class Charmer : Skill
    {
        public Charmer() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.Conditions[1] = new CurrentOponentIsAlsoTheLastOponent();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeRivalsStatsIn( "Atk", -3); 
            this.Effects[1] = new ChangeRivalsStatsIn( "Spd", -3); 
        }
    }

    public class Luna : Skill
    {
        public Luna() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Effects = new Effect[2];
            this.Effects[0] = new ReduceRivalsDefInPercentajeForFirstAttack( 0.5); 
            this.Effects[1] = new ReduceRivalsResInPercentageForFirstAttack( 0.5); 
        }
    }

    public class BeliefInLove : Skill
    {
        public BeliefInLove() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new RivalStartsCombatOrHasFullHP();
            this.Conditions[1] = new RivalStartsCombatOrHasFullHP();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeRivalsStatsIn( "Atk", -5); 
            this.Effects[1] = new ChangeRivalsStatsIn( "Def", -5); 
        }
    }


    public class BeorcsBlessing : Skill
    {
        public BeorcsBlessing() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new NeutralizeOponentsBonus(); 
        }
    }

    public class AgneasArrow : Skill
    {
        public AgneasArrow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new NeutralizePenalties(); 
        }
    }
    public class Agility : Skill
    {
        public Agility(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UseCertainWeapon(weapon);
            this.Conditions[1] = new UseCertainWeapon(weapon);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Spd", 12); 
            this.Effects[1] = new ChangeStatsIn( "Atk", -6); 
        }
    }

    public class Power : Skill
    {
        public Power(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UseCertainWeapon(weapon);
            this.Conditions[1] = new UseCertainWeapon(weapon);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk", 10); 
            this.Effects[1] = new ChangeStatsIn( "Def", -10); 
        }
    }

    public class Focus : Skill
    {
        public Focus(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new UseCertainWeapon(weapon);
            this.Conditions[1] = new UseCertainWeapon(weapon);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsIn( "Atk",10); 
            this.Effects[1] = new ChangeStatsIn( "Res", -10); 
        }
    }

    public class CloseDef : Skill
    {
        public CloseDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new OpponentStartsCombatWithCertainWeapon(["Sword", "Lance", "Axe"]);
            this.Conditions[1] = new OpponentStartsCombatWithCertainWeapon(["Sword", "Lance", "Axe"]);
            this.Conditions[2] = new OpponentStartsCombatWithCertainWeapon(["Sword", "Lance", "Axe"]);
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsIn( "Def",8); 
            this.Effects[1] = new ChangeStatsIn( "Res", 8); 
            this.Effects[2] = new NeutralizeOponentsBonus(); 
        }
    }

    public class DistantDef : Skill
    {
        public DistantDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new OpponentStartsCombatWithCertainWeapon(["Magic", "Bow"]);
            this.Conditions[1] = new OpponentStartsCombatWithCertainWeapon(["Magic", "Bow"]);
            this.Conditions[2] = new OpponentStartsCombatWithCertainWeapon(["Magic", "Bow"]);
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsIn( "Def", 8); 
            this.Effects[1] = new ChangeStatsIn( "Res", 8); 
            this.Effects[2] = new NeutralizeOponentsBonus(); 
        }
    }

    public class Lull : Skill
    {
        public Lull(String firstStat, String secondStat) : base()
        {
            this.Conditions = new Condition[4];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Conditions[3] = new AlwaysTrue();
            this.Effects = new Effect[4];
            this.Effects[0] = new ChangeRivalsStatsIn( firstStat, -3); 
            this.Effects[1] = new ChangeRivalsStatsIn( secondStat, -3); 
            this.Effects[2] = new NeutralizeOneOfOpponentsBonus( firstStat); 
            this.Effects[3] = new NeutralizeOneOfOpponentsBonus( secondStat); 
        }
    }

    public class Fort : Skill
    {
        public Fort(String firstStat, String secondStat) : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsIn( firstStat, 6); 
            this.Effects[1] = new ChangeStatsIn( secondStat, 6); 
            this.Effects[2] = new ChangeStatsIn( "Atk", -2); ; 
        }
    }

    public class LifeAndDeath : Skill
    {
        public LifeAndDeath() : base()
        {
            this.Conditions = new Condition[4];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Conditions[3] = new AlwaysTrue();
            this.Effects = new Effect[4];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6); 
            this.Effects[1] = new ChangeStatsIn( "Spd", 6); 
            this.Effects[2] = new ChangeStatsIn( "Def", -5);
            this.Effects[3] = new ChangeStatsIn( "Res", -5);
        }
    }

    public class SolidGround : Skill
    {
        public SolidGround() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6); 
            this.Effects[1] = new ChangeStatsIn( "Def", 6); 
            this.Effects[2] = new ChangeStatsIn( "Res", -5);
        }
    }

    public class StillWater : Skill
    {
        public StillWater() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6); 
            this.Effects[1] = new ChangeStatsIn( "Res", 6); 
            this.Effects[2] = new ChangeStatsIn( "Def", -5);
        }
    }
    public class DragonSkin : Skill
    {
        public DragonSkin() : base()
        {
            this.Conditions = new Condition[5];
            this.Conditions[0] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.Conditions[1] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.Conditions[2] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.Conditions[3] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.Conditions[4] = new RivalStartsAttackOrHasHpGreaterThan(0.75);
            this.Effects = new Effect[5];
            this.Effects[0] = new ChangeStatsIn( "Atk", 6); 
            this.Effects[1] = new ChangeStatsIn( "Spd", 6); 
            this.Effects[2] = new ChangeStatsIn( "Def", 6);
            this.Effects[3] = new ChangeStatsIn( "Res", 6);
            this.Effects[4] = new NeutralizeOponentsBonus();
        }
    }

    public class LightAndDark : Skill
    {
        public LightAndDark() : base()
        {
            this.Conditions = new Condition[6];
            this.Conditions[0] = new AlwaysTrue();
            this.Conditions[1] = new AlwaysTrue();
            this.Conditions[2] = new AlwaysTrue();
            this.Conditions[3] = new AlwaysTrue();
            this.Conditions[4] = new AlwaysTrue();
            this.Conditions[5] = new AlwaysTrue();
            this.Effects = new Effect[6];
            this.Effects[0] = new ChangeRivalsStatsIn( "Atk", -5); 
            this.Effects[1] = new ChangeRivalsStatsIn( "Spd", -5); 
            this.Effects[2] = new ChangeRivalsStatsIn( "Def", -5);
            this.Effects[3] = new ChangeRivalsStatsIn( "Res", -5);
            this.Effects[4] = new NeutralizePenalties();
            this.Effects[5] = new NeutralizeOponentsBonus();
        }
    }

    public class SingleMinded : Skill
    {
        public SingleMinded() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new CurrentOponentIsAlsoTheLastOponent();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsIn( "Atk", 8); 
        }
    }

    public class Ignis : Skill
    {
        public Ignis() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatInPercentageOnlyForFirstAttack( "Atk", 0.5); 
        }
    }

    public class Perceptive : Skill
    {
        public Perceptive() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UnitStartsCombat();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInBasePlusOnePointForEvery( "Spd", 12, 4);
        }
    }

    public class Wrath : Skill
    {
        public Wrath() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new WrathEffect();
        }
    }

    public class Soulblade : Skill
    {
        public Soulblade() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new UseCertainWeapon("Sword");
            this.Effects = new Effect[1];
            this.Effects[0] = new SoulbladeEffect();
        }
    }

    public class Sandstorm : Skill
    {
        public Sandstorm() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrue();
            this.Effects = new Effect[1];
            this.Effects[0] = new SandstormEffect();
        }
    }

// SKILLS E3, TAL VEZ SEPARAR SKILLS POR TIPO (BONUS, HIBRIDAS...)

// REDUCCION DE DAÑO ABSOLUTA

// HARE LA SEPARACIÓN AL TIRO

 //   public class Gentility : Skill
  //  {
   //     public Gentility() : base()
    //    {
     //       this.Conditions = new Condition[1];
   //         this.Conditions[0] = new OpponentStartsCombatWithCertainWeapon(["Axe"]);
   //         this.Effects = new Effect[1];
    //        this.Effects[0] = new SandstormEffect();
    //    }
   // }


    
    
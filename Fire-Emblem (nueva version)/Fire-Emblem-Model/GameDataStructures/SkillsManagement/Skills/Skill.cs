using System.Net.Mail;
using System.Runtime.CompilerServices;

namespace Fire_Emblem_Model;


    public abstract class Skill
    {
        protected Condition[] Conditions;
        protected Effect[] Effects;

        public void ApplyFirstCategorySkills(Unit myUnit, Unit oponentsUnit)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, oponentsUnit) && this.Conditions[i].GetPriority() == 1)
                {
                    this.Effects[i].ApplyEffect(myUnit, oponentsUnit);
                }
            }
        }
        
        public void ApplySecondCategorySkills(Unit myUnit, Unit oponentsUnit)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, oponentsUnit) && this.Conditions[i].GetPriority() == 2)
                {
                    this.Effects[i].ApplyEffect(myUnit, oponentsUnit);
                }
            }
        }
    }

    public class EmptySkill : Skill
    {
        public EmptySkill() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            this.Effects = new Effect[] { new EmptyEffect() };
        }
    }

    public class HpPlus15 : Skill
    {
        public HpPlus15() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeHpInEffect(15);
        }
    }

    public class FairFight : Skill
    {
        public FairFight() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",6);
            this.Effects[1] = new ChangeRivalsStatsInEffect("Atk", 6);
        }
    }

    public class Resolve : Skill
    {
        public Resolve() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.75); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.75);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect("Def", 7); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 7); 
        }
    }

    public class SpeedPlus5 : Skill
    {
        public SpeedPlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            this.Effects = new Effect[] { new ChangeStatsInEffect( "Spd", 5) };
        }
    }

    public class ArmoredBlow : Skill
    {
        public ArmoredBlow() : base()
        {
            this.Conditions = new Condition[] { new MyUnitStartsCombatCondition() };
            this.Effects = new Effect[] { new ChangeStatsInEffect("Def", 8) };
        }
    }


    public class AtkAndDefPlus5 : Skill
    {
        public AtkAndDefPlus5() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrueCondition(); 
            this.Conditions[1] = new AlwaysTrueCondition(); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect("Atk", 5); 
            this.Effects[1] = new ChangeStatsInEffect( "Def", 5);
            
        }
    }

    public class AtkAndResPlus5 : Skill
    {
        public AtkAndResPlus5() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrueCondition(); 
            this.Conditions[1] = new AlwaysTrueCondition(); 

            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 5); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 5);
            
        }
    }

    public class SpdAndResPlus5 : Skill
    {
        public SpdAndResPlus5 () : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrueCondition(); 
            this.Conditions[1] = new AlwaysTrueCondition(); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 5); 
            this.Effects[1] = new ChangeStatsInEffect("Res", 5);
            
        }
    }


    public class AttackPlus6 : Skill
    {
        public AttackPlus6() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            this.Effects = new Effect[] { new ChangeStatsInEffect( "Atk", 6) };
        }
    }

    public class BracingBlow : Skill
    {
        public BracingBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Def", 6);
            this.Effects[1] = new ChangeStatsInEffect( "Res",6);
        }
    }

    public class WillToWin : Skill
    {
        public WillToWin() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.5); 
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 8); 
        }
    }

    public class TomePrecision : Skill
    {
        public TomePrecision() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition(["Magic"]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition(["Magic"]);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",6); 
            this.Effects[1] = new ChangeStatsInEffect("Spd", 6); 
        }
    }

    public class DefensePlus5 : Skill
    {
        public DefensePlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            this.Effects = new Effect[] { new ChangeStatsInEffect( "Def", 5) };
        }
    }

    public class ResistancePlus5 : Skill
    {
        public ResistancePlus5() : base()
        {
            this.Conditions = new Condition[] { new AlwaysTrueCondition() };
            this.Effects = new Effect[] { new ChangeStatsInEffect( "Res", 5) };
        }
    }

    public class DeadlyBlade : Skill
    {
        public DeadlyBlade() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AndCondition([new MyUnitStartsCombatCondition(), new MyUnitUsesCertainWeaponsCondition(["Sword"])]);
            this.Conditions[1] = new AndCondition([new MyUnitStartsCombatCondition(), new MyUnitUsesCertainWeaponsCondition(["Sword"])]);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 8); 
            this.Effects[1] = new ChangeStatsInEffect("Spd", 8); 
        }
    }

    public class DeathBlow : Skill
    {
        public DeathBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect("Atk", 8);
        }
    }

    public class DartingBlow : Skill
    {
        public DartingBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 8);
        }
    }

    public class WardingBlow : Skill
    {
        public WardingBlow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect("Res", 8);
        }
    }

    public class SwiftSparrow : Skill
    {
        public SwiftSparrow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6);
            this.Effects[1] = new ChangeStatsInEffect( "Spd", 6);
        }
    }

    public class SturdyBlow : Skill
    {
        public SturdyBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6);
            this.Effects[1] = new ChangeStatsInEffect( "Def",6);
        }
    }

    public class MirrorStrike : Skill
    {
        public MirrorStrike() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",6);
            this.Effects[1] = new ChangeStatsInEffect( "Res", 6);
        }
    }

    public class SteadyBlow : Skill
    {
        public SteadyBlow() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 6);
            this.Effects[1] = new ChangeStatsInEffect( "Def", 6);
        }
    }

    public class SwiftStrike : Skill
    {
        public SwiftStrike() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Conditions[1] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 6);
            this.Effects[1] = new ChangeStatsInEffect( "Res", 6);
        }
    }

    public class BrazenAtkSpd : Skill
    {
        public BrazenAtkSpd() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Spd", 10); 
        }
    }

    public class BrazenAtkDef : Skill
    {
        public BrazenAtkDef() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Def", 10); 
        }
    }

    public class BrazenAtkRes : Skill
    {
        public BrazenAtkRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 10); 
            this.Effects[1] = new ChangeStatsInEffect( "Res",10); 
        }
    }

    public class BrazenSpdDef : Skill
    {
        public BrazenSpdDef() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Def", 10); 
        }
    }

    public class BrazenSpdRes : Skill
    {
        public BrazenSpdRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Res",10); 
        }
    }

    public class BrazenDefRes : Skill
    {
        public BrazenDefRes() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyHpIsLessThanCondition(0.8); 
            this.Conditions[1] = new MyHpIsLessThanCondition(0.8); 
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Def",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 10); 
        }
    }

// boost heredarán de esta clase
    public class Boost : Skill
    {
        public Boost() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyHpIsLessThanOpponentsHpPlusCondition(3); 
            this.Effects = new Effect[1];
        }
    }


    public class FireBoost : Boost
    {
        public FireBoost() : base()
        {
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6); 
        }
    }

    public class WindBoost : Boost
    {
        public WindBoost() : base()
        {
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 6); 
        }
    }

    public class EarthBoost : Boost
    {
        public EarthBoost() : base()
        {
            this.Effects[0] = new ChangeStatsInEffect( "Def", 6); 
        }
    }

    public class WaterBoost : Boost
    {
        public WaterBoost() : base()
        {
            this.Effects[0] = new ChangeStatsInEffect( "Res", 6); 
        }
    }

    public class ChaosStyle : Skill
    {
        public ChaosStyle() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AttackBetweenSpecificWeaponsCondition("fisica", "magia");
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 3); 
        }
    }

    public class BlindingFlash : Skill
    {
        public BlindingFlash() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Spd", -4); 
        }
    }

    public class NotQuite : Skill
    {
        public NotQuite() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Atk", -4); 
        }
    }

    public class StunningSmile : Skill
    {
        public StunningSmile() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentIsAManCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Spd", -8); 
        }
    }

    public class DisarmingSigh : Skill
    {
        public DisarmingSigh() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentIsAManCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Atk", -8); 
        }
    }

    public class Charmer : Skill
    {
        public Charmer() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
            this.Conditions[1] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Atk", -3); 
            this.Effects[1] = new ChangeRivalsStatsInEffect( "Spd", -3); 
        }
    }

    public class Luna : Skill
    {
        public Luna() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Effects = new Effect[2];
            this.Effects[0] = new ReduceRivalsDefInPercentajeForFirstAttackEffect( 0.5); 
            this.Effects[1] = new ReduceRivalsResInPercentageForFirstAttackEffect( 0.5); 
        }
    }

    public class BeliefInLove : Skill
    {
        public BeliefInLove() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OrCondition([new OpponentHasFullHpCondition(), new OpponentStartsCombatCondition()] );
            this.Conditions[1] = new OrCondition([new OpponentHasFullHpCondition(), new OpponentStartsCombatCondition()] );
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Atk", -5); 
            this.Effects[1] = new ChangeRivalsStatsInEffect( "Def", -5); 
        }
    }


    public class BeorcsBlessing : Skill
    {
        public BeorcsBlessing() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new NeutralizeOponentsBonusEffect(); 
        }
    }

    public class AgneasArrow : Skill
    {
        public AgneasArrow() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new NeutralizePenaltiesEffect(); 
        }
    }
    public class Agility : Skill
    {
        public Agility(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Spd", 12); 
            this.Effects[1] = new ChangeStatsInEffect( "Atk", -6); 
        }
    }

    public class Power : Skill
    {
        public Power(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 10); 
            this.Effects[1] = new ChangeStatsInEffect( "Def", -10); 
        }
    }

    public class Focus : Skill
    {
        public Focus(String weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( "Atk",10); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", -10); 
        }
    }

    public class CloseDef : Skill
    {
        public CloseDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Sword", "Lance", "Axe"]), new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Sword", "Lance", "Axe"]), new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Sword", "Lance", "Axe"]), new OpponentStartsCombatCondition()]);
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( "Def",8); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 8); 
            this.Effects[2] = new NeutralizeOponentsBonusEffect(); 
        }
    }

    public class DistantDef : Skill
    {
        public DistantDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Magic", "Bow"]), new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Magic", "Bow"]), new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new AndCondition([ new OpponentUsesCertainWeaponCondition(["Magic", "Bow"]), new OpponentStartsCombatCondition()]);
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( "Def", 8); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 8); 
            this.Effects[2] = new NeutralizeOponentsBonusEffect(); 
        }
    }

    public class Lull : Skill
    {
        public Lull(String firstStat, String secondStat) : base()
        {
            this.Conditions = new Condition[4];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Conditions[3] = new AlwaysTrueCondition();
            this.Effects = new Effect[4];
            this.Effects[0] = new ChangeRivalsStatsInEffect( firstStat, -3); 
            this.Effects[1] = new ChangeRivalsStatsInEffect( secondStat, -3); 
            this.Effects[2] = new NeutralizeOneOfOpponentsBonusEffect( firstStat); 
            this.Effects[3] = new NeutralizeOneOfOpponentsBonusEffect( secondStat); 
        }
    }

    public class Fort : Skill
    {
        public Fort(String firstStat, String secondStat) : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( firstStat, 6); 
            this.Effects[1] = new ChangeStatsInEffect( secondStat, 6); 
            this.Effects[2] = new ChangeStatsInEffect( "Atk", -2); ; 
        }
    }

    public class LifeAndDeath : Skill
    {
        public LifeAndDeath() : base()
        {
            this.Conditions = new Condition[4];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Conditions[3] = new AlwaysTrueCondition();
            this.Effects = new Effect[4];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6); 
            this.Effects[1] = new ChangeStatsInEffect( "Spd", 6); 
            this.Effects[2] = new ChangeStatsInEffect( "Def", -5);
            this.Effects[3] = new ChangeStatsInEffect( "Res", -5);
        }
    }

    public class SolidGround : Skill
    {
        public SolidGround() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6); 
            this.Effects[1] = new ChangeStatsInEffect( "Def", 6); 
            this.Effects[2] = new ChangeStatsInEffect( "Res", -5);
        }
    }

    public class StillWater : Skill
    {
        public StillWater() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6); 
            this.Effects[1] = new ChangeStatsInEffect( "Res", 6); 
            this.Effects[2] = new ChangeStatsInEffect( "Def", -5);
        }
    }
    public class DragonSkin : Skill
    {
        public DragonSkin() : base()
        {
            this.Conditions = new Condition[5];
            this.Conditions[0] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), new OpponentStartsCombatCondition()]);
            this.Conditions[3] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), new OpponentStartsCombatCondition()]);
            this.Conditions[4] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), new OpponentStartsCombatCondition()]);
            this.Effects = new Effect[5];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 6); 
            this.Effects[1] = new ChangeStatsInEffect( "Spd", 6); 
            this.Effects[2] = new ChangeStatsInEffect( "Def", 6);
            this.Effects[3] = new ChangeStatsInEffect( "Res", 6);
            this.Effects[4] = new NeutralizeOponentsBonusEffect();
        }
    }

    public class LightAndDark : Skill
    {
        public LightAndDark() : base()
        {
            this.Conditions = new Condition[6];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Conditions[3] = new AlwaysTrueCondition();
            this.Conditions[4] = new AlwaysTrueCondition();
            this.Conditions[5] = new AlwaysTrueCondition();
            this.Effects = new Effect[6];
            this.Effects[0] = new ChangeRivalsStatsInEffect( "Atk", -5); 
            this.Effects[1] = new ChangeRivalsStatsInEffect( "Spd", -5); 
            this.Effects[2] = new ChangeRivalsStatsInEffect( "Def", -5);
            this.Effects[3] = new ChangeRivalsStatsInEffect( "Res", -5);
            this.Effects[4] = new NeutralizePenaltiesEffect();
            this.Effects[5] = new NeutralizeOponentsBonusEffect();
        }
    }

    public class SingleMinded : Skill
    {
        public SingleMinded() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect( "Atk", 8); 
        }
    }

    public class Ignis : Skill
    {
        public Ignis() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatInPercentageOnlyForFirstAttackEffect( "Atk", 0.5); 
        }
    }

    public class Perceptive : Skill
    {
        public Perceptive() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInBasePlusOnePointForEveryEffect( "Spd", 12, 4);
        }
    }

    public class Wrath : Skill
    {
        public Wrath() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new WrathEffect();
        }
    }

    public class Soulblade : Skill
    {
        public Soulblade() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition(["Sword"]);
            this.Effects = new Effect[1];
            this.Effects[0] = new SoulbladeEffect();
        }
    }

    public class Sandstorm : Skill
    {
        public Sandstorm() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
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


    
    
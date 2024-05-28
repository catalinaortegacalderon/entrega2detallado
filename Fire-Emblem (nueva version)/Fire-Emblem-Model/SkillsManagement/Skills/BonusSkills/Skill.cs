using System.Net.Mail;
using System.Runtime.CompilerServices;
using Fire_Emblem_Model.DataTypes;

namespace Fire_Emblem_Model;


    public abstract class Skill
    {
        protected Condition[] Conditions;
        protected Effect[] Effects;

        public void ApplyFirstCategorySkills(Unit myUnit, Unit opponentsUnit)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, opponentsUnit) && this.Conditions[i].GetPriority() == 1)
                {
                    this.Effects[i].ApplyEffect(myUnit, opponentsUnit);
                }
            }
        }
        
        public void ApplySecondCategorySkills(Unit myUnit, Unit opponentsUnit)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, opponentsUnit) && this.Conditions[i].GetPriority() == 2)
                {
                    this.Effects[i].ApplyEffect(myUnit, opponentsUnit);
                }
            }
        }
        
        public void ApplyThirdCategorySkills(Unit myUnit, Unit opponentsUnit)
        {
            if (this.Conditions.Length == 0) return;
            for (int i = 0; i < this.Conditions.Length; i++)
            {
                if (this.Conditions[i].Verify(myUnit, opponentsUnit) && this.Conditions[i].GetPriority() == 3)
                {
                    this.Effects[i].ApplyEffect(myUnit, opponentsUnit);
                }
            }
        }
    }

    public class NotQuite : Skill
    {
        public NotQuite() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentStartsCombatCondition();
            
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -4); 
        }
    }

    public class StunningSmile : Skill
    {
        public StunningSmile() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentIsAManCondition();
            
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Spd, -8); 
        }
    }

    public class DisarmingSigh : Skill
    {
        public DisarmingSigh() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new OpponentIsAManCondition();
            
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -8); 
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
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -3); 
            this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Spd, -3); 
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
            this.Effects[0] = new ReduceOpponentsDefInPercentajeForFirstAttackEffect( 0.5); 
            this.Effects[1] = new ReduceOpponentsResInPercentageForFirstAttackEffect( 0.5); 
        }
    }

    public class BeliefInLove : Skill
    {
        public BeliefInLove() : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new OrCondition([new OpponentHasFullHpCondition(),
                new OpponentStartsCombatCondition()] );
            this.Conditions[1] = new OrCondition([new OpponentHasFullHpCondition(), 
                new OpponentStartsCombatCondition()] );
            
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -5); 
            this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Def, -5); 
        }
    }


    public class BeorcsBlessing : Skill
    {
        public BeorcsBlessing() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            
            this.Effects = new Effect[1];
            this.Effects[0] = new NeutralizeOpponentsBonusEffect(); 
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
        public Agility(Weapon weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Spd, 12); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Atk, -6); 
        }
    }

    public class Power : Skill
    {
        public Power(Weapon weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 10); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Def, -10); 
        }
    }

    public class Focus : Skill
    {
        public Focus(Weapon weapon) : base()
        {
            this.Conditions = new Condition[2];
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            this.Conditions[1] = new MyUnitUsesCertainWeaponsCondition([weapon]);
            
            this.Effects = new Effect[2];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk,10); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Res, -10); 
        }
    }

    public class CloseDef : Skill
    {
        public CloseDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
                Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
                Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Sword, 
                Weapon.Lance, Weapon.Axe]), new OpponentStartsCombatCondition()]);
            
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Def,8); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 8); 
            this.Effects[2] = new NeutralizeOpponentsBonusEffect(); 
        }
    }

    public class DistantDef : Skill
    {
        public DistantDef() : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Magic,
                Weapon.Bow]), new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Magic, 
                Weapon.Bow]), new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new AndCondition([ new OpponentUsesCertainWeaponCondition([Weapon.Magic, 
                Weapon.Bow]), new OpponentStartsCombatCondition()]);
            
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Def, 8); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 8); 
            this.Effects[2] = new NeutralizeOpponentsBonusEffect(); 
        }
    }

    public class Lull : Skill
    {
        public Lull(StatType firstStat, StatType secondStat) : base()
        {
            this.Conditions = new Condition[4];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            this.Conditions[3] = new AlwaysTrueCondition();
            
            this.Effects = new Effect[4];
            this.Effects[0] = new ChangeOpponentsStatsInEffect( firstStat, -3); 
            this.Effects[1] = new ChangeOpponentsStatsInEffect( secondStat, -3); 
            this.Effects[2] = new NeutralizeOneOfOpponentsBonusEffect( firstStat); 
            this.Effects[3] = new NeutralizeOneOfOpponentsBonusEffect( secondStat); 
        }
    }

    public class Fort : Skill
    {
        public Fort(StatType firstStat, StatType secondStat) : base()
        {
            this.Conditions = new Condition[3];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Conditions[1] = new AlwaysTrueCondition();
            this.Conditions[2] = new AlwaysTrueCondition();
            
            this.Effects = new Effect[3];
            this.Effects[0] = new ChangeStatsInEffect( firstStat, 6); 
            this.Effects[1] = new ChangeStatsInEffect( secondStat, 6); 
            this.Effects[2] = new ChangeStatsInEffect( StatType.Atk, -2); ; 
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
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Spd, 6); 
            this.Effects[2] = new ChangeStatsInEffect( StatType.Def, -5);
            this.Effects[3] = new ChangeStatsInEffect( StatType.Res, -5);
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
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Def, 6); 
            this.Effects[2] = new ChangeStatsInEffect( StatType.Res, -5);
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
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Res, 6); 
            this.Effects[2] = new ChangeStatsInEffect( StatType.Def, -5);
        }
    }
    public class DragonSkin : Skill
    {
        public DragonSkin() : base()
        {
            this.Conditions = new Condition[5];
            this.Conditions[0] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
                new OpponentStartsCombatCondition()]);
            this.Conditions[1] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
                new OpponentStartsCombatCondition()]);
            this.Conditions[2] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
                new OpponentStartsCombatCondition()]);
            this.Conditions[3] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
                new OpponentStartsCombatCondition()]);
            this.Conditions[4] = new OrCondition([new OpponentHasHpGreaterThanCondition(0.75), 
                new OpponentStartsCombatCondition()]);
            
            this.Effects = new Effect[5];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 6); 
            this.Effects[1] = new ChangeStatsInEffect( StatType.Spd, 6); 
            this.Effects[2] = new ChangeStatsInEffect( StatType.Def, 6);
            this.Effects[3] = new ChangeStatsInEffect( StatType.Res, 6);
            this.Effects[4] = new NeutralizeOpponentsBonusEffect();
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
            this.Effects[0] = new ChangeOpponentsStatsInEffect( StatType.Atk, -5); 
            this.Effects[1] = new ChangeOpponentsStatsInEffect( StatType.Spd, -5); 
            this.Effects[2] = new ChangeOpponentsStatsInEffect( StatType.Def, -5);
            this.Effects[3] = new ChangeOpponentsStatsInEffect( StatType.Res, -5);
            this.Effects[4] = new NeutralizePenaltiesEffect();
            this.Effects[5] = new NeutralizeOpponentsBonusEffect();
        }
    }

    public class SingleMinded : Skill
    {
        public SingleMinded() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyCurrentOpponentIsAlsoTheLastOpponentCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInEffect( StatType.Atk, 8); 
        }
    }

    public class Ignis : Skill
    {
        public Ignis() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new AlwaysTrueCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatInPercentageOnlyForFirstAttackEffect( StatType.Atk, 0.5); 
        }
    }

    public class Perceptive : Skill
    {
        public Perceptive() : base()
        {
            this.Conditions = new Condition[1];
            this.Conditions[0] = new MyUnitStartsCombatCondition();
            this.Effects = new Effect[1];
            this.Effects[0] = new ChangeStatsInBasePlusOnePointForEveryEffect( StatType.Spd, 12, 4);
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
            this.Conditions[0] = new MyUnitUsesCertainWeaponsCondition([Weapon.Sword]);
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


    
    
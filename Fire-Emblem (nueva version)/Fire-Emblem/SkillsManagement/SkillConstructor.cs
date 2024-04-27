namespace Fire_Emblem;

public class SkillConstructor
{
    public static void Construct(Unit[][] units, int currentPlayerNumber, int[] unitCounters,
        string skillString, int skillsCounter)
    {
        Skill[] skills = units[currentPlayerNumber][unitCounters[currentPlayerNumber]].Skills;
        if (skillString == "HP +15")
        {
            skills[skillsCounter] = new HpPlus15();
        }
        else if (skillString == "Speed +5")
        {
            skills[skillsCounter] = new SpeedPlus5();
        }

        else if (skillString == "Resolve")
        {
            skills[skillsCounter] = new Resolve();
        }
        else if (skillString == "Armored Blow")
        {
            skills[skillsCounter] = new ArmoredBlow();
        }

        else if (skillString == "Fair Fight")
        {
            skills[skillsCounter] = new FairFight();
        }
        else if (skillString == "Atk/Def +5")
        {
            skills[skillsCounter] = new AtkAndDefPlus5();
        }
        else if (skillString == "Atk/Res +5")
        {
            skills[skillsCounter] = new AtkAndResPlus5();
        }
        else if (skillString == "Spd/Res +5")
        {
            skills[skillsCounter] = new SpdAndResPlus5();
        }
        else if (skillString == "Attack +6")
        {
            skills[skillsCounter] = new AttackPlus6();
        }
        else if (skillString == "Bracing Blow")
        {
            skills[skillsCounter] = new BracingBlow();
        }
            
        else if (skillString == "Will to Win")
        {
            skills[skillsCounter] = new WillToWin();
        }
        else if (skillString == "Tome Precision")
        {
            skills[skillsCounter] = new TomePrecision();
        }
        else if (skillString == "Defense +5")
        {
            skills[skillsCounter] = new DefensePlus5();
        }
        else if (skillString == "Resistance +5")
        {
            skills[skillsCounter] = new ResistancePlus5();
        }
        else if (skillString == "Deadly Blade")
        {
            skills[skillsCounter] = new DeadlyBlade();
        }
        else if (skillString == "Death Blow")
        {
            skills[skillsCounter] = new DeathBlow();
        }
        else if (skillString == "Darting Blow")
        {
            skills[skillsCounter] = new DartingBlow();
        }
        else if (skillString == "Warding Blow")
        {
            skills[skillsCounter] = new WardingBlow();
        }
        else if (skillString == "Swift Sparrow")
        {
            skills[skillsCounter] = new SwiftSparrow();
        }
        else if (skillString == "Sturdy Blow")
        {
            skills[skillsCounter] = new SturdyBlow();
        }
        else if (skillString == "Mirror Strike")
        {
            skills[skillsCounter] = new MirrorStrike();
        }
        else if (skillString == "Steady Blow")
        {
            skills[skillsCounter] = new SteadyBlow();
        }
        else if (skillString == "Swift Strike")
        {
            skills[skillsCounter] = new SwiftStrike();
        }
        else if (skillString == "Brazen Atk/Spd")
        {
            skills[skillsCounter] = new BrazenAtkSpd();
        }
        else if (skillString == "Brazen Atk/Def")
        {
            skills[skillsCounter] = new BrazenAtkDef();
        }
        else if (skillString == "Brazen Atk/Res")
        {
            skills[skillsCounter] = new BrazenAtkRes();
        }
        else if (skillString == "Brazen Spd/Def")
        {
            skills[skillsCounter] = new BrazenSpdDef();
        }
        else if (skillString == "Brazen Spd/Res")
        {
            skills[skillsCounter] = new BrazenSpdRes();
        }
        else if (skillString == "Brazen Def/Res")
        {
            skills[skillsCounter] = new BrazenDefRes();
        }
        else if (skillString == "Fire Boost")
        {
            skills[skillsCounter] = new FireBoost();
        }
        else if (skillString == "Wind Boost")
        {
            skills[skillsCounter] = new WindBoost();
        }
        else if (skillString == "Earth Boost")
        {
            skills[skillsCounter] = new EarthBoost();
        }
        else if (skillString == "Water Boost")
        {
            skills[skillsCounter] = new WaterBoost();
        }
        else if (skillString == "Chaos Style")
        {
            skills[skillsCounter] = new ChaosStyle();
        }
        else if (skillString == "Blinding Flash")
        {
            skills[skillsCounter] = new BlindingFlash();
        }
        else if (skillString == "Not *Quite*")
        {
            skills[skillsCounter] = new NotQuite();
        }
        else if (skillString == "Stunning Smile")
        {
            skills[skillsCounter] = new StunningSmile();
        }
        else if (skillString == "Disarming Sigh")
        {
            skills[skillsCounter] = new DisarmingSigh();
        }
        else if (skillString == "Charmer")
        {
            skills[skillsCounter] = new Charmer();
        }
        else if (skillString == "Luna")
        {
            skills[skillsCounter] = new Luna();
        }
        else if (skillString == "Belief in Love")
        {
            skills[skillsCounter] = new BeliefInLove();
        }
        else if (skillString == "Beorc's Blessing")
        {
            skills[skillsCounter] = new BeorcsBlessing();
        }
        else if (skillString == "Agnea's Arrow")
        {
            skills[skillsCounter] = new AgneasArrow();
        }
        else if (skillString == "Sword Agility")
        {
            skills[skillsCounter] = new Agility("Sword");
        }
        else if (skillString == "Lance Power")
        {
            skills[skillsCounter] = new Power("Lance");
        }
        else if (skillString == "Sword Power")
        {
            skills[skillsCounter] = new Power("Sword");
        }
        else if (skillString == "Bow Focus")
        {
            skills[skillsCounter] = new Focus("Bow");
        }
        else if (skillString == "Lance Agility")
        {
            skills[skillsCounter] = new Agility("Lance");
        }
        else if (skillString == "Axe Power")
        {
            skills[skillsCounter] = new Power("Axe");
        }
        else if (skillString == "Bow Agility")
        {
            skills[skillsCounter] = new Agility("Bow");
        }
        else if (skillString == "Sword Focus")
        {
            skills[skillsCounter] = new Focus("Sword");
        }
        else if (skillString == "Close Def")
        {
            skills[skillsCounter] = new CloseDef();
        }
        else if (skillString == "Distant Def")
        {
            skills[skillsCounter] = new DistantDef();
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Lull")
        {
            skills[skillsCounter] = new Lull(skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Fort.")
        {
            skills[skillsCounter] = new Fort(skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (skillString == "Life and Death")
        {
            skills[skillsCounter] = new LifeAndDeath();
        }
        else if (skillString == "Solid Ground")
        {
            skills[skillsCounter] = new SolidGround();
        }
        else if (skillString == "Still Water")
        {
            skills[skillsCounter] = new StillWater();
        }
        else if (skillString == "Dragonskin")
        {
            skills[skillsCounter] = new DragonSkin();
        }
        else if (skillString == "Light and Dark")
        {
            skills[skillsCounter] = new LightAndDark();
        }
        else if (skillString == "Single-Minded")
        {
            skills[skillsCounter] = new SingleMinded();
        }
        else if (skillString == "Ignis")
        {
            skills[skillsCounter] = new Ignis();
        }
        else if (skillString == "Perceptive")
        {
            skills[skillsCounter] = new Perceptive();
        }
        else if (skillString == "Wrath")
        {
            skills[skillsCounter] = new Wrath();
        }
        else if (skillString == "Soulblade")
        {
            skills[skillsCounter] = new Soulblade();
        }
        else if (skillString == "Sandstorm")
        {
            skills[skillsCounter] = new Sandstorm();
        }
    }
}
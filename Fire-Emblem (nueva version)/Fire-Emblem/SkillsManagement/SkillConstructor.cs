namespace Fire_Emblem;
using Fire_Emblem_View;

public class SkillConstructor
{
    public static void Construct(View view, Unit[][] units, int currentPlayerNumber, int[] contadores_unidades,
        string skillString, int skillsCounter)
    {
        Skill[] skills = units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]].skills;
        if (skillString == "HP +15")
        {
            skills[skillsCounter] = new HpMas15(view);
        }
        else if (skillString == "Speed +5")
        {
            skills[skillsCounter] = new SpeedMas5(view);
        }

        else if (skillString == "Resolve")
        {
            skills[skillsCounter] = new Resolve(view);
        }
        else if (skillString == "Armored Blow")
        {
            skills[skillsCounter] = new ArmoredBlow(view);
        }

        else if (skillString == "Fair Fight")
        {
            skills[skillsCounter] = new FairFight(view);
        }
        else if (skillString == "Atk/Def +5")
        {
            skills[skillsCounter] = new AtkAndDefMas5(view);
        }
        else if (skillString == "Atk/Res +5")
        {
            skills[skillsCounter] = new AtkAndResMas5(view);
        }
        else if (skillString == "Spd/Res +5")
        {
            skills[skillsCounter] = new SpdAndResMas5(view);
        }
        else if (skillString == "Attack +6")
        {
            skills[skillsCounter] = new AttackMas6(view);
        }
        else if (skillString == "Bracing Blow")
        {
            skills[skillsCounter] = new BracingBlow(view);
        }
            
        else if (skillString == "Will to Win")
        {
            skills[skillsCounter] = new WillToWin(view);
        }
        else if (skillString == "Tome Precision")
        {
            skills[skillsCounter] = new TomePrecision(view);
        }
        else if (skillString == "Defense +5")
        {
            skills[skillsCounter] = new DefenseMas5(view);
        }
        else if (skillString == "Resistance +5")
        {
            skills[skillsCounter] = new ResistanceMas5(view);
        }
        else if (skillString == "Deadly Blade")
        {
            skills[skillsCounter] = new DeadlyBlade(view);
        }
        else if (skillString == "Death Blow")
        {
            skills[skillsCounter] = new DeathBlow(view);
        }
        else if (skillString == "Darting Blow")
        {
            skills[skillsCounter] = new DartingBlow(view);
        }
        else if (skillString == "Warding Blow")
        {
            skills[skillsCounter] = new WardingBlow(view);
        }
        else if (skillString == "Swift Sparrow")
        {
            skills[skillsCounter] = new SwiftSparrow(view);
        }
        else if (skillString == "Sturdy Blow")
        {
            skills[skillsCounter] = new SturdyBlow(view);
        }
        else if (skillString == "Mirror Strike")
        {
            skills[skillsCounter] = new MirrorStrike(view);
        }
        else if (skillString == "Steady Blow")
        {
            skills[skillsCounter] = new SteadyBlow(view);
        }
        else if (skillString == "Swift Strike")
        {
            skills[skillsCounter] = new SwiftStrike(view);
        }
        else if (skillString == "Brazen Atk/Spd")
        {
            skills[skillsCounter] = new BrazenAtkSpd(view);
        }
        else if (skillString == "Brazen Atk/Def")
        {
            skills[skillsCounter] = new BrazenAtkDef(view);
        }
        else if (skillString == "Brazen Atk/Res")
        {
            skills[skillsCounter] = new BrazenAtkRes(view);
        }
        else if (skillString == "Brazen Spd/Def")
        {
            skills[skillsCounter] = new BrazenSpdDef(view);
        }
        else if (skillString == "Brazen Spd/Res")
        {
            skills[skillsCounter] = new BrazenSpdRes(view);
        }
        else if (skillString == "Brazen Def/Res")
        {
            skills[skillsCounter] = new BrazenDefRes(view);
        }
        else if (skillString == "Fire Boost")
        {
            skills[skillsCounter] = new FireBoost(view);
        }
        else if (skillString == "Wind Boost")
        {
            skills[skillsCounter] = new WindBoost(view);
        }
        else if (skillString == "Earth Boost")
        {
            skills[skillsCounter] = new EarthBoost(view);
        }
        else if (skillString == "Water Boost")
        {
            skills[skillsCounter] = new WaterBoost(view);
        }
        else if (skillString == "Chaos Style")
        {
            skills[skillsCounter] = new ChaosStyle(view);
        }
        else if (skillString == "Blinding Flash")
        {
            skills[skillsCounter] = new BlindingFlash(view);
        }
        else if (skillString == "Not *Quite*")
        {
            skills[skillsCounter] = new NotQuite(view);
        }
        else if (skillString == "Stunning Smile")
        {
            skills[skillsCounter] = new StunningSmile(view);
        }
        else if (skillString == "Disarming Sigh")
        {
            skills[skillsCounter] = new DisarmingSigh(view);
        }
        else if (skillString == "Charmer")
        {
            skills[skillsCounter] = new Charmer(view);
        }
        else if (skillString == "Luna")
        {
            skills[skillsCounter] = new Luna(view);
        }
        else if (skillString == "Belief in Love")
        {
            skills[skillsCounter] = new BeliefInLove(view);
        }
        else if (skillString == "Beorc's Blessing")
        {
            skills[skillsCounter] = new BeorcsBlessing(view);
        }
        else if (skillString == "Agnea's Arrow")
        {
            skills[skillsCounter] = new AgneasArrow(view);
        }
        else if (skillString == "Sword Agility")
        {
            skills[skillsCounter] = new Agility(view, "Sword");
        }
        else if (skillString == "Lance Power")
        {
            skills[skillsCounter] = new Power(view, "Lance");
        }
        else if (skillString == "Sword Power")
        {
            skills[skillsCounter] = new Power(view, "Sword");
        }
        else if (skillString == "Bow Focus")
        {
            skills[skillsCounter] = new Focus(view, "Bow");
        }
        else if (skillString == "Lance Agility")
        {
            skills[skillsCounter] = new Agility(view, "Lance");
        }
        else if (skillString == "Axe Power")
        {
            skills[skillsCounter] = new Power(view, "Axe");
        }
        else if (skillString == "Bow Agility")
        {
            skills[skillsCounter] = new Agility(view, "Bow");
        }
        else if (skillString == "Sword Focus")
        {
            skills[skillsCounter] = new Focus(view, "Sword");
        }
        else if (skillString == "Close Def")
        {
            skills[skillsCounter] = new CloseDef(view);
        }
        else if (skillString == "Distant Def")
        {
            skills[skillsCounter] = new DistantDef(view);
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Lull")
        {
            skills[skillsCounter] = new Lull(view,skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Fort.")
        {
            skills[skillsCounter] = new Fort(view,skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (skillString == "Life and Death")
        {
            skills[skillsCounter] = new LifeAndDeath(view);
        }
        else if (skillString == "Solid Ground")
        {
            skills[skillsCounter] = new SolidGround(view);
        }
        else if (skillString == "Still Water")
        {
            skills[skillsCounter] = new StillWater(view);
        }
        else if (skillString == "Dragonskin")
        {
            skills[skillsCounter] = new DragonSkin(view);
        }
        else if (skillString == "Light and Dark")
        {
            skills[skillsCounter] = new LightAndDark(view);
        }
        else if (skillString == "Single-Minded")
        {
            skills[skillsCounter] = new SingleMinded(view);
        }
        else if (skillString == "Ignis")
        {
            skills[skillsCounter] = new Ignis(view);
        }
        else if (skillString == "Perceptive")
        {
            skills[skillsCounter] = new Perceptive(view);
        }
        else if (skillString == "Wrath")
        {
            skills[skillsCounter] = new Wrath(view);
        }
        else if (skillString == "Soulblade")
        {
            skills[skillsCounter] = new Soulblade(view);
        }
        else if (skillString == "Sandstorm")
        {
            skills[skillsCounter] = new Sandstorm(view);
        }
    }
}
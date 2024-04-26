namespace Fire_Emblem;
using Fire_Emblem_View;

public class SkillConstructor
{
    public static void Construct(View view, Unit[][] units, int currentPlayerNumber, int[] contadores_unidades,
        string habilidad, int skillsCounter)
    {
        Skill[] skills = units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]].skills;
        if (habilidad == "HP +15")
        {
            skills[skillsCounter] = new HpMas15(view);
        }
        else if (habilidad == "Speed +5")
        {
            skills[skillsCounter] = new SpeedMas5(view);
        }

        else if (habilidad == "Resolve")
        {
            skills[skillsCounter] = new Resolve(view);
        }
        else if (habilidad == "Armored Blow")
        {
            skills[skillsCounter] = new ArmoredBlow(view);
        }

        else if (habilidad == "Fair Fight")
        {
            skills[skillsCounter] = new FairFight(view);
        }
        else if (habilidad == "Atk/Def +5")
        {
            skills[skillsCounter] = new AtkAndDefMas5(view);
        }
        else if (habilidad == "Atk/Res +5")
        {
            skills[skillsCounter] = new AtkAndResMas5(view);
        }
        else if (habilidad == "Spd/Res +5")
        {
            skills[skillsCounter] = new SpdAndResMas5(view);
        }
        else if (habilidad == "Attack +6")
        {
            skills[skillsCounter] = new AttackMas6(view);
        }
        else if (habilidad == "Bracing Blow")
        {
            skills[skillsCounter] = new BracingBlow(view);
        }
            
        else if (habilidad == "Will to Win")
        {
            skills[skillsCounter] = new WillToWin(view);
        }
        else if (habilidad == "Tome Precision")
        {
            skills[skillsCounter] = new TomePrecision(view);
        }
        else if (habilidad == "Defense +5")
        {
            skills[skillsCounter] = new DefenseMas5(view);
        }
        else if (habilidad == "Resistance +5")
        {
            skills[skillsCounter] = new ResistanceMas5(view);
        }
        else if (habilidad == "Deadly Blade")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DeadlyBlade(view);
        }
        else if (habilidad == "Death Blow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DeathBlow(view);
        }
        else if (habilidad == "Darting Blow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DartingBlow(view);
        }
        else if (habilidad == "Warding Blow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new WardingBlow(view);
        }
        else if (habilidad == "Swift Sparrow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SwiftSparrow(view);
        }
        else if (habilidad == "Sturdy Blow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SturdyBlow(view);
        }
        else if (habilidad == "Mirror Strike")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new MirrorStrike(view);
        }
        else if (habilidad == "Steady Blow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SteadyBlow(view);
        }
        else if (habilidad == "Swift Strike")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SwiftStrike(view);
        }
        else if (habilidad == "Brazen Atk/Spd")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenAtkSpd(view);
        }
        else if (habilidad == "Brazen Atk/Def")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenAtkDef(view);
        }
        else if (habilidad == "Brazen Atk/Res")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenAtkRes(view);
        }
        else if (habilidad == "Brazen Spd/Def")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenSpdDef(view);
        }
        else if (habilidad == "Brazen Spd/Res")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenSpdRes(view);
        }
        else if (habilidad == "Brazen Def/Res")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BrazenDefRes(view);
        }
        else if (habilidad == "Fire Boost")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new FireBoost(view);
        }
        else if (habilidad == "Wind Boost")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new WindBoost(view);
        }
        else if (habilidad == "Earth Boost")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new EarthBoost(view);
        }
        else if (habilidad == "Water Boost")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new WaterBoost(view);
        }
        else if (habilidad == "Chaos Style")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new ChaosStyle(view);
        }
        else if (habilidad == "Blinding Flash")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BlindingFlash(view);
        }
        else if (habilidad == "Not *Quite*")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new NotQuite(view);
        }
        else if (habilidad == "Stunning Smile")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new StunningSmile(view);
        }
        else if (habilidad == "Disarming Sigh")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DisarmingSigh(view);
        }
        else if (habilidad == "Charmer")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Charmer(view);
        }
        else if (habilidad == "Luna")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Luna(view);
        }
        else if (habilidad == "Belief in Love")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BeliefInLove(view);
        }
        else if (habilidad == "Beorc's Blessing")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new BeorcsBlessing(view);
        }
        else if (habilidad == "Agnea's Arrow")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new AgneasArrow(view);
        }
        else if (habilidad == "Sword Agility")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Agility(view, "Sword");
        }
        else if (habilidad == "Lance Power")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Power(view, "Lance");
        }
        else if (habilidad == "Sword Power")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Power(view, "Sword");
        }
        else if (habilidad == "Bow Focus")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Focus(view, "Bow");
        }
        else if (habilidad == "Lance Agility")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Agility(view, "Lance");
        }
        else if (habilidad == "Axe Power")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Power(view, "Axe");
        }
        else if (habilidad == "Bow Agility")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Agility(view, "Bow");
        }
        else if (habilidad == "Sword Focus")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Focus(view, "Sword");
        }
        else if (habilidad == "Close Def")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new CloseDef(view);
        }
        else if (habilidad == "Distant Def")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DistantDef(view);
        }
        else if (habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Lull")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Lull(view,habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Fort.")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Fort(view,habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                habilidad.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]);
        }
        else if (habilidad == "Life and Death")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new LifeAndDeath(view);
        }
        else if (habilidad == "Solid Ground")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SolidGround(view);
        }
        else if (habilidad == "Still Water")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new StillWater(view);
        }
        else if (habilidad == "Dragonskin")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new DragonSkin(view);
        }
        else if (habilidad == "Light and Dark")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new LightAndDark(view);
        }
        else if (habilidad == "Single-Minded")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new SingleMinded(view);
        }
        else if (habilidad == "Ignis")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Ignis(view);
        }
        else if (habilidad == "Perceptive")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Perceptive(view);
        }
        else if (habilidad == "Wrath")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Wrath(view);
        }
        else if (habilidad == "Soulblade")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Soulblade(view);
        }
        else if (habilidad == "Sandstorm")
        {
            units[currentPlayerNumber][contadores_unidades[currentPlayerNumber]]
                .skills[skillsCounter] = new Sandstorm(view);
        }
    }
}
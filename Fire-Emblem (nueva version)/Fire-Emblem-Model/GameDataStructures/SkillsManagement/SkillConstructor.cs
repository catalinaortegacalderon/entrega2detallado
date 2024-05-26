using Fire_Emblem_Model.GameDataStructures.Lists;

namespace Fire_Emblem_Model;

public class SkillConstructor
{
    public static void Construct(Unit[][] units, int currentPlayerNumber, int[] unitCounters,
        string skillString, int skillsCounter)
    {
        
        SkillsList skills = units[currentPlayerNumber][unitCounters[currentPlayerNumber]].Skills;
        if (skillString == "HP +15")
        {
            skills.AddSkill(skillsCounter, new HpPlus15());
            //skills.GetSkillByIndex(skillsCounter) = new HpPlus15());
        }
        else if (skillString == "Speed +5")
        {
            skills.AddSkill(skillsCounter, new SpeedPlus5());
        }

        else if (skillString == "Resolve")
        {
            skills.AddSkill(skillsCounter, new Resolve());
        }
        else if (skillString == "Armored Blow")
        {
            skills.AddSkill(skillsCounter, new ArmoredBlow());
        }

        else if (skillString == "Fair Fight")
        {
            skills.AddSkill(skillsCounter, new FairFight());
        }
        else if (skillString == "Atk/Def +5")
        {
            skills.AddSkill(skillsCounter, new AtkAndDefPlus5());
        }
        else if (skillString == "Atk/Res +5")
        {
            skills.AddSkill(skillsCounter, new AtkAndResPlus5());
        }
        else if (skillString == "Spd/Res +5")
        {
            skills.AddSkill(skillsCounter, new SpdAndResPlus5());
        }
        else if (skillString == "Attack +6")
        {
            skills.AddSkill(skillsCounter, new AttackPlus6());
        }
        else if (skillString == "Bracing Blow")
        {
            skills.AddSkill(skillsCounter, new BracingBlow());
        }
            
        else if (skillString == "Will to Win")
        {
            skills.AddSkill(skillsCounter, new WillToWin());
        }
        else if (skillString == "Tome Precision")
        {
            skills.AddSkill(skillsCounter, new TomePrecision());
        }
        else if (skillString == "Defense +5")
        {
            skills.AddSkill(skillsCounter, new DefensePlus5());
        }
        else if (skillString == "Resistance +5")
        {
            skills.AddSkill(skillsCounter, new ResistancePlus5());
        }
        else if (skillString == "Deadly Blade")
        {
            skills.AddSkill(skillsCounter, new DeadlyBlade());
        }
        else if (skillString == "Death Blow")
        {
            skills.AddSkill(skillsCounter, new DeathBlow());
        }
        else if (skillString == "Darting Blow")
        {
            skills.AddSkill(skillsCounter, new DartingBlow());
        }
        else if (skillString == "Warding Blow")
        {
            skills.AddSkill(skillsCounter, new WardingBlow());
        }
        else if (skillString == "Swift Sparrow")
        {
            skills.AddSkill(skillsCounter, new SwiftSparrow());
        }
        else if (skillString == "Sturdy Blow")
        {
            skills.AddSkill(skillsCounter, new SturdyBlow());
        }
        else if (skillString == "Mirror Strike")
        {
            skills.AddSkill(skillsCounter, new MirrorStrike());
        }
        else if (skillString == "Steady Blow")
        {
            skills.AddSkill(skillsCounter, new SteadyBlow());
        }
        else if (skillString == "Swift Strike")
        {
            skills.AddSkill(skillsCounter, new SwiftStrike());
        }
        else if (skillString == "Brazen Atk/Spd")
        {
            skills.AddSkill(skillsCounter, new BrazenAtkSpd());
        }
        else if (skillString == "Brazen Atk/Def")
        {
            skills.AddSkill(skillsCounter, new BrazenAtkDef());
        }
        else if (skillString == "Brazen Atk/Res")
        {
            skills.AddSkill(skillsCounter, new BrazenAtkRes());
        }
        else if (skillString == "Brazen Spd/Def")
        {
            skills.AddSkill(skillsCounter, new BrazenSpdDef());
        }
        else if (skillString == "Brazen Spd/Res")
        {
            skills.AddSkill(skillsCounter, new BrazenSpdRes());
        }
        else if (skillString == "Brazen Def/Res")
        {
            skills.AddSkill(skillsCounter, new BrazenDefRes());
        }
        else if (skillString == "Fire Boost")
        {
            skills.AddSkill(skillsCounter, new FireBoost());
        }
        else if (skillString == "Wind Boost")
        {
            skills.AddSkill(skillsCounter, new WindBoost());
        }
        else if (skillString == "Earth Boost")
        {
            skills.AddSkill(skillsCounter, new EarthBoost());
        }
        else if (skillString == "Water Boost")
        {
            skills.AddSkill(skillsCounter, new WaterBoost());
        }
        else if (skillString == "Chaos Style")
        {
            skills.AddSkill(skillsCounter, new ChaosStyle());
        }
        else if (skillString == "Blinding Flash")
        {
            skills.AddSkill(skillsCounter, new BlindingFlash());
        }
        else if (skillString == "Not *Quite*")
        {
            skills.AddSkill(skillsCounter, new NotQuite());
        }
        else if (skillString == "Stunning Smile")
        {
            skills.AddSkill(skillsCounter, new StunningSmile());
        }
        else if (skillString == "Disarming Sigh")
        {
            skills.AddSkill(skillsCounter, new DisarmingSigh());
        }
        else if (skillString == "Charmer")
        {
            skills.AddSkill(skillsCounter, new Charmer());
        }
        else if (skillString == "Luna")
        {
            skills.AddSkill(skillsCounter, new Luna());
        }
        else if (skillString == "Belief in Love")
        {
            skills.AddSkill(skillsCounter, new BeliefInLove());
        }
        else if (skillString == "Beorc's Blessing")
        {
            skills.AddSkill(skillsCounter, new BeorcsBlessing());
        }
        else if (skillString == "Agnea's Arrow")
        {
            skills.AddSkill(skillsCounter, new AgneasArrow());
        }
        else if (skillString == "Sword Agility")
        {
            skills.AddSkill(skillsCounter, new Agility("Sword"));
        }
        else if (skillString == "Lance Power")
        {
            skills.AddSkill(skillsCounter, new Power("Lance"));
        }
        else if (skillString == "Sword Power")
        {
            skills.AddSkill(skillsCounter, new Power("Sword"));
        }
        else if (skillString == "Bow Focus")
        {
            skills.AddSkill(skillsCounter, new Focus("Bow"));
        }
        else if (skillString == "Lance Agility")
        {
            skills.AddSkill(skillsCounter, new Agility("Lance"));
        }
        else if (skillString == "Axe Power")
        {
            skills.AddSkill(skillsCounter, new Power("Axe"));
        }
        else if (skillString == "Bow Agility")
        {
            skills.AddSkill(skillsCounter, new Agility("Bow"));
        }
        else if (skillString == "Sword Focus")
        {
            skills.AddSkill(skillsCounter, new Focus("Sword"));
        }
        else if (skillString == "Close Def")
        {
            skills.AddSkill(skillsCounter, new CloseDef());
        }
        else if (skillString == "Distant Def")
        {
            skills.AddSkill(skillsCounter, new DistantDef());
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Lull")
        {
            skills.AddSkill(skillsCounter, new Lull(skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]));
        }
        else if (skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[0] == "Fort.")
        {
            skills.AddSkill(skillsCounter, new Fort(skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[1],
                skillString.Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries)[2]));
        }
        else if (skillString == "Life and Death")
        {
            skills.AddSkill(skillsCounter, new LifeAndDeath());
        }
        else if (skillString == "Solid Ground")
        {
            skills.AddSkill(skillsCounter, new SolidGround());
        }
        else if (skillString == "Still Water")
        {
            skills.AddSkill(skillsCounter, new StillWater());
        }
        else if (skillString == "Dragonskin")
        {
            skills.AddSkill(skillsCounter, new DragonSkin());
        }
        else if (skillString == "Light and Dark")
        {
            skills.AddSkill(skillsCounter, new LightAndDark());
        }
        else if (skillString == "Single-Minded")
        {
            skills.AddSkill(skillsCounter, new SingleMinded());
        }
        else if (skillString == "Ignis")
        {
            skills.AddSkill(skillsCounter, new Ignis());
        }
        else if (skillString == "Perceptive")
        {
            skills.AddSkill(skillsCounter, new Perceptive());
        }
        else if (skillString == "Wrath")
        {
            skills.AddSkill(skillsCounter, new Wrath());
        }
        else if (skillString == "Soulblade")
        {
            skills.AddSkill(skillsCounter, new Soulblade());
        }
        else if (skillString == "Sandstorm")
        {
            skills.AddSkill(skillsCounter, new Sandstorm());
        }
        else if (skillString == "Gentility")
        {
            skills.AddSkill(skillsCounter, new Gentility());
        }
        else if (skillString.Split(" ").Length >= 2 && skillString.Split(" ")[1] == "Guard")
        {
            skills.AddSkill(skillsCounter, new Guard(skillString.Split(" ")[0]));
        }
        else if (skillString == "Sympathetic")
        {
            skills.AddSkill(skillsCounter, new Sympathetic());
        }
        // con armsshield me falla un tests mas que sin, ver despues porque
        else if (skillString == "Arms Shield")
        {
            skills.AddSkill(skillsCounter, new ArmsShield());
        }
        // tal vez separar posture aca
        else if (skillString.Split(" ").Length >= 2 && (skillString.Split(" ")[1] == "Stance" || skillString.Split(" ")[1] == "Posture"))
        {
            // tal vez codigo rep aca
            if (skillString.Split(" ")[0] == "Fierce")
            {
                skills.AddSkill(skillsCounter, new Stance("Atk", "", 8, 0));
            }

            if (skillString.Split(" ")[0] == "Darting")
            {
                skills.AddSkill(skillsCounter, new Stance("Spd", "", 8, 0));
            }

            if (skillString.Split(" ")[0] == "Steady" && skillString.Split(" ")[1] == "Stance")
            {
                skills.AddSkill(skillsCounter, new Stance("Def", "", 8, 0));
            }

            if (skillString.Split(" ")[0] == "Warding")
            {
                skills.AddSkill(skillsCounter, new Stance("Res", "", 8, 0));
            }

            if (skillString.Split(" ")[0] == "Kestrel")
            {
                skills.AddSkill(skillsCounter, new Stance("Atk", "Spd", 6, 6));
            }

            if (skillString.Split(" ")[0] == "Sturdy")
            {
                skills.AddSkill(skillsCounter, new Stance("Atk", "Def", 6, 6));
            }

            if (skillString.Split(" ")[0] == "Mirror")
            {
                skills.AddSkill(skillsCounter, new Stance("Atk", "Res", 6, 6));
            }

            if (skillString.Split(" ")[0] == "Steady" && skillString.Split(" ")[1] == "Posture")
            {
                skills.AddSkill(skillsCounter, new Stance("Spd", "Def", 6, 6));
            }

            if (skillString.Split(" ")[0] == "Swift")
            {
                skills.AddSkill(skillsCounter, new Stance("Spd", "Res", 6, 6));
            }

            if (skillString.Split(" ")[0] == "Bracing")
            {
                skills.AddSkill(skillsCounter, new Stance("Def", "Res", 6, 6));
            }
        }
        else if (skillString == "Dragon Wall")
        { 
            skills.AddSkill(skillsCounter, new DragonWall());
        }
        else if (skillString == "Dodge")
        { 
            skills.AddSkill(skillsCounter, new Dodge());
        }
        else if (skillString == "Golden Lotus")
        { 
            skills.AddSkill(skillsCounter, new GoldenLotus());
        }
        else if (skillString == "Bravery")
        { 
            skills.AddSkill(skillsCounter, new Bravery());
        }
        else if (skillString == "Back at You")
        { 
            skills.AddSkill(skillsCounter, new BackAtYou());
        }
        else if (skillString == "Lunar Brace")
        { 
            skills.AddSkill(skillsCounter, new LunarBrace());
        }
        else if (skillString == "Bushido")
        { 
            skills.AddSkill(skillsCounter, new Bushido());
        }
        else if (skillString == "Moon-Twin Wing")
        { 
            skills.AddSkill(skillsCounter, new MoonTwinWing());
        }
        else if (skillString == "Blue Skies")
        { 
            skills.AddSkill(skillsCounter, new BlueSkies());
        }
        else if (skillString == "Aegis Shield")
        { 
            skills.AddSkill(skillsCounter, new AegisShield());
        }
        else if (skillString == "Remote Sparrow")
        { 
            skills.AddSkill(skillsCounter, new RemoteSparrow());
        }
        else if (skillString == "Remote Mirror")
        { 
            skills.AddSkill(skillsCounter, new RemoteMirror());
        }
        else if (skillString == "Remote Sturdy")
        { 
            skills.AddSkill(skillsCounter, new RemoteSturdy());
        }
        else if (skillString == "Poetic Justice")
        { 
            skills.AddSkill(skillsCounter, new PoeticJustice());
        }
        else if (skillString == "Laguz Friend")
        { 
            skills.AddSkill(skillsCounter, new LaguzFriend());
        }
        else if (skillString == "Chivalry")
        { 
            skills.AddSkill(skillsCounter, new Chivalry());
        }
        else if (skillString == "Dragon's Wrath")
        { 
            skills.AddSkill(skillsCounter, new DragonsWrath());
        }
        else if (skillString == "Prescience")
        { 
            skills.AddSkill(skillsCounter, new Prescience());
        }
        else if (skillString == "Extra Chivalry")
        { 
            skills.AddSkill(skillsCounter, new ExtraChilvary());
        }
        else if (skillString == "Guard Bearing")
        { 
            skills.AddSkill(skillsCounter, new GuardBearing());
        }
        else if (skillString == "Divine Recreation")
        { 
            skills.AddSkill(skillsCounter, new DivineRecreation());
        }
    }
}
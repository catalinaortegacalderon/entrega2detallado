using ConsoleApp1.DataTypes;
using ConsoleApp1.EncapsulatedLists;
using ConsoleApp1.GameDataStructures;
using ConsoleApp1.SkillsManagement.Skills.AbsolutDamageReductionSkills;
using ConsoleApp1.SkillsManagement.Skills.BonusSkills;
using ConsoleApp1.SkillsManagement.Skills.ExtraDamageSkills;
using ConsoleApp1.SkillsManagement.Skills.HybridSkills;
using ConsoleApp1.SkillsManagement.Skills.PercentualDamageReductionSkills;

namespace ConsoleApp1.SkillsManagement;

public class SkillConstructor
{
    public static void Construct(Unit[][] units, int currentPlayerNumber, int[] unitCounters,
        string skillString, int skillsCounter)
    {
        
        SkillsList skills = units[currentPlayerNumber][unitCounters[currentPlayerNumber]].Skills;
        if (skillString == "HP +15")
        {
            skills.AddSkill(skillsCounter, new HpPlus15());
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
            skills.AddSkill(skillsCounter, new Agility(Weapon.Sword));
        }
        else if (skillString == "Lance Power")
        {
            skills.AddSkill(skillsCounter, new Power(Weapon.Lance));
        }
        else if (skillString == "Sword Power")
        {
            skills.AddSkill(skillsCounter, new Power(Weapon.Sword));
        }
        else if (skillString == "Bow Focus")
        {
            skills.AddSkill(skillsCounter, new Focus(Weapon.Bow));
        }
        else if (skillString == "Lance Agility")
        {
            skills.AddSkill(skillsCounter, new Agility(Weapon.Lance));
        }
        else if (skillString == "Axe Power")
        {
            skills.AddSkill(skillsCounter, new Power(Weapon.Axe));
        }
        else if (skillString == "Bow Agility")
        {
            skills.AddSkill(skillsCounter, new Agility(Weapon.Bow));
        }
        else if (skillString == "Sword Focus")
        {
            skills.AddSkill(skillsCounter, new Focus(Weapon.Sword));
        }
        else if (skillString == "Close Def")
        {
            skills.AddSkill(skillsCounter, new CloseDef());
        }
        else if (skillString == "Distant Def")
        {
            skills.AddSkill(skillsCounter, new DistantDef());
        }
        else if (DoesStringContain(skillString, "Lull"))
        {
            skills.AddSkill(skillsCounter, new Lull(
                GetStatFromString(skillString, 1),
                GetStatFromString(skillString, 2)));
        }
        else if (DoesStringContain(skillString, "Fort."))
        {
            skills.AddSkill(skillsCounter, new Fort(
                GetStatFromString(skillString, 1),
                GetStatFromString(skillString, 2)));
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
            CreateGuard(skillString, skillsCounter, skills);
        }
        else if (skillString == "Sympathetic")
        {
            skills.AddSkill(skillsCounter, new Sympathetic());
        }
        else if (skillString == "Arms Shield")
        {
            skills.AddSkill(skillsCounter, new ArmsShield());
        }
        else if (skillString.Split(" ").Length >= 2 && (skillString.Split(" ")[1] == "Stance" 
                                                        || skillString.Split(" ")[1] == "Posture"))
        {
            CreateStance(skillString, skillsCounter, skills);
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
            skills.AddSkill(skillsCounter, new ExtraChivalry());
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

    private static StatType GetStatFromString(string skillString, int statNumber)
    {
        var statAsString = skillString.Split(new char[] { ' ', '/' }, 
            StringSplitOptions.RemoveEmptyEntries)[statNumber];
        return ConvertStatStringToStatType(statAsString);
    }

    private static bool DoesStringContain(string skillString, string skillName)
    {
        return skillString.Split(new char[] { ' ', '/' }, 
            StringSplitOptions.RemoveEmptyEntries)[0] == skillName;
    }

    private static void CreateStance(string skillString, int skillsCounter, SkillsList skills)
    {
        if (skillString.Split(" ")[0] == "Fierce")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Atk, 
                StatType.None, 8, 0));
        }

        if (skillString.Split(" ")[0] == "Darting")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Spd, 
                StatType.None, 8, 0));
        }

        if (skillString.Split(" ")[0] == "Steady" && skillString.Split(" ")[1] == "Stance")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Def, 
                StatType.None, 8, 0));
        }

        if (skillString.Split(" ")[0] == "Warding")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Res, 
                StatType.None, 8, 0));
        }

        if (skillString.Split(" ")[0] == "Kestrel")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Atk, 
                StatType.Spd, 6, 6));
        }

        if (skillString.Split(" ")[0] == "Sturdy")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Atk, 
                StatType.Def, 6, 6));
        }

        if (skillString.Split(" ")[0] == "Mirror")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Atk, 
                StatType.Res, 6, 6));
        }

        if (skillString.Split(" ")[0] == "Steady" && skillString.Split(" ")[1] == "Posture")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Spd, 
                StatType.Def, 6, 6));
        }

        if (skillString.Split(" ")[0] == "Swift")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Spd, 
                StatType.Res, 6, 6));
        }

        if (skillString.Split(" ")[0] == "Bracing")
        {
            skills.AddSkill(skillsCounter, new Stance(StatType.Def, 
                StatType.Res, 6, 6));
        }
    }

    private static void CreateGuard(string skillString, int skillsCounter, SkillsList skills)
    {
        Weapon weapon = Weapon.Empty;
        string weaponString = skillString.Split(" ")[0];
        if (weaponString == "Magic")
        { 
            weapon =  Weapon.Magic;
        }
        if (weaponString == "Axe")
        {
            weapon = Weapon.Axe;
        }
        if (weaponString == "Lance")
        {
            weapon = Weapon.Lance;
        }

        if (weaponString == "Bow")
        {
            weapon = Weapon.Bow;
        }
        if (weaponString == "Sword")
        {
            weapon = Weapon.Sword;
        }
        skills.AddSkill(skillsCounter, new Guard(weapon)) ;
    }
    
    private static StatType ConvertStatStringToStatType(string statString)
    {
        StatType stat = StatType.None;
        if (statString == "Atk")
        {
            stat = StatType.Atk;
        }
        else if (statString == "Res")
        {
            stat = StatType.Res;
        }
        else if (statString == "Def")
        {
            stat = StatType.Def;
        }
        else if (statString == "Spd")
        {
            stat = StatType.Spd;
        }
        return stat;
    }
}
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Evil/NecroticSwordOfDoom.cs
//cs_include Scripts/Farm/LVLQuickto100.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Various/DageChallengeMerge.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Good/BLOD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using RBot;

public class NecroticBladeoftheUnderworld
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public NecroticSwordOfDoom NSoD = new();
    public LVLQuick LVL = new();
    public CoreLegion Legion = new();
    public DageChallengeMerge DageChallengeMerge = new();
    public AnotherOneBitesTheDust SoulSand = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Legion Token", "Beast Soul", "Soul Sand", "Dage the Evil Insignia" });

        Core.SetOptions();

        GetNBoU();

        Core.SetOptions(false);
    }

    public void Event_RunToArea(ScriptInterface bot, string zone)
    {
            switch (zone.ToLower())
        {
            case "a":
                //Move to the left
                bot.Player.WalkTo(Bot.Runtime.Random.Next(40, 175), Bot.Runtime.Random.Next(400, 410), speed: 8);
                break;
            case "b":
                //Move to the right
                bot.Player.WalkTo(Bot.Runtime.Random.Next(760, 930), Bot.Runtime.Random.Next(410, 415), speed: 8);
                break;
            default:
                //Move to the center
                bot.Player.WalkTo(Bot.Runtime.Random.Next(480, 500), Bot.Runtime.Random.Next(300, 420), speed: 8);
                break;
        }
    }

    public void GetNBoU()
    {
        Bot.Events.RunToArea += Event_RunToArea;

        //Necessary AddDrops
        Core.AddDrop(new[] { "Necrotic Blade of the Underworld", "Underworld Blade of DOOM", "Necrotic Sword of Doom", "Legion Token", "Beast Soul", "Soul Sand", "Dage the Evil Insignia" });

        //Item Check
        if (Core.CheckInventory("Necrotic Blade of the Underworld"))
            return;

        //Story Preloading
        Story.PreLoad();

        //Leveling to 95
        LVL.QuickLvl(95);

        //Unlocking Quest
        DageChallengeMerge.DageQuests();

        Core.EnsureAccept(8548);

        //Get Necrotic Sword of Doom
        if (!Core.CheckInventory("Necrotic Sword of Doom"))
        {
            Core.Logger("Getting Necrotic Sword of Doom (This Will Take a LONG Time");
            NSoD.GetNSOD();
        }

        //Underworld Blade of DOOM
        if (!Core.CheckInventory("Underworld Blade of DOOM"))
        {
            Adv.BoostHuntMonster("Dage", "Dage the Evil", "Underworld Blade of DOOM", isTemp: false, publicRoom: false);
            Bot.Wait.ForPickup("Underworld Blade of DOOM");
        }

        //Farm 25,000 Legion Tokens
        if (!Core.CheckInventory("Legion Token", 25000))
            Legion.FarmLegionToken();

        //Beast Souls
        if (!Core.CheckInventory("Beast Soul", 25))
            Adv.BoostKillMonster("SevenCirclesWar", "r17", "Left", "The Beast", "Beast Soul", 25, isTemp: false, publicRoom: false);

        //Soul Sand
        if (!Core.CheckInventory("Soul Sand", 7))
            SoulSand.SoulSand(7);

        //Dage the Evil Insignia
        if (!Core.CheckInventory("Dage the Evil Insignia", 5))
        {
            Core.EnsureAccept(8547);
            Core.EquipClass(ClassType.Solo);

            Adv.BoostKillMonster("UltraDage", "Boss", "Right", "Dage the Dark Lord", "Dage the Dark Lord Defeated", isTemp: false, publicRoom: false);

            Core.EnsureComplete(8547);
            Bot.Wait.ForPickup("Dage the Evil Insignia");
        }

        Core.EnsureComplete(8548);
        Bot.Wait.ForPickup("Necrotic Blade of the Underworld");

        Bot.Events.RunToArea -= Event_RunToArea;
    }
}
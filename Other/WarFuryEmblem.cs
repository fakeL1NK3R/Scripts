//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/WarTraining.cs
using RBot;

public class WarfuryEmblem
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public WarTraining WFT = new();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        WarfuryEmblemFarm();

        Core.SetOptions(false);
    }

    public void WarfuryEmblemFarm(int EmblemQuant = 60)
    {
        if (Core.CheckInventory("Warfury Emblem", EmblemQuant))
            return;

        WFT.StoryLine();

        Core.AddDrop("Warfury Emblem");
        Adv.BestGear(GearBoost.Human);
        while (!Core.CheckInventory("Warfury Emblem", EmblemQuant))
        {
            Core.EnsureAccept(8204);
            Core.HuntMonster("wartraining", "Warfury Soldier", "Warfury Training", 30);
            Core.EnsureComplete(8204);
            Bot.Wait.ForPickup("Warfury Emblem");
        }
    }
}
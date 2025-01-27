//cs_include Scripts/CoreBots.cs

using RBot;

public class DecorateYourSpace
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Getthestuff();

        Core.SetOptions(false);
    }

    public void Getthestuff()
    {
        List<RBot.Items.ItemBase> RewardOptions = Core.EnsureLoad(7782).Rewards;
        List<string> RewardsList = new List<string>();
        foreach (RBot.Items.ItemBase Item in RewardOptions)
            RewardsList.Add(Item.Name);

        string[] Rewards = RewardsList.ToArray();

        Core.AddDrop(Rewards);

        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(7782);
        while (!Core.CheckInventory(Rewards) && !Bot.ShouldExit())
            Core.KillMonster("yokaigrave", "r2", "Left", "*", "Graves Cleared", 10, log: false);
        Core.CancelRegisteredQuests();
    }
}

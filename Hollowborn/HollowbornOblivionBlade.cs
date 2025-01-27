//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nulgath/CoreNulgath.cs

using RBot;

public class HollowbornOblivionBlade
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreHollowborn HB = new CoreHollowborn();
    public CoreNulgath Nulgath = new CoreNulgath();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        GetBlade();
        Adv.EnhanceItem("Hollowborn Oblivion Blade", EnhancementType.Lucky, WeaponSpecial.Spiral_Carve);

        Core.SetOptions(false);
    }

    public void GetBlade()
    {
        if (Core.CheckInventory("Hollowborn Oblivion Blade"))
            return;

        Core.AddDrop("Hollowborn Oblivion Blade", "4th Dimension Gem");

        Core.EnsureAccept(7294);
        Farm.Experience(80);
        Nulgath.FarmVoucher(false);
        ArchFiendEnchantedOrbs();
        Nulgath.FarmUni13();
        Nulgath.DiamondEvilWar(500);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Bot.Wait.ForPickup("Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        Nulgath.ApprovalAndFavor(0, 1000);
        Nulgath.FarmBloodGem(50);
        Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Diabolical Minion's Seed", isTemp: false);
        if (!Core.CheckInventory("4th Dimension Gem"))
        {
            Core.EnsureAccept(5163);
            Core.HuntMonster("blackholesun", "Black Light Elemental", "Black Light", 5);
            Core.EnsureComplete(5163);
            Bot.Wait.ForPickup("4th Dimension Gem");
        }
        Core.HuntMonster("goldenarena", "Blessed Dragon", "Celestial Seal", isTemp: false);
        Core.HuntMonster("shadowattack", "Death", "Death's Power", isTemp: false);
        HB.FreshSouls(350);
        if (!Core.CheckInventory("Unidentified 36"))
            HB.FreshSouls(1);
        Core.EnsureComplete(7294);
        Bot.Wait.ForPickup("Hollowborn Oblivion Blade");
    }

    public void ArchFiendEnchantedOrbs()
    {
        if (Core.CheckInventory("ArchFiend Enchanted Orbs"))
            return;
            
        if (!Core.CheckInventory("Unidentified 36"))
            HB.FreshSouls(1);
        if (!Core.CheckInventory("Unidentified 25"))
        {
            Farm.Gold(15000000);
            Core.BuyItem("tercessuinotlim", 1951, "Unmoulded Fiend Essence");
            Core.BuyItem("tercessuinotlim", 1951, "Unidentified 25");
        }
        Nulgath.FarmUni13();
        Nulgath.DiamondEvilWar(150);
        HB.FreshSouls(100);
        Nulgath.FarmBloodGem(10);
        Nulgath.FarmVoucher(false);
        Core.BuyItem("tercessuinotlim", 1820, "ArchFiend Enchanted Orbs");
        Bot.Wait.ForPickup("ArchFiend Enchanted Orbs");
    }
}
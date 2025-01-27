//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreBots.cs

using System.Collections.Generic;
using System.Linq;
using RBot;
using RBot.Items;
using RBot.Shops;

public class PotionBuyer
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Bot.Options.LagKiller = false;

        INeedYourStrongestPotions();

        Core.SetOptions(false);
    }


    public void INeedYourStrongestPotions(int potionQuant = 50, List<string>? potions = null)
    {
        Farm.AlchemyREP();
        Core.Logger($"{Bot.Player.Username}: Hello potion seller, I’m going into battle and I want your strongest potions");
        if (potions is null)
            potions = new() { "Battle Elixir", "Potent Honor Potion", "Fate Tonic" };
        List<ShopItem> shopItems;
        Core.Logger($"Potion Seller: You can’t handle my potions, they are too strong for you");
        if (ShopCache.Loaded.Any(s => s.ID == 2036))
        {
            shopItems = ShopCache.Loaded.First(s => s.ID == 2036).Items;
        }
        else
        {
            Core.Join("alchemyacademy");
            Bot.Shops.Load(2036);
            shopItems = Bot.Shops.ShopItems;
        }
        Core.Logger($"Potion Seller: My potions are too strong for you, traveller");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion seller I tell you I’m going into battle and I want only your strongest potions");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: You can’t handle my potions, they are too strong for you");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: Potion seller, listen to me, I want only your strongest potions");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My potions would kill you traveller, you cannot handle my potions");
        Bot.Sleep(2500);
        Core.Logger($"{Bot.Player.Username}: POTION SELLER i require your strongest potions");
        Bot.Sleep(2500);
        Core.Logger($"Potion Seller: My strongest potions would kill you traveller, you can’t handle my strongest potions.You better go to a seller that sells weaker potions");
        foreach (string potion in potions)
        {
            int voucherQuant = shopItems.Where(p => p.Name.ToLower() == potion.ToLower()).First().Requirements[0].Quantity * potionQuant;
            while (!Core.CheckInventory(potion, potionQuant))
            {
                if (!Core.CheckInventory("Gold Voucher 500k", voucherQuant))
                {
                    Farm.Gold(500000 * voucherQuant);
                    Core.BuyItem("alchemyacademy", 2036, "Gold Voucher 500k", voucherQuant);
                }
                Core.BuyItem("alchemyacademy", 2036, potion, potionQuant);
            }
        }
    }
}
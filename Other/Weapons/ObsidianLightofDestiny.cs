//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailys.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/ThroneofDarkness/00ThroneofDarkness.cs
//cs_include Scripts/Story/ThroneofDarkness/01Vaden(CastleofBones).cs
//cs_include Scripts/Story/ThroneofDarkness/02aXeven(ParadoxPortal).cs
//cs_include Scripts/Story/ThroneofDarkness/03aZiri(BaconCatFortress).cs
//cs_include Scripts/Story/ThroneofDarkness/04aPax(DeathPit).cs
//cs_include Scripts/Story/ThroneofDarkness/05aSekt(ShiftingPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/05bSekt(FourthDimensionalPyramid).cs
//cs_include Scripts/Story/ThroneofDarkness/06aScarletta(ShatterGlassMaze).cs
//cs_include Scripts/Story/ThroneofDarkness/06bScarletta(TowerofMirrors).cs

using RBot;

public class ObsidianLightofDestiny
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreBLOD BLOD = new CoreBLOD();
    public CompleteThroneOfDarknessSaga CToD = new CompleteThroneOfDarknessSaga();
    public CastleofBones s01 = new CastleofBones();
    public ParadoxPortal s02a = new ParadoxPortal();
    public FlyingBaconCatFortress s03a = new FlyingBaconCatFortress();
    public DeathPitArena s04a = new DeathPitArena();
    public ShiftingPyramid s05a = new ShiftingPyramid();
    public FourthDimensionalPyramid s05b = new FourthDimensionalPyramid();
    public HedgeMaze s06a = new HedgeMaze();
    public TowerofMirrors s06b = new TowerofMirrors();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        //CToD.CompleteToD(); //if you want to do this, just un-// it, its not required the shop can just be loaded.
        Farm.MysteriousDungeonREP();
        Axe();

        Core.SetOptions(false);
    }

    public void Axe()
    {
        if (Core.CheckInventory("Obsidian Light of Destiny"))
            return;

        //The Edge of Destiny
        if (!Core.CheckInventory("Obsidian Light of Destiny"))
        {
            if (!Core.CheckInventory("Blinding Edge of Obsidian"))
            {
                BLOD.FindingFragmentsBow(120);
                BLOD.FindingFragmentsMace(40);
                BLOD.FindingFragments(2174);
                BLOD.fin
                while (!Core.CheckInventory("Spirit Orb", 5000))
                    BLOD.FindingFragments(2179);
                while (!Core.CheckInventory("Loyal Spirit Orb", 750))
                    BLOD.FindingFragments(2179);
                Bot.Wait.ForPickup("Blinding Edge of Obsidian");
            }
        }
    }
}
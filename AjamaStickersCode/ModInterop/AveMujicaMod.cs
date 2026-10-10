using System.Reflection;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.ModInterop;

public static class AveMujicaMod
{
    public static bool IsLoaded { get; } = ModManager.GetLoadedMods()
        .Any(mod => mod.manifest?.id == "AveMujica");
    
    private static Assembly? Assembly { get; } = (ModManager.GetLoadedMods()
        .FirstOrDefault(mod => mod.manifest?.id == "AveMujica")?
        .assemblies.FirstOrDefault());
    
    public const string Oblivionis = "AVEMUJICA-AVE_MUJICA";
    
    private const string Keywords = "AveMujica.AveMujicaCode.Cards.AveMujicaKeywords";
    
    public static CardKeyword Compose { get; } = (CardKeyword)
        (Assembly?.GetType(Keywords)?
             .GetField("Compose")?
             .GetValue(null)
         ?? CardKeyword.None);
    
    public static CardKeyword Perform { get; } = (CardKeyword)
        (Assembly?.GetType(Keywords)?
             .GetField("Perform")?
             .GetValue(null)
         ?? CardKeyword.None);

    public static Type AbstractPerformCard { get; } =
        Assembly?.GetType("AveMujica.AveMujicaCode.Cards.AbstractPerformCard")
        ?? typeof(int);

    public static async Task RandomCompose(Player owner, PlayerChoiceContext choiceContext, bool isUpgraded)
    {
        if (Assembly == null) return;

        var method = Assembly.GetType("AveMujica.AveMujicaCode.Cards.CardMods.ComposeHelper")?
            .GetMethod("RandomCompose");
        if (method == null) return;

        var task = (Task) method.Invoke(null, [owner, choiceContext, isUpgraded])!;

        await task;
    }

    public static void TryEnchantCardWithMasterful(CardModel card, int amount)
    {
        if (Assembly == null) return;

        var method = Assembly.GetType("AveMujica.AveMujicaCode.Enchantments.Masterful")?
            .GetMethod("TryEnchantCardWithMasterful");
        if (method == null) return;

        method.Invoke(null, [card, amount]);
    }
}
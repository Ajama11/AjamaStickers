using System.Reflection;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.ModInterop;

public static class AveMujicaMod
{
    public static bool IsLoaded { get; } = Interop.IsLoaded("AveMujica");
    private static Assembly? Assembly { get; } = Interop.GetAssembly("AveMujica");
    
    public const string Oblivionis = "AVEMUJICA-AVE_MUJICA";
    
    private const string Keywords = "AveMujica.AveMujicaCode.Cards.AveMujicaKeywords";
    
    public static CardKeyword Compose { get; } =
        Interop.GetKeyword(Assembly, Keywords, "Compose");
    
    public static CardKeyword Perform { get; } =
        Interop.GetKeyword(Assembly, Keywords, "Perform");

    public static Type AbstractPerformCard { get; } =
        Interop.GetType(Assembly, "AveMujica.AveMujicaCode.Cards.AbstractPerformCard");

    public static async Task RandomCompose(Player owner, PlayerChoiceContext choiceContext, bool isUpgraded)
    {
        var method = Interop.GetMethod(Assembly,
            "AveMujica.AveMujicaCode.Cards.CardMods.ComposeHelper",
            "RandomCompose");
        if (method == null) return;
        
        await (Task) method.Invoke(null, [owner, choiceContext, isUpgraded])!;
    }

    public static void TryEnchantCardWithMasterful(CardModel card, int amount)
    {
        var method = Interop.GetMethod(Assembly,
            "AveMujica.AveMujicaCode.Enchantments.Masterful",
            "TryEnchantCardWithMasterful");
        if (method == null) return;

        method.Invoke(null, [card, amount]);
    }
}
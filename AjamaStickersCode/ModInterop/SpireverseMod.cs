using System.Reflection;
using AjamaStickers.AjamaStickersCode.Utils;
using BaseLib.Extensions;
using BaseLib.Utils.ModInterop;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.ModInterop;

public static class SpireverseMod
{
    public static bool IsLoaded { get; } = Interop.IsLoaded("IntoTheSpireverse");
    private static Assembly? Assembly { get; } = Interop.GetAssembly("IntoTheSpireverse");
    
    public const string Tectonic = "INTOTHESPIREVERSE-SHADOW_IRONCLAD";
    public const string Acolyte = "INTOTHESPIREVERSE-SHADOW_SILENT";
    public const string Captain = "INTOTHESPIREVERSE-SHADOW_REGENT";
    
    private const string Keywords = "IntoTheSpireverse.IntoTheSpireverseCode.Keywords.IntoTheSpireverseKeywords";
    
    public static CardKeyword Muddle { get; } =
        Interop.GetKeyword(Assembly, Keywords, "Muddle");
    
    public static async Task<IEnumerable<CardModel>> MuddleFromHand(
        PlayerChoiceContext choiceContext,
        Player player,
        AbstractModel source,
        int count = 1)
    {
        var method = Interop.GetMethod(Assembly,
            Keywords, // Eventually MuddleCmd
            "ApplyMuddleFromHandSelection");
        if (method == null) return [];

        return await (Task<IEnumerable<CardModel>>) method.Invoke(null, [choiceContext, player, source, count])!;
    }
}
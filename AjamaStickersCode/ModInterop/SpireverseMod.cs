using System.Reflection;
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
    public static bool IsLoaded { get; } = ModManager.GetLoadedMods()
        .Any(mod => mod.manifest?.id == "IntoTheSpireverse");
    
    private static Assembly? Assembly { get; } = (ModManager.GetLoadedMods()
        .FirstOrDefault(mod => mod.manifest?.id == "IntoTheSpireverse")?
        .assemblies.FirstOrDefault());
    
    public const string Tectonic = "INTOTHESPIREVERSE-SHADOW_IRONCLAD";
    public const string Acolyte = "INTOTHESPIREVERSE-SHADOW_SILENT";
    public const string Captain = "INTOTHESPIREVERSE-SHADOW_REGENT";
    
    private const string Keywords = "IntoTheSpireverse.IntoTheSpireverseCode.Keywords.IntoTheSpireverseKeywords";
    
    public static CardKeyword Muddle { get; } = (CardKeyword)
        (Assembly?.GetType(Keywords)?
             .GetField("Muddle")?
             .GetValue(null)
         ?? CardKeyword.None);
    
    public static async Task<IEnumerable<CardModel>> MuddleFromHand(
        PlayerChoiceContext choiceContext,
        Player player,
        AbstractModel source,
        int count = 1)
    {
        if (Assembly == null) return [];

        var method = Assembly.GetType(Keywords)?
            .GetMethod("ApplyMuddleFromHandSelection");
        
        if (method == null) return [];

        var task = (Task<IEnumerable<CardModel>>) method.Invoke(null, [choiceContext, player, source, count])!;

        return await task;
    }
}
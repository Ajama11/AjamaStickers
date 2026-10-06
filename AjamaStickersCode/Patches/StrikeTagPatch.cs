using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers.Specific.Ironclad;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Patches;

[HarmonyPatch(typeof(CardModel), nameof(CardModel.Tags), MethodType.Getter)]
public static class StrikeTagPatch
{
    [HarmonyPostfix]
    public static IEnumerable<CardTag> Postfix(IEnumerable<CardTag> __result, CardModel __instance)
    {
        return __instance.HasSpecificSticker<StrikeDummy>() ?
            [..__result, CardTag.Strike] :
            __result;
    }
}
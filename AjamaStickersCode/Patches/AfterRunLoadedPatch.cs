using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Extensions;
using HarmonyLib;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace AjamaStickers.AjamaStickersCode.Patches;

[HarmonyPatch(typeof(RunState), nameof(RunState.FromSerializable))]
public static class AfterRunLoadedPatch
{
    [HarmonyPostfix]
    public static RunState Postfix(RunState __result)
    {
        foreach (var player in __result.Players)
        {
            foreach (var card in player.Deck.Cards)
            {
                if (card.TryGetModifier<BaseSticker>(out var sticker))
                {
                    sticker.WhenStickerApplied();
                    sticker.ValuesSetForThisSave = true;
                }
            }
        }
        
        return __result;
    }
}
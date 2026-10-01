using System.Reflection;
using System.Reflection.Emit;
using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Singletons;
using AjamaStickers.AjamaStickersCode.Stickers;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;

namespace AjamaStickers.AjamaStickersCode.Patches;

[HarmonyPatch]
public static class AfterCardTransformedPatch
{
    [HarmonyPatch(typeof(CardCmd), nameof(CardCmd.Transform),
        typeof(IEnumerable<CardTransformation>),
        typeof(Rng),
        typeof(CardPreviewStyle))]
    [HarmonyPrefix]
    private static void TransformPrefix(ref IEnumerable<CardTransformation> transformations, out List<CardTransformation> __state)
    {
        __state = transformations.ToList();
    }
    
    [HarmonyPatch(typeof(CardCmd), nameof(CardCmd.Transform),
        typeof(IEnumerable<CardTransformation>),
        typeof(Rng),
        typeof(CardPreviewStyle))]
    [HarmonyPostfix]
    public static void TransformPostfix(ref Task<IEnumerable<CardPileAddResult>> __result, List<CardTransformation> __state)
    {
        __result = DoTheThings(__result, __state);
    }

    private static async Task<IEnumerable<CardPileAddResult>> DoTheThings(Task<IEnumerable<CardPileAddResult>> originalTask, List<CardTransformation> transformations)
    {
        var originalResults = await originalTask;

        foreach (var transformation in transformations)
        {
            CardModel original = transformation.Original;
            CardModel? replacement = transformation.Replacement;
        
            if (original.TryGetSticker<Coin>(out var coin) &&
                !transformation.IsInCombat)
            {
                await CoinStickerSingleton.GiveGold(coin!, original.Owner);
            }
            else
            {
                if (original.TryGetSticker(out var sticker) &&
                    (replacement?.CanApplySticker(sticker!) ?? false))
                {
                    replacement.ApplySticker(sticker!);
                }
            }
        }

        return originalResults;
    }
}
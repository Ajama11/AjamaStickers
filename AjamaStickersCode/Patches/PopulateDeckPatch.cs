using System.Runtime.CompilerServices;
using AjamaStickers.AjamaStickersCode.Singletons;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Players;

namespace AjamaStickers.AjamaStickersCode.Patches;

[HarmonyPatch(typeof(Player), nameof(Player.PopulateDeck))]
public static class PopulateDeckPatch
{
    [HarmonyPostfix]
    public static void Postfix(Player __instance)
    {
        foreach (var card in __instance.Deck.Cards)
        {
            StickerSingletons.SetRandomPlacement(card);
        }
    }
}
using System.Runtime.CompilerServices;
using AjamaStickers.AjamaStickersCode.Singletons;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Runs;

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

[HarmonyPatch(typeof(AscensionManager), nameof(AscensionManager.ApplyEffectsTo))]
public static class WhyIsntAscendersBanePartOfPopulateDeckPatch
{
    [HarmonyPostfix]
    public static void Postfix()
    {
        foreach (var player in RunManager.Instance.State!.Players)
        {
            foreach (var ascendersBane in player.Deck.Cards.OfType<AscendersBane>())
            {
                // You know what, check if the player has multiple Ascender's Banes because of some Extra Ascensions mod or Start-With-Bing-Bong or something, who give a shit
                // The constant multiplayer desyncs from the funny Stickers Are Placed and Rotated Randomly But Consistently feature is making me lose my mind
                // If you're reading this, uh, get yourself a treat or something
                StickerSingletons.SetRandomPlacement(ascendersBane);
            }
        }
    }
}
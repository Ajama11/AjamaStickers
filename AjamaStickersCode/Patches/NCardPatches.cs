using System.Runtime.CompilerServices;
using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Nodes;
using AjamaStickers.AjamaStickersCode.Singletons;
using AjamaStickers.AjamaStickersCode.Stickers;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Cards;

namespace AjamaStickers.AjamaStickersCode.Patches;

[HarmonyPatch(typeof(NCard))]
public static class NCardPatches
{
    [HarmonyPatch(nameof(NCard._Ready))]
    [HarmonyPostfix]
    public static void ReadyPostfix(NCard __instance)
    {
        var nSticker = PreloadManager.Cache.GetScene("res://AjamaStickers/scenes/sticker.tscn")
            .Instantiate<TextureRect>();
        
        __instance.Body.AddChildSafely(nSticker);
        __instance.Body.MoveChildSafely(nSticker, __instance._frame.GetIndex());
    }
    
    [HarmonyPatch(nameof(NCard.UpdateVisuals))]
    [HarmonyPostfix]
    public static void UpdateVisualsPostfix(NCard __instance)
    {
        TextureRect? nSticker = __instance.Body.GetNodeOrNull<TextureRect>("AjamaSticker");
        
        if (nSticker == null || __instance.Model == null || !__instance.Model.HasAnySticker())
        {
            nSticker?.Visible = false;
            return;
        }

        BaseSticker sticker = __instance.Model.GetCurrentSticker()!;

        int hash = StickerSingletons.RandomPlacement[__instance.Model];
        
        float randomRotation = (hash % 50) - 25f; // -25 to 25
        float randomXPosition = (hash % (120 - 64)) - 120f; // -120 to -64
        float randomYPosition = (hash % (160 - 64)) - 160f; // -160 to -64

        nSticker.Texture = sticker.BigIcon;
        nSticker.RotationDegrees = randomRotation;
        nSticker.Position = new Vector2(randomXPosition, randomYPosition);
        
        nSticker.Visible = true;
    }
}
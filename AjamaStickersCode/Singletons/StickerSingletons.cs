using AjamaStickers.AjamaStickersCode.Rewards;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace AjamaStickers.AjamaStickersCode.Singletons;

public static class StickerSingletons
{
    public static readonly SavedSpireField<Player, int> Chance = new(() => 10, "AjamaSticker-Chance");
    
    public class StickerCombatSingleton() : CustomSingletonModel(HookType.Combat)
    {
        
    }
    
    public class StickerRunSingleton() : CustomSingletonModel(HookType.Run)
    {
        // Temporary forcibly set sticker
        // public override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
        // {
        //     if (card.Pile is not { Type: PileType.Deck }) return Task.CompletedTask;
        //
        //     if (!card.TryGetModifier<TestSticker>(out _)) card.AddModifier<TestSticker>();
        //
        //     return Task.CompletedTask;
        // }

        public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
        {
            if (room is not CombatRoom) return false;
            
            rewards.Add(new StickerReward(player));
            // rewards.Add(new StickerReward(player));
            // rewards.Add(new StickerReward(player));
            // rewards.Add(new StickerReward(player));
            // rewards.Add(new StickerReward(player));
            
            return true;
        }
    }
}
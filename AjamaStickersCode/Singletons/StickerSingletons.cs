using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using AjamaStickers.AjamaStickersCode.Rewards;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace AjamaStickers.AjamaStickersCode.Singletons;

public static class StickerSingletons
{
    public static readonly SavedSpireField<Player, int> Chance =
        new(() => 10, "AjamaSticker-Chance");
    
    public static readonly SavedSpireField<CardModel, int> RandomPlacement =
        (SavedSpireField<CardModel, int>) new SavedSpireField<CardModel, int>
                (() => -1, "AjamaSticker-RandomPlacement")
            .CopyOnClone();
    
    [SuppressMessage("ReSharper", "RedundantAlwaysMatchSubpattern")]
    [SuppressMessage("ReSharper", "ConditionalAccessQualifierIsNonNullableAccordingToAPIContract")]
    public static void SetRandomPlacement(CardModel card)
    {
        if (RandomPlacement[card] != -1) return;
        
        // There are so many non-nullable things here that CAN be null that I forget which ones are truly not null ever
        
        CardModel? cardInDeckIfPossible = null;

        if (card.Owner is { Deck: { Cards: not null } })
        {
            cardInDeckIfPossible = card.Owner.Deck.Cards
                .ToList()
                .Find(c => c == card);
        }
            
        int positionInDeck = cardInDeckIfPossible == null ?
            42069 :
            card.Owner.Deck.Cards!.FirstIndex(c => c == card);

        int netId = card.Owner?.NetId == null ?
            420 :
            (int) card.Owner.NetId;
            
        int seed = card.Owner?.RunState.Rng.Seed == null ?
            69420 :
            (int) card.Owner.RunState.Rng.Seed;
            
        RandomPlacement[card] =
            RuntimeHelpers.GetHashCode(card.Title) +
            RuntimeHelpers.GetHashCode(card.FloorAddedToDeck) +
            RuntimeHelpers.GetHashCode(positionInDeck + netId) + 
            RuntimeHelpers.GetHashCode(seed);
    }
    
    public class StickerCombatSingleton() : CustomSingletonModel(HookType.Combat)
    {
        public override Task AfterCardEnteredCombat(CardModel card)
        {
            SetRandomPlacement(card);
            return Task.CompletedTask;
        }
    }
    
    public class StickerRunSingleton() : CustomSingletonModel(HookType.Run)
    {
        public override Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy)
        {
            if (card.Pile is not { Type: PileType.Deck }) return Task.CompletedTask;

            SetRandomPlacement(card);
            
            return Task.CompletedTask;
        }

        public override bool TryModifyRewards(Player player, List<Reward> rewards, AbstractRoom? room)
        {
            if (room is not CombatRoom) return false;
            
            rewards.Add(new StickerReward(player));
            
            return true;
        }
    }
}
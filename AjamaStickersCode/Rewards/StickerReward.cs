using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace AjamaStickers.AjamaStickersCode.Rewards;

public class StickerReward(Player player) : CustomReward(player)
{
    [CustomEnum]
    public static RewardType StickerType;
    protected override RewardType RewardType => StickerType;

    protected override string IconPath => ImageHelper.GetImagePath("ui/reward_screen/reward_icon_uncommon.png");
    public override LocString Description => new("gameplay_ui", "AJAMASTICKER-STICKER_REWARD");
    
    public struct StickeredCardOption
    {
        public CardModel Card;
        public BaseSticker Sticker;
    }

    public readonly List<StickeredCardOption> StickeredCardOptions = [];
    public readonly Dictionary<CardModel, CardModel> DeckVersion = [];
    
    public override CreateRewardFromSave<CustomReward> DeserializeMethod => CreateFromSerializable;
    
    public static CustomReward CreateFromSerializable(SerializableReward save, Player player1)
    {
        return new StickerReward(player1);
    }
    
    public override SerializableReward ToSerializable()
    {
        return new SerializableReward
        {
            RewardType = StickerType
        };
    }

    public override bool IsPopulated => StickeredCardOptions.Count > 0;
    
    public override void Populate()
    {
        var stickerlessCards = Player.Deck.Cards.Where(c => !c.HasAnySticker()).ToList();

        List<StickeredCardOption> allOptions = [];
        
        foreach (var card in stickerlessCards)
        {
            var newCard = Player.RunState.CloneCard(card);
            var possibleStickers = newCard.GetPossibleStickers();
            
            BaseSticker? sticker = Player.RunState.Rng.CombatCardSelection.NextItem(possibleStickers);
            if (sticker == null) continue;
            
            newCard.ApplySticker(sticker);
            
            DeckVersion[newCard] = card;
            allOptions.Add(new StickeredCardOption
            {
                Card = newCard,
                Sticker = sticker
            });
        }

        StickeredCardOptions.AddRange(allOptions.TakeRandom(3, Player.RunState.Rng.CombatCardSelection));
    }

    protected override async Task<bool> OnSelect()
    {
        if (StickeredCardOptions.Count == 0) return true;
        
        var selectedCard = await CardSelectCmd.FromChooseACardScreen(
            new BlockingPlayerChoiceContext(), 
            StickeredCardOptions.Select(o => o.Card).ToList(),
            Player, true);

        if (selectedCard == null) return false;

        var sticker = StickeredCardOptions.First(o => o.Card == selectedCard).Sticker;
        DeckVersion[selectedCard].ApplySticker(sticker);
        
        return true;
    }

    public override void MarkContentAsSeen()
    {
        
    }
}
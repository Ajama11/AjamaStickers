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

    public readonly List<CardModel> CardOptions = [];
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

    public override bool IsPopulated => CardOptions.Count > 0;
    
    public override void Populate()
    {
        var stickerlessCards = Player.Deck.Cards.Where(c => !c.HasAnySticker()).ToList();
        
        var starry = ModelDb.Sticker<Starry>();
        var eligibleStarryCards = stickerlessCards.Where(c => c.CanApplySticker(starry)).ToList();

        List<CardModel> allOptions = [];
        
        foreach (var card in eligibleStarryCards)
        {
            var newCard = Player.RunState.CloneCard(card);
            
            newCard.AddModifier((CardModifier) starry.MutableClone());
            
            DeckVersion[newCard] = card;
            allOptions.Add(newCard);
        }

        CardOptions.AddRange(allOptions.TakeRandom(3, Player.RunState.Rng.CombatCardSelection));
    }

    protected override async Task<bool> OnSelect()
    {
        var selectedCard = await CardSelectCmd.FromChooseACardScreen(
            new BlockingPlayerChoiceContext(), CardOptions, Player, true);

        if (selectedCard == null) return false;

        var starry = ModelDb.Sticker<Starry>();
        DeckVersion[selectedCard].ApplySticker(starry);
        
        return true;
    }

    public override void MarkContentAsSeen()
    {
        
    }
}
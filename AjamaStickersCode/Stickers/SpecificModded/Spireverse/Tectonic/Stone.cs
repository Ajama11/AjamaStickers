using AjamaStickers.AjamaStickersCode.ModInterop;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificModded.Spireverse.Tectonic;

public class Stone() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        SpireverseMod.IsLoaded &&
        player.Character.Id.Entry == SpireverseMod.Tectonic;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        SpireverseMod.IsLoaded;

    public static readonly ModelId DeepRock = new ("CARD", "INTOTHESPIREVERSE-DEEP_ROCK");

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override List<IHoverTip> ExtraHoverTips
    {
        get
        {
            CardModel? deepRock = ModelDb.GetByIdOrNull<CardModel>(DeepRock);
            return deepRock != null ? [HoverTipFactory.FromCard(deepRock)] : [];
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;
        
        CardModel? deepRock = ModelDb.GetByIdOrNull<CardModel>(DeepRock);
        if (deepRock == null) return;

        CardSelectorPrefs prefs = new (CardSelectorPrefs.TransformSelectionPrompt, 
            0, DynamicVars.Cards.IntValue);

        List<CardModel> cards =
            (await CardSelectCmd.FromHand(choiceContext, Player, prefs,
                _ => true, this))
            .ToList();

        foreach (var card in cards)
        {
            await CardCmd.Transform(card, CombatState!.CreateCard(deepRock, Player));
        }
    }
}
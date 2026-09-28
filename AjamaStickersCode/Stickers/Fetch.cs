using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Fetch() : BaseSticker
{
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        CardSelectorPrefs prefs = new CardSelectorPrefs(SelectionScreenPrompt, 
            0, DynamicVars.Cards.IntValue);

        List<CardModel> cards =
            (await CardSelectCmd.FromCombatPile(choiceContext,
                Owner.Owner.PlayerCombatState!.DiscardPile, Owner.Owner,
                prefs))
            .ToList();

        if (cards.Count > 0) await CardPileCmd.Add(cards, PileType.Hand);
    }
}
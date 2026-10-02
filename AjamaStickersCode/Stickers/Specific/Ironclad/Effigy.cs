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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Ironclad;

public class Effigy() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Ironclad;

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
            (await CardSelectCmd.FromHand(choiceContext, Player, prefs,
                _ => true, this))
            .ToList();

        foreach (var card in cards)
        {
            await CardCmd.Exhaust(choiceContext, card);
        }
    }
}
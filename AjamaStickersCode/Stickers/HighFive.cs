using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class HighFive() : BaseSticker
{
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        List<CardModel> randomCards = (Player.PlayerCombatState!.DrawPile.Cards
                .Where(c => c.HasAnySticker()))
            .TakeRandom(DynamicVars.Cards.IntValue, Rng.CombatCardSelection)
            .ToList();

        foreach (var card in randomCards)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
    }
}
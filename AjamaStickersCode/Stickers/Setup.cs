using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Setup() : BaseSticker
{
    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(CardKeyword.Retain)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        CardModel? highestCostCard = Player.PlayerCombatState!.DrawPile.Cards
            .MaxBy(c => c.EnergyCost.GetWithModifiers(CostModifiers.All));
        
        if (highestCostCard == null) return;

        await CardPileCmd.Add(highestCostCard, PileType.Hand);
        highestCostCard.GiveSingleTurnRetain();
    }
}
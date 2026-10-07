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

public class Mitosis() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        (card.EnergyCost.GetWithModifiers(CostModifiers.All) > 0 ||
         card.EnergyCost.CostsX) &&
        card.Type != CardType.Power &&
        card.CanBeGeneratedInCombat;
    
    public override async Task AfterAutoPostPlayPhaseEntered(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner!.Owner) return;

        if (Owner.Pile?.Type == PileType.Hand)
        {
            CardCmd.PreviewCardPileAdd(
                await CardPileCmd.AddGeneratedCardToCombat(
                    Card.CreateClone(), PileType.Draw,
                    Player, CardPilePosition.Random));
        }
    }
}
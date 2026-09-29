using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Opportunity() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.GetWithModifiers(CostModifiers.All) != 0;

    public override async Task AfterAutoPostPlayPhaseEntered(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner!.Owner) return;

        if (Owner.Pile?.Type == PileType.Hand)
            await CardCmd.AutoPlay(choiceContext, Owner, null);
    }
}
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Conclude() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.GetWithModifiers(CostModifiers.All) != 0 &&
        !card.EnergyCost.CostsX &&
        card is not VoidForm;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new EnergyVar(0)
    ];

    public override void WhenStickerApplied()
    {
        Owner!.EnergyCost.UpgradeBy(-9999);
        Owner.EnergyCost.FinalizeUpgrade();
    }

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return Task.CompletedTask;

        PlayerCmd.EndTurn(Owner.Owner, false);
        
        return Task.CompletedTask;
    }
}
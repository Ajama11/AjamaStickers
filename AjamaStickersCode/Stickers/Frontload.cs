using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Frontload() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        !(
            card.Keywords.Contains(CardKeyword.Innate) && 
            card.EnergyCost.GetWithModifiers(CostModifiers.All) == 0
        ) &&
        !(
            card.Type == CardType.Power &&
            card.EnergyCost.GetWithModifiers(CostModifiers.All) <= 1
        ) &&
        !card.EnergyCost.CostsX;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new EnergyVar(1)
    ];

    public override void WhenStickerApplied()
    {
        Owner!.AddKeyword(CardKeyword.Innate);
        if (Owner.Type != CardType.Power) Owner.AddKeyword(CardKeyword.Exhaust);
        
        Owner.EnergyCost.UpgradeBy(-DynamicVars.Energy.IntValue);
        Owner.EnergyCost.FinalizeUpgrade();
    }
}
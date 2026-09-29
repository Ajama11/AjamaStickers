using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Omamori() : BaseSticker
{
    public override bool RequiresCardToBePlayable => false;

    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Rarity == CardRarity.Curse &&
        card.Keywords.Contains(CardKeyword.Unplayable) && 
        !card.Keywords.Contains(CardKeyword.Ethereal);

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new EnergyVar(2)
    ];

    public override void WhenStickerApplied()
    {
        Owner!.EnergyCost.SetCustomBaseCost(DynamicVars.Energy.IntValue);
        Owner.RemoveKeyword(CardKeyword.Unplayable);
        Owner.AddKeyword(CardKeyword.Exhaust);
    }
}
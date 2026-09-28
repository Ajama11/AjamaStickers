using System.Diagnostics.CodeAnalysis;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Chemical() : BaseSticker
{
    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.CostsX ||
        card.HasStarCostX ||
        card is ICustomResourceCost { CostsX: true };

    private const string Increase = "Increase";
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new IntVar(Increase, 1)
    ];

    public override int ModifyXValue(CardModel card, int originalValue)
    {
        return card == Owner ?
            originalValue + DynamicVars[Increase].IntValue :
            originalValue;
    }
}
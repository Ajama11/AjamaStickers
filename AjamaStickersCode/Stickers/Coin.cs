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

public class Coin() : BaseSticker
{
    public override bool RequiresCardToBePlayable => false;
    public override bool HideNewLineOverride => InCombat;

    public override bool ExtraConditionalToApply(CardModel card) =>
        !card.Keywords.Contains(CardKeyword.Eternal);

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new GoldVar(200)
    ];
}
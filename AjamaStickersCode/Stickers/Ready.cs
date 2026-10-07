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

public class Ready() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        !card.Keywords.Contains(CardKeyword.Retain);
    
    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(CardKeyword.Retain)
    ];

    public override void WhenStickerApplied()
    {
        Card.AddKeyword(CardKeyword.Retain);
    }
}
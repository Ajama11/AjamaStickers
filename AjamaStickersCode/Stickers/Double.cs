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
using MegaCrit.Sts2.Core.Models.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Double() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type is (CardType.Attack or CardType.Skill) &&
        !card.Keywords.Contains(CardKeyword.Exhaust) &&
        card is not (BattleTrance or PanicButton);

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(StaticHoverTip.ReplayStatic)
    ];

    public override void WhenStickerApplied()
    {
        Card.BaseReplayCount += 1;
        Card.AddKeyword(CardKeyword.Exhaust);
    }
}
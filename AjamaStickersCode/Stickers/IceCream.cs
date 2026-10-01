using AjamaStickers.AjamaStickersCode.Powers;
using AjamaStickers.AjamaStickersCode.Utils;
using BaseLib.Extensions;
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

public class IceCream() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type == CardType.Power;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<IceCreamStickerPower>(2)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.ApplySelf<IceCreamStickerPower>(choiceContext, this);
    }
}
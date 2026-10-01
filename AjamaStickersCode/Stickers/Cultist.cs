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
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Cultist() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type == CardType.Power &&
        card.EnergyCost.GetWithModifiers(CostModifiers.All) >= 2;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<RitualPower>(1)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<RitualPower>(),
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.ApplySelf<RitualPower>(choiceContext, this);
    }
}
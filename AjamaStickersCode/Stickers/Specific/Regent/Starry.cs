using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Starry : BaseSticker
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new StarsVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await PlayerCmd.GainStars(DynamicVars.Stars.BaseValue, cardPlay.Player);
    }
}
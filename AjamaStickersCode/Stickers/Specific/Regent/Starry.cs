using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Regent;

public class Starry : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character.ShouldAlwaysShowStarCounter;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new StarsVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await PlayerCmd.GainStars(DynamicVars.Stars.BaseValue, cardPlay.Player);
    }
}
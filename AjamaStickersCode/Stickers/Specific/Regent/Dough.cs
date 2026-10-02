using AjamaStickers.AjamaStickersCode.Powers;
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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Regent;

public class Dough() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Regent;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<DoughStickerPower>(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.ApplySelf<DoughStickerPower>(choiceContext, this);
    }
}
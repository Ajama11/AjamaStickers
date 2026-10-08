using AjamaStickers.AjamaStickersCode.Powers;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Regent;

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
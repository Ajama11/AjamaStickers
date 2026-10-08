using AjamaStickers.AjamaStickersCode.Powers;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Defect;

public class Focused() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character.BaseOrbSlotCount > 0;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<FocusedStickerPower>(2)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<FocusPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.ApplySelf<FocusedStickerPower>(choiceContext, this);
    }
}
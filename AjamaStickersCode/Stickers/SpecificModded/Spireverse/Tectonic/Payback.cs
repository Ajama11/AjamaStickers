using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.ModInterop;
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

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificModded.Spireverse.Tectonic;

public class Payback() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        SpireverseMod.IsLoaded &&
        player.Character.Id.Entry == SpireverseMod.Tectonic;

    public override bool ExtraConditionalToApply(CardModel card) =>
        SpireverseMod.IsLoaded;

    public static readonly ModelId Retaliation = new ("POWER", "INTOTHESPIREVERSE-RETALIATION_POWER");

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new (Retaliation.Entry, 5)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
        HoverTipFactory.FromPowerOutside(Retaliation);

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.ApplySelf(choiceContext, Retaliation, this);
    }
}
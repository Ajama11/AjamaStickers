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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Ironclad;

public class StrikeDummy() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Ironclad;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Rarity != CardRarity.Basic &&
        !card.Tags.Contains(CardTag.Strike) &&
        card.EnergyCost.GetWithModifiers(CostModifiers.All) > 0;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new EnergyVar(1)
    ];

    public override void WhenStickerApplied()
    {
        Card.EnergyCost.UpgradeBy(-DynamicVars.Energy.IntValue);
        Card.EnergyCost.FinalizeUpgrade();
    }
}
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Defect;

public class Talons() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Defect;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.GetWithModifiers(CostModifiers.All) == 0 &&
        !card.EnergyCost.CostsX;
    
    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(StaticHoverTip.ReplayStatic)
    ];

    public override void WhenStickerApplied()
    {
        Card.BaseReplayCount += 1;
    }
}
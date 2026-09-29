using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Silent;

public class Cunning : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        DraftRequirement(player, c => c.Keywords.Contains(CardKeyword.Sly));

    public override bool ExtraConditionalToApply(CardModel card) =>
        !card.Keywords.Contains(CardKeyword.Sly);

    public override void WhenStickerApplied()
    {
        Owner!.AddKeyword(CardKeyword.Sly);
    }
}
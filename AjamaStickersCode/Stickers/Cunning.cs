using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Cunning : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card)
    {
        return !card.Keywords.Contains(CardKeyword.Sly);
    }

    public override void WhenStickerApplied()
    {
        Owner!.AddKeyword(CardKeyword.Sly);
    }
}
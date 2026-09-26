using MegaCrit.Sts2.Core.Entities.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Cunning : BaseSticker
{
    public override void WhenStickerApplied()
    {
        Owner!.AddKeyword(CardKeyword.Sly);
    }
}
using System.Diagnostics.CodeAnalysis;
using AjamaStickers.AjamaStickersCode.Stickers;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Extensions;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public static class CardModelExtensions
{
    extension(CardModel card)
    {
        public bool HasAnySticker()
        {
            return BaseSticker.HasAnySticker(card);
        }

        public bool CanApplySticker(BaseSticker sticker)
        {
            return sticker.CanApplySticker(card);
        }
    }
}
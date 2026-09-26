using System.Diagnostics.CodeAnalysis;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
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

        public void ApplySticker<T>() where T : BaseSticker
        {
            card.AddModifier<T>();
        }

        public void ApplySticker(BaseSticker canonicalSticker)
        {
            card.AddModifier((CardModifier) canonicalSticker.MutableClone());
        }

        public List<BaseSticker> GetPossibleStickers()
        {
            return ModelDb.AllStickers
                .Where(s =>
                    s.CanSpawn(card.Owner) &&
                    s.CanApplySticker(card))
                .ToList();
        }
    }
}
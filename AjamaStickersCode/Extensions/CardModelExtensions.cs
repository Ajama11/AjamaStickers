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

        public bool HasSpecificSticker<T>() where T : BaseSticker
        {
            return card.TryGetModifier<T>(out _);
        }
        
        public bool TryGetSticker<T>(out T? sticker) where T : BaseSticker
        {
            return card.TryGetModifier(out sticker);
        }

        public bool TryGetSticker(out BaseSticker? sticker)
        {
            return card.TryGetModifier(out sticker);
        }

        public bool CanApplySticker(BaseSticker sticker)
        {
            return sticker.CanApplySticker(card);
        }

        public T ApplySticker<T>() where T : BaseSticker
        {
            return (T) card.ApplySticker(ModelDb.Sticker<T>());
        }
        
        public BaseSticker ApplySticker(BaseSticker canonicalSticker)
        {
            var mutable = (BaseSticker) canonicalSticker.MutableClone();
            card.AddModifier(mutable);
            return mutable;
        }

        public List<BaseSticker> GetPossibleStickers()
        {
            return ModelDb.AllStickers
                .Where(s =>
                    s.CanSpawn(card.Owner) &&
                    s.CanApplySticker(card))
                .ToList();
        }

        public BaseSticker? GetCurrentSticker()
        {
            return card.GetModifier<BaseSticker>();
        }
    }
}
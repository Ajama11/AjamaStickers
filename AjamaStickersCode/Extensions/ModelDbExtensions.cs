using AjamaStickers.AjamaStickersCode.Stickers;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Extensions;

public static class ModelDbExtensions
{
    private static IEnumerable<BaseSticker>? _allStickers;
    
    extension(ModelDb)
    {
        public static IEnumerable<BaseSticker> AllStickers =>
            _allStickers ??= ModelDb.AllAbstractModelSubtypes
                .Where(t => t.IsSubclassOf(typeof(BaseSticker)))
                .Select(t => (BaseSticker) ModelDb.Get(t));
    }
}
using AjamaStickers.AjamaStickersCode.Extensions;
using BaseLib.Abstracts;
using BaseLib.Extensions;

namespace AjamaStickers.AjamaStickersCode.Powers;

public abstract class AjamaStickerPower : CustomPowerModel
{
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}
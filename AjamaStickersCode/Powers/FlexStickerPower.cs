using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Powers;

public class FlexStickerPower : CustomTemporaryPowerModelWrapper<Flex, StrengthPower>
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override LocString Title => new ("powers", $"{Id.Entry}.title");
    
    public override string CustomPackedIconPath => "res://images/atlases/power_atlas.sprites/flex_potion_power.tres";
    public override string CustomBigIconPath => ImageHelper.GetImagePath("powers/flex_potion_power.png");
}
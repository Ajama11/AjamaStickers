using System.Diagnostics.CodeAnalysis;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Extensions;

[SuppressMessage("Performance", "CA1822:Mark members as static")]
public static class HoverTipFactoryExtensions
{
    extension(HoverTipFactory)
    {
        public static List<IHoverTip> FromCardOutside(ModelId modelId, Func<CardModel, CardModel>? modifyCard = null)
        {
            CardModel? card = ModelDb.GetByIdOrNull<CardModel>(modelId);
            if (card == null) return [];

            // ReSharper disable once InvertIf
            if (modifyCard != null)
            {
                card = card.ToMutable();
                card = modifyCard(card);
            }
        
            return [HoverTipFactory.FromCard(card)];
        }

        public static List<IHoverTip> FromPowerOutside(ModelId modelId, int? amount = null)
        {
            PowerModel? power = ModelDb.GetByIdOrNull<PowerModel>(modelId);
            return power != null ? [HoverTipFactory.FromPower(power, amount)] : [];
        }
        
        public static List<IHoverTip> FromPowerWithPowerHoverTipsOutside(ModelId modelId, int? amount = null)
        {
            PowerModel? power = ModelDb.GetByIdOrNull<PowerModel>(modelId);
            return power != null ? [HoverTipFactory.FromPower(power, amount), ..power.HoverTips] : [];
        }
    }
}
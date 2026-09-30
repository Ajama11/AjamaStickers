using AjamaStickers.AjamaStickersCode.Singletons;
using BaseLib.Cards.Variables;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Merchant() : BaseSticker
{
    public override bool RequiresCardToBePlayable => false;
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new DisplayVar<Merchant>("TotalDiscount", merchant =>
        {
            int bonusIfStickerIsBeingOfferedInReward =
                merchant.Owner?.Pile == null ? 1 : 0;

            return ((MerchantStickerSingleton.GetTotalMerchantStickers(merchant.Owner!.Owner) + bonusIfStickerIsBeingOfferedInReward) * 5)
                .ToString();
        })
    ];

    public override void AddExtraArgsToLoc(LocString loc)
    {
        loc.Add("IsReward", Owner?.Pile == null);
    }
}
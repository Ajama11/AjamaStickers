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
using MegaCrit.Sts2.Core.Runs;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Merchant() : BaseSticker
{
    public override bool RequiresCardToBePlayable => false;
    public override bool HideNewLineOverride => InCombat;

    public override bool CanSpawn(Player player) =>
        MerchantStickerSingleton.GetTotalMerchantStickers() < 10 &&
        player.RunState.GameMode != GameMode.Blitz;

    public bool IsReward { get; set; }

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new DisplayVar<Merchant>("TotalDiscount", merchant =>
        {
            int bonusIfStickerIsBeingOfferedInReward =
                merchant.IsReward ? 1 : 0;

            int numberOfStickers =
                MerchantStickerSingleton.GetTotalMerchantStickers()
                + bonusIfStickerIsBeingOfferedInReward;

            return Math.Min((numberOfStickers * 5), 50).ToString();
        })
    ];

    public override void AddExtraArgsToLoc(LocString loc)
    {
        loc.Add("IsReward", IsReward);
    }
}
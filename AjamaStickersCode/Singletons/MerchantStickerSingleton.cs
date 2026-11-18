using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace AjamaStickers.AjamaStickersCode.Singletons;

public class MerchantStickerSingleton() : CustomSingletonModel(HookType.Run)
{
    public static int GetTotalMerchantStickers()
    {
        return RunManager.Instance.State!.Players
            .Sum(p => p.Deck.Cards.Count(c => 
                c.HasSpecificSticker<Merchant>()));
    }
    
    public override decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal cost)
    {
        decimal discount = Math.Min(GetTotalMerchantStickers() * 0.05m, 0.5m);
        
        return cost * (1 - discount);
    }
}
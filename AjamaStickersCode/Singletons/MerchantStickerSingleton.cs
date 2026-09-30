using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Singletons;

public class MerchantStickerSingleton() : CustomSingletonModel(HookType.Run)
{
    public static int GetTotalMerchantStickers(Player anyPlayer)
    {
        return anyPlayer.RunState.Players
            .Sum(p => p.Deck.Cards.Count(c => 
                c.HasSpecificSticker<Merchant>()));
    }
    
    public override decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal cost)
    {
        decimal discount = Math.Min(GetTotalMerchantStickers(player) * 0.05m, 0.5m);
        
        return cost * (1 - discount);
    }
}
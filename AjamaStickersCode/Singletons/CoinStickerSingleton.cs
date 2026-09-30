using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Singletons;

public class CoinStickerSingleton() : CustomSingletonModel(HookType.Run)
{
    public override async Task BeforeCardRemoved(CardModel card)
    {
        if (!card.TryGetSticker<Coin>(out var coin)) return;

        await GiveGold(coin!, card.Owner);
    }

    public static async Task GiveGold(Coin coin, Player player)
    {
        await PlayerCmd.GainGold(coin.DynamicVars.Gold.BaseValue, player);
    }
}
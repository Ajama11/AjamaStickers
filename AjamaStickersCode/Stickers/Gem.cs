using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.ModInterop;
using AjamaStickers.AjamaStickersCode.Utils;
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

public class Gem() : BaseSticker
{
    private const string Replay = "Replay";
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new IntVar(Replay, 1)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(StaticHoverTip.ReplayStatic)
    ];

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return Task.CompletedTask;

        List<CardModel> eligibleCards = Player.PlayerCombatState!.DrawPile.Cards
            .Where(c =>
                !c.Keywords.Contains(CardKeyword.Unplayable) &&
                c.GetEnchantedReplayCount() < 1 &&
                c.Type is CardType.Attack or CardType.Skill or CardType.Power)
            .ToList();

        if (eligibleCards.Count == 0)
        {
            eligibleCards = Player.PlayerCombatState!.DrawPile.Cards
                .Where(c =>
                    !c.Keywords.Contains(CardKeyword.Unplayable) &&
                    c.GetEnchantedReplayCount() < 1 &&
                    c.Type is not (CardType.Curse or CardType.Quest))
                .ToList();
        }

        var card = Rng.CombatCardSelection.NextItem(eligibleCards);
        if (card == null) return Task.CompletedTask;

        card.BaseReplayCount += DynamicVars[Replay].IntValue;
        CardCmd.Preview(card);
        
        return Task.CompletedTask;
    }
}
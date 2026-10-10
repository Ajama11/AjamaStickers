using AjamaStickers.AjamaStickersCode.ModInterop;
using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificModded.AveMujica;

public class Umiri() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        AveMujicaMod.IsLoaded &&
        player.Character.Id.Entry == AveMujicaMod.Oblivionis;

    public override bool ExtraConditionalToApply(CardModel card) =>
        AveMujicaMod.IsLoaded;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(AveMujicaMod.Perform)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        List<CardModel> randomPerformCards = Player.PlayerCombatState!.DrawPile.Cards
            .Where(c =>
                c.GetType().IsAssignableTo(AveMujicaMod.AbstractPerformCard))
            .TakeRandom(DynamicVars.Cards.IntValue, Rng.CombatCardSelection)
            .ToList();
        if (randomPerformCards.Count == 0) return;

        await CardPileCmd.Add(randomPerformCards, PileType.Hand);
    }
}
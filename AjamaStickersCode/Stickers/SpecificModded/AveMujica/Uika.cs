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

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificModded.AveMujica;

public class Uika() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        AveMujicaMod.IsLoaded &&
        player.Character.Id.Entry == AveMujicaMod.Oblivionis;

    public override bool ExtraConditionalToApply(CardModel card) =>
        AveMujicaMod.IsLoaded;

    public override void AddExtraArgsToLoc(LocString loc)
    {
        loc.Add("IsUpgraded", Card.IsUpgraded);
    }

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(AveMujicaMod.Compose)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await AveMujicaMod.RandomCompose(Player, choiceContext, Card.IsUpgraded);
    }
}
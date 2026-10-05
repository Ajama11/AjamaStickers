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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Silent;

public class Haste() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        DraftRequirement(player, c => c.Keywords.Contains(CardKeyword.Sly));

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(8)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        IEnumerable<CardModel> cards = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Player);

        await Cmd.CustomScaledWait(0.4f, 0.8f);
        
        await CardCmd.Discard(choiceContext, cards);
    }
}
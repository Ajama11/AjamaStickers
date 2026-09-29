using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Flow() : BaseSticker
{
    public override bool RequiresCardToBePlayable => false;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(1)
    ];

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card != Owner) return;

        await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner.Owner);
    }
}
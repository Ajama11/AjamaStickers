using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Brace() : BaseSticker
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(3, BlockProps.card)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;
        
        await CreatureCmd.GainBlock(Owner!.Owner.Creature, DynamicVars.Block, cardPlay);
    }
}
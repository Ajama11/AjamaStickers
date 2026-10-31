using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Energize : BaseSticker
{
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new EnergyVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;
        
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, 
            cardPlay.Player.Creature, DynamicVars.Energy.BaseValue,
            cardPlay.Player.Creature, Card);
    }
}
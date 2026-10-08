using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Defect;

public class Capacitor() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character.BaseOrbSlotCount > 0;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        IsNotSelfDupingCard(card);

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new RepeatVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;
        if (!Active) return;

        await OrbCmd.AddSlots(Player, DynamicVars.Repeat.IntValue);
        
        Active = false;
    }
}
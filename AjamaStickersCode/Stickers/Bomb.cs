using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Bomb() : BaseSticker
{
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.GetWithModifiers(CostModifiers.All) >= 2;

    private const string Turns = "Turns";
    private const string BombDamage = "BombDamage";
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new (Turns, 3),
        new (BombDamage, 25)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        (await PowerCmd.Apply<TheBombPower>(choiceContext,
            Owner.Owner.Creature, DynamicVars[Turns].BaseValue,
            Owner.Owner.Creature, Owner))
            ?.SetDamage(DynamicVars[BombDamage].BaseValue);
    }
}
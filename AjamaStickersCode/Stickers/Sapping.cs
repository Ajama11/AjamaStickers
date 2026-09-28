using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Sapping() : BaseSticker
{
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<StrengthPower>(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;
        if (!Active) return;

        await PowerCmd.Apply<StrengthPower>(choiceContext,
            Owner.CombatState!.HittableEnemies, -DynamicVars.Strength.BaseValue,
            Owner.Owner.Creature, cardPlay.Card);

        Active = false;
    }
}
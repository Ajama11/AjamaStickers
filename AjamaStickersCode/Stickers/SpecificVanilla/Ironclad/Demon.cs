using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Ironclad;

public class Demon() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Ironclad;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new HpLossVar(2),
        new PowerVar<StrengthPower>(1)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<StrengthPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await CreatureCmd.Damage(choiceContext,
            Player.Creature, DynamicVars.HpLoss.BaseValue, DamageProps.cardHpLoss,
            Card, cardPlay);

        await MyActions.ApplySelf<StrengthPower>(choiceContext, this);
    }
}
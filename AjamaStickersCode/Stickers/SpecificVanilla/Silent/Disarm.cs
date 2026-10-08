using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Silent;

public class Disarm() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Silent;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type == CardType.Power;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<WeakPower>(2)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<WeakPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.Apply<WeakPower>(choiceContext, this, CombatState!.HittableEnemies);
    }
}
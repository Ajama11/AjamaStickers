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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Necrobinder;

public class Skull() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        DraftRequirement(player, c => c.DynamicVars.ContainsKey("DoomPower"));
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new PowerVar<DoomPower>(3)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<DoomPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await PowerCmd.Apply<DoomPower>(choiceContext,
            Owner.CombatState!.HittableEnemies, DynamicVars.Doom.BaseValue,
            Owner.Owner.Creature, Owner);
    }
}
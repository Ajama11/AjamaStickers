using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Silent;

public class Virus() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        DraftRequirement(player, c => c.DynamicVars.ContainsKey("PoisonPower"));

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<PoisonPower>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        foreach (var enemy in CombatState!.HittableEnemies)
        {
            var poison = enemy.GetPower<PoisonPower>();
            if (poison != null) await poison.Trigger();
        }
    }
}
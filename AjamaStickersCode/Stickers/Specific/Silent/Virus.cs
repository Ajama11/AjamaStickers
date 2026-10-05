using AjamaStickers.AjamaStickersCode.Utils;
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

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Silent;

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
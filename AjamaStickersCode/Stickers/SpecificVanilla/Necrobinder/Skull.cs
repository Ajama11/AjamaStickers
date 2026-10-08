using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Necrobinder;

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

        await MyActions.Apply<DoomPower>(choiceContext, this, CombatState!.HittableEnemies);
    }
}
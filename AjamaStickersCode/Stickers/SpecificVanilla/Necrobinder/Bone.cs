using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Necrobinder;

public class Bone() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        DraftRequirement(player, c => c.DynamicVars.ContainsKey(SummonVar.defaultName));
    
    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new SummonVar(2)
    ];
    
    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.Static(StaticHoverTip.SummonDynamic, DynamicVars.Summon)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await OstyCmd.Summon(choiceContext, Owner.Owner, DynamicVars.Summon.BaseValue, this);
    }
}
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Regent;

public class Blade() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Regent || 
        player.Deck.Cards.Any(c => c.DynamicVars.ContainsKey(ForgeVar.defaultName));

    public override bool ExtraConditionalToApply(CardModel card) =>
        card is not SummonForth;

    public override List<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<SovereignBlade>()
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        List<SovereignBlade> blades = Player.PlayerCombatState!.AllCards
            .OfType<SovereignBlade>()
            .Where(b => b.Pile?.Type != PileType.Hand)
            .ToList();

        await CardPileCmd.Add(blades, PileType.Hand);
    }
}
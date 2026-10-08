using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Necrobinder;

public class Ghost() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Necrobinder;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Keywords.Contains(CardKeyword.Ethereal);
    
    public override async Task AfterAutoPostPlayPhaseEntered(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Player) return;

        if (Card.Pile?.Type == PileType.Hand)
            await CardCmd.Discard(choiceContext, Card);
    }
}
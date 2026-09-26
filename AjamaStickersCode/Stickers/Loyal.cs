using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Loyal : BaseSticker
{
    public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
    {
        if (player != Owner!.Owner) return;
        if (!CombatManager.Instance.History.CardPlaysFinished
                .Any(e =>
                    e.HappenedLastPlayerTurn(player) &&
                    e.CardPlay.Card == Owner))
        {
            return;
        }

        if (Owner.Pile?.Type != PileType.Hand)
        {
            await CardPileCmd.Add(Owner, PileType.Hand);
        }
    }
}
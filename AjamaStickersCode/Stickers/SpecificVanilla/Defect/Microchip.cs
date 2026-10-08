using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Defect;

public class Microchip() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character.BaseOrbSlotCount > 0;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        foreach (var orb in Player.PlayerCombatState!.OrbQueue.Orbs.ToList())
        {
            await OrbCmd.Passive(choiceContext, orb, null);
        }
    }
}
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace AjamaStickers.AjamaStickersCode.Powers;

public class IceCreamStickerPower : AjamaStickerPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool ShouldPlayerResetEnergy(Player player)
    {
        if (player.Creature != Owner) return true;
        if (Owner.Player!.PlayerCombatState!.TurnNumber == 1) return true;

        return false;
    }

    public override async Task AfterEnergyReset(Player player)
    {
        if (player.Creature != Owner) return;
        
        await PowerCmd.Decrement(this);
    }
}
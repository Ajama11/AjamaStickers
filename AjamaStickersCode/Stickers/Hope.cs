using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Hope() : BaseSticker
{
    public override bool CanSpawn(Player player) => IsMultiplayer;

    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Tags.Contains(CardTag.Defend) &&
        card.DynamicVars.TryGetValue("Block", out _ );

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource)
    {
        if (cardSource != Card) return;
        
        foreach (var ally in Allies)
        {
            await CreatureCmd.GainBlock(ally, amount, BlockProps.cardUnpowered, null);
        }
    }
}
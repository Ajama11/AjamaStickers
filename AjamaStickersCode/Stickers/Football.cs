using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Football() : BaseSticker
{
    public override bool CanSpawn(Player player) => IsMultiplayer;

    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type != CardType.Power &&
        !card.Keywords.Contains(CardKeyword.Exhaust) &&
        card is not (TheBall or Outrage);

    public override CardLocation ModifyCardPlayResultLocation(CardModel card, bool isAutoPlay, ResourceInfo resources, CardLocation cardLocation)
    {
        if (card != Owner) return cardLocation;
        if (Owner.CombatState == null) return cardLocation;
        if (Allies.Count == 0) return cardLocation;

        CardLocation newLocation = cardLocation;
        newLocation.player = Rng.CombatTargets.NextItem(Allies)!.Player!;

        if (newLocation.pileType == PileType.Discard)
        {
            newLocation.pileType = PileType.Draw;
            newLocation.position = CardPilePosition.Random;
        }

        return newLocation;
    }
}
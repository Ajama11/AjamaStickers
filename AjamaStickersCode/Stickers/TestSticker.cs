using AjamaStickers.AjamaStickersCode.Singletons;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class TestSticker : BaseSticker
{
    public override bool CanSpawn(Player player) => false;

    public override void WhenStickerApplied()
    {
        if (Owner == null) return;
        
        Owner.AddKeyword(CardKeyword.Innate);
        Owner.AddKeyword(CardKeyword.Retain);
        if (Owner.Type != CardType.Power) Owner.AddKeyword(CardKeyword.Exhaust);
        
        Owner.EnergyCost.AddThisCombat(-1);
        Owner.BaseReplayCount += 1;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await PlayerCmd.GainStars(1, cardPlay.Player);
    }

    public override void ModifyDescriptionPost(Creature? target, ref string description)
    {
        description = description + "\n[aqua]" + LocString.GetIfExists("static_hover_tips", "COOK.title")?.GetFormattedText() + ".[/aqua]";
    }
}
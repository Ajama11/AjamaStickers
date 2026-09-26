using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public class Starry : BaseSticker
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new StarsVar(1)
    ];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await PlayerCmd.GainStars(DynamicVars.Stars.BaseValue, cardPlay.Player);
    }

    public override void ModifyDescriptionPost(Creature? target, ref string description)
    {
        description = description + "\n[aqua]" + LocString.GetIfExists("static_hover_tips", "CHANNELING.title")?.GetFormattedText() + ".[/aqua]";
    }
}
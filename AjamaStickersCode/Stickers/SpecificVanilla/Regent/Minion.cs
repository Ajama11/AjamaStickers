using AjamaStickers.AjamaStickersCode.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificVanilla.Regent;

public class Minion() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Regent;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.EnergyCost.GetWithModifiers(CostModifiers.All) >= 2;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(2)
    ];

    public override List<IHoverTip> ExtraHoverTips
    {
        get
        {
            CardModel card = ModelDb.Card<MinionStrike>().ToMutable();
            
            card.AddKeyword(CardKeyword.Retain);
            
            return [HoverTipFactory.FromCard(card)];
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.CreateCards(ModelDb.Card<MinionStrike>(),
            DynamicVars.Cards.IntValue, this,
            modifyCardsBeforePreview: list =>
            {
                foreach (var card in list)
                {
                    card.AddKeyword(CardKeyword.Retain);
                }
                return list;
            });
    }
}
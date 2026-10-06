using AjamaStickers.AjamaStickersCode.Utils;
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
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace AjamaStickers.AjamaStickersCode.Stickers.Specific.Necrobinder;

public class Heartbeat() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        player.Character is MegaCrit.Sts2.Core.Models.Characters.Necrobinder;
    
    public override bool ExtraConditionalToApply(CardModel card) =>
        card.Type == CardType.Power;

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new CardsVar(2)
    ];

    public override List<IHoverTip> ExtraHoverTips
    {
        get
        {
            CardModel card = ModelDb.Card<Soul>().ToMutable();

            CardCmd.Enchant<Sown>(card, 1);
            
            return [HoverTipFactory.FromCard(card)];
        }
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return;

        await MyActions.CreateCards(ModelDb.Card<Soul>(),
            DynamicVars.Cards.IntValue, this,
            PileType.Draw, CardPilePosition.Random,
            modifyCardsBeforePreview: list =>
            {
                foreach (var card in list)
                {
                    CardCmd.Enchant<Sown>(card, 1);
                }
                return list;
            });
    }
}
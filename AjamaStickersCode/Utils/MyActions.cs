using AjamaStickers.AjamaStickersCode.Stickers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace AjamaStickers.AjamaStickersCode.Utils;

public class MyActions
{
    public static async Task<IEnumerable<CardModel>> CreateCards(CardModel canonicalCard, int amount,
        BaseSticker sticker, PileType pile = PileType.Hand, CardPilePosition position = CardPilePosition.Bottom, bool preview = true, float previewTime = 1.2f, Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        return await CreateCards(canonicalCard, amount, sticker.Owner!.Owner, sticker.Owner.CombatState!, pile, position, preview, previewTime, modifyCardsBeforePreview);
    }
    
    public static async Task<IEnumerable<CardModel>> CreateCards(CardModel canonicalCard, int amount, Player owner, ICombatState combatState, PileType pile = PileType.Hand, CardPilePosition position = CardPilePosition.Bottom, bool preview = true, float previewTime = 1.2f, Func<List<CardModel>, List<CardModel>>? modifyCardsBeforePreview = null)
    {
        if (amount == 0 || CombatManager.Instance.IsOverOrEnding)
        {
            return [];
        }

        List<CardModel> cards = [];

        for (int i = 0; i < amount; i++)
        {
            cards.Add(combatState.CreateCard(canonicalCard, owner));
        }

        if (modifyCardsBeforePreview != null)
        {
            cards = modifyCardsBeforePreview(cards);
        }

        IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(cards, pile, owner, position);

        if (pile != PileType.Hand && preview)
            CardCmd.PreviewCardPileAdd(results, previewTime, amount > 5 ?
                CardPreviewStyle.MessyLayout :
                CardPreviewStyle.HorizontalLayout);

        return cards;
    }
}
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public abstract class BaseSticker : CardModifier, ICustomModel
{
    public virtual bool RequiresCardToBePlayable => true;
    
    public bool ValuesSetForThisSave { get; set; } = false;
    
    public static bool HasAnySticker(CardModel card) =>
        Modifiers(card).Any(m => m is BaseSticker);

    public bool CanApplySticker(CardModel card)
    {
        bool possiblyPlayable = true;
        if (RequiresCardToBePlayable) possiblyPlayable = !card.Keywords.Contains(CardKeyword.Unplayable);
        return possiblyPlayable && !HasAnySticker(card) && ExtraConditionalToApply(card);
    }

    public override void OnInitialApplication()
    {
        if (ValuesSetForThisSave) return;
        
        WhenStickerApplied();

        ValuesSetForThisSave = true;
    }

    public virtual bool ExtraConditionalToApply(CardModel card)
    {
        return true;
    }

    public virtual void WhenStickerApplied()
    {
        
    }
}
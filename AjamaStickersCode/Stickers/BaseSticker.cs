using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public abstract class BaseSticker : CardModifier, ICustomModel
{
    public virtual bool RequiresCardToBePlayable => true;
    public virtual List<IHoverTip> ExtraHoverTips => [];
    
    public virtual bool ExtraConditionalToApply(CardModel card) => true;
    public virtual bool CanSpawn(Player player) => true;
    public virtual void WhenStickerApplied() { }
    
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

    public override void AddTips(List<IHoverTip> tips)
    {
        tips.Add(new HoverTip(GetLoc("title"), GetLoc()));
        tips.AddRange(ExtraHoverTips);
    }

    public override void ModifyDescription(Creature? target, ref string description)
    {
        LocString? beforeCard = null;
        LocString? afterCard = null;

        if (LocString.Exists("card_modifiers", $"{Id.Entry}.beforeCard"))
            beforeCard = GetLoc("beforeCard");
        
        if (LocString.Exists("card_modifiers", $"{Id.Entry}.afterCard"))
            afterCard = GetLoc("afterCard");

        if (beforeCard != null)
            description = $"{beforeCard.GetFormattedText()}\n{description}";
        
        if (afterCard != null)
            description = $"{description}\n{afterCard.GetFormattedText()}";
    }
}
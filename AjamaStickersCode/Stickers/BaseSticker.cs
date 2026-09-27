using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Assets;
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

    public Texture2D BigIcon
    {
        get
        {
            string path = $"res://AjamaStickers/images/stickers/{Id.Entry.RemovePrefix().ToLowerInvariant()}.png";
            const string placeholderPath = "res://AjamaStickers/images/stickers/base_sticker.png";
            
            return ResourceLoader.Exists(path) ?
                PreloadManager.Cache.GetTexture2D(path) :
                PreloadManager.Cache.GetTexture2D(placeholderPath);
        }
    }
    
    public Texture2D SmallIcon
    {
        get
        {
            string path = $"res://AjamaStickers/images/stickers/{Id.Entry.RemovePrefix().ToLowerInvariant()}_small.png";
            const string placeholderPath = "res://AjamaStickers/images/stickers/base_sticker_small.png";
            
            return ResourceLoader.Exists(path) ?
                PreloadManager.Cache.GetTexture2D(path) :
                PreloadManager.Cache.GetTexture2D(placeholderPath);
        }
    }

    public virtual bool ExtraConditionalToApply(CardModel card) => true;
    public virtual bool CanSpawn(Player player) => true;
    public virtual void WhenStickerApplied() { }
    
    public bool ValuesSetForThisSave { get; set; } = false;

    public LocString TitleLocString => GetLoc("title");
    public string Title => TitleLocString.GetFormattedText();

    public LocString SelectionScreenPrompt => GetLoc("selectionScreenPrompt");
    
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
        tips.Add(new HoverTip(TitleLocString, GetLoc(), SmallIcon));
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
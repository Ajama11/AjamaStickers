using BaseLib.Abstracts;
using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace AjamaStickers.AjamaStickersCode.Stickers;

public abstract class BaseSticker : CardModifier, ICustomModel
{
    public virtual bool RequiresCardToBePlayable => true;
    public virtual List<IHoverTip> ExtraHoverTips => [];
    protected virtual IEnumerable<DynamicVar> StickerCanonicalVars => [];
    
    public virtual bool ExtraConditionalToApply(CardModel card) => true;
    public virtual bool CanSpawn(Player player) => true;
    public virtual void WhenStickerApplied() { }

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
    
    public bool ValuesSetForThisSave { get; set; }

    public const string ActiveKey = "Active";
    protected sealed override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BoolVar(ActiveKey, true),
        ..StickerCanonicalVars
    ];

    public bool Active
    {
        get => ((BoolVar) DynamicVars[ActiveKey]).BoolVal;
        set => ((BoolVar) DynamicVars[ActiveKey]).BoolVal = value;
    }

    public LocString TitleLocString => GetLoc("title");
    public string Title => TitleLocString.GetFormattedText();

    public LocString SelectionScreenPrompt => GetLoc("selectionScreenPrompt");
    
    public static bool HasAnySticker(CardModel card) =>
        Modifiers(card).Any(m => m is BaseSticker);

    public bool CanApplySticker(CardModel card)
    {
        return !HasAnySticker(card) && CanApplyStickerInternal(card);
    }

    /// <summary>
    /// Doesn't care if the card already has a Sticker, for debug purposes.
    /// </summary>
    public bool CanApplyStickerInternal(CardModel card)
    {
        bool possiblyPlayable = true;
        if (RequiresCardToBePlayable) possiblyPlayable = !card.Keywords.Contains(CardKeyword.Unplayable);
        return possiblyPlayable && ExtraConditionalToApply(card);
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
        {
            string beforeCardText = beforeCard.GetFormattedText();
            
            if (!beforeCard.GetRawText().StartsWith("{Active:") && description.Length != 0)
            {
                beforeCardText = beforeCardText + "\n";
            }
            
            description = beforeCardText + description;
        }
        
        if (afterCard != null)
        {
            string afterCardText = afterCard.GetFormattedText();
            
            if (!afterCard.GetRawText().StartsWith("{Active:") && description.Length != 0)
            {
                afterCardText = "\n" + afterCardText;
            }
            
            description = description + afterCardText;
        }
    }
}
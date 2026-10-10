using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.ModInterop;
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

namespace AjamaStickers.AjamaStickersCode.Stickers.SpecificModded.AveMujica;

public class Nyamu() : BaseSticker
{
    public override bool CanSpawn(Player player) =>
        AveMujicaMod.IsLoaded &&
        player.Character.Id.Entry == AveMujicaMod.Oblivionis;

    public override bool ExtraConditionalToApply(CardModel card) =>
        AveMujicaMod.IsLoaded;

    private const string Enchant = "Enchant";
    
    public static readonly ModelId Masterful = new ("ENCHANTMENT", "AVEMUJICA-MASTERFUL");

    protected override IEnumerable<DynamicVar> StickerCanonicalVars =>
    [
        new IntVar(Enchant, 2)
    ];

    public override List<IHoverTip> ExtraHoverTips =>
        HoverTipFactory.FromEnchantmentOutside(Masterful);

    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card != Owner) return Task.CompletedTask;

        CardModel? card = Rng.CombatCardSelection.NextItem(Player.PlayerCombatState!.Hand.Cards);
        if (card == null) return Task.CompletedTask;

        AveMujicaMod.TryEnchantCardWithMasterful(card, DynamicVars[Enchant].IntValue);
        
        return Task.CompletedTask;
    }
}
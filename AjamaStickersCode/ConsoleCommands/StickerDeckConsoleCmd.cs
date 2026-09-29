using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Runs;

namespace AjamaStickers.AjamaStickersCode.ConsoleCommands;

public class StickerConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "ajamasticker-deck";
    public override string Args => "<id:string>";
    public override string Description => "Apply a Sticker to a card in your Deck.";
    public override bool IsNetworked => true;
    
    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (!RunManager.Instance.IsInProgress)
        {
            return new CmdResult(success: false, "A run is currently not in progress!");
        }
        
        if (args.Length == 0)
        {
            return new CmdResult(success: false, "No Sticker specified.");
        }

        string stickerName = args[0].ToUpperInvariant();
        
        BaseSticker? sticker = ModelDb.AllStickers.FirstOrDefault(s => s.Id.Entry == stickerName);
        if (sticker == null)
        {
            return new CmdResult(success: false, "Sticker not found");
        }

        Task task = ChooseCardsInDeckToApplyStickerTo(issuingPlayer!, sticker);
        
        return new CmdResult(task, success: true, $"Applied {sticker.Title} to cards in deck.");
    }
    
    public override CompletionResult GetArgumentCompletions(Player? player, string[] args)
    {
        if (args.Length <= 1)
        {
            List<string> candidates = ModelDb.AllStickers.Select(s => s.Id.Entry).ToList();
            return CompleteArgument(candidates, [], args.FirstOrDefault() ?? "");
        }
        
        return new CompletionResult
        {
            Type = CompletionType.Argument,
            ArgumentContext = CmdName
        };
    }

    public static async Task ChooseCardsInDeckToApplyStickerTo(Player player, BaseSticker sticker)
    {
        List<CardModel> cards = (await CardSelectCmd.FromDeckGeneric(player,
                new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, 9999),
                c => c.CanApplySticker(sticker)))
            .ToList();

        foreach (var card in cards)
        {
            card.ApplySticker(sticker);
        }
    }
}
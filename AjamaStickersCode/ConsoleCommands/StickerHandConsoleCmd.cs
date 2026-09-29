using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Stickers;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Runs;

namespace AjamaStickers.AjamaStickersCode.ConsoleCommands;

public class StickerCombatConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "ajamasticker-hand";
    public override string Args => "<id:string> <hand-index:int>";
    public override string Description => "Apply a Sticker to a card in your Hand.";
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

        int result = 0;
        
        if (args.Length != 0 && !int.TryParse(args[1], out result))
        {
            return new CmdResult(success: false, "Arg 1 must be the hand index (int), got '" + args[1] + "'.");
        }
        
        CardPile hand = PileType.Hand.GetPile(issuingPlayer!);
        
        if (result < 0 || result >= hand.Cards.Count)
        {
            return new CmdResult(success: false, $"Invalid hand index {result}. Valid range: 0-{hand.Cards.Count - 1}.");
        }
        
        CardModel card = hand.Cards[result];
        
        if (card.HasAnySticker())
        {
            return new CmdResult(success: false, $"{card.Title} already has a Sticker!");
        }

        BaseSticker? sticker = (BaseSticker?) ModelDb.AllStickers.FirstOrDefault(s => s.Id.Entry == stickerName)?.MutableClone();
        if (sticker == null)
        {
            return new CmdResult(success: false, "Sticker not found");
        }

        if (!card.CanApplySticker(sticker))
        {
            return new CmdResult(success: false, $"{sticker.Title} is incompatible with {card.Title}!");
        }
        
        card.AddModifier(sticker);
        NCard.FindOnTable(card)?.UpdateVisuals(PileType.Hand, CardPreviewMode.Normal);
        
        return new CmdResult(success: true, $"Applied {sticker.Title} to '{card.Title}' at index '{result}' in hand.");
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
}
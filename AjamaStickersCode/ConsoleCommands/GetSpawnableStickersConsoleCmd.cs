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

public class GetSpawnableStickersHandConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "ajamasticker-get_spawnable";
    public override string Args => "<hand-index:int>|<card-id:string>";
    public override string Description => "Print which Stickers can spawn for a card, either in your Hand or canonical.";
    public override bool IsNetworked => false;
    
    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (!RunManager.Instance.IsInProgress)
        {
            return new CmdResult(success: false, "Several Stickers need to know the player's character to be able to spawn, but a run is currently not in progress!");
        }
        
        if (args.Length == 0) return new CmdResult(success: false, "No card specified.");

        CardModel? card;
        
        if (int.TryParse(args[0], out var result))
        {
            CardPile hand = PileType.Hand.GetPile(issuingPlayer!);
        
            if (result < 0 || result >= hand.Cards.Count)
            {
                return new CmdResult(success: false, $"Invalid hand index {result}. Valid range: 0-{hand.Cards.Count - 1}.");
            }
        
            card = hand.Cards[result];
        }
        else
        {
            string cardName = args[0].ToUpperInvariant();
            
            card = ModelDb.AllCards.FirstOrDefault(c => c.Id.Entry == cardName);
            
            if (card == null)
            {
                return new CmdResult(success: false, "Card '" + cardName + "' not found");
            }
        }

        List<string> spawnableStickerNames = ModelDb.AllStickers
            .Where(s => 
                s.CanSpawn(issuingPlayer!) && 
                s.CanApplyStickerInternal(card))
            .Select(sticker => sticker.Title)
            .ToList();

        return new CmdResult(success: true, $"{spawnableStickerNames.AsReadable(", ")} can be applied to {card.Title}.");
    }
    
    public override CompletionResult GetArgumentCompletions(Player? player, string[] args)
    {
        if (args.Length <= 1)
        {
            List<string> candidates = ModelDb.AllCards.Select(card => card.Id.Entry).ToList();
            return CompleteArgument(candidates, [], args.FirstOrDefault() ?? "");
        }

        return new CompletionResult
        {
            Type = CompletionType.Argument,
            ArgumentContext = CmdName
        };
    }
}
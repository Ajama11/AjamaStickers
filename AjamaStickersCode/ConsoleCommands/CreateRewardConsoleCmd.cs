using AjamaStickers.AjamaStickersCode.Extensions;
using AjamaStickers.AjamaStickersCode.Rewards;
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

public class CreateRewardConsoleCmd : AbstractConsoleCmd
{
    public override string CmdName => "ajamasticker-reward";
    public override string Args => "";
    public override string Description => "Create a Sticker Reward.";
    public override bool IsNetworked => true;
    
    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (!RunManager.Instance.IsInProgress)
        {
            return new CmdResult(success: false, "A run is currently not in progress!");
        }

        Task task = OfferReward(issuingPlayer!);
        
        return new CmdResult(task, success: true, "Spawned Sticker reward.");
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

    public static async Task OfferReward(Player player)
    {
        await RewardsCmd.OfferCustom(player, [new StickerReward(player)]);
    }
}
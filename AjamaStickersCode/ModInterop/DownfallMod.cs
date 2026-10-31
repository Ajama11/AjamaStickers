using System.Reflection;
using AjamaStickers.AjamaStickersCode.Utils;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Modding;

namespace AjamaStickers.AjamaStickersCode.ModInterop;

public static class DownfallMod
{
    public static bool IsLoaded { get; } = Interop.IsLoaded("Downfall");
    private static Assembly? MainAssembly { get; } = Interop.GetAssembly("Downfall", "Downfall");

    // public static bool TestTheThing()
    // {
    //     var assemblies = ModManager.GetLoadedMods()
    //         .FirstOrDefault(mod => mod.manifest?.id == "Downfall")?
    //         .assemblies;
    //     
    //     MainFile.Logger.Warn($"{assemblies!.Count}");
    //     MainFile.Logger.Warn($"{assemblies.AsReadable("\n")}");
    //
    //     return true;
    // }
}
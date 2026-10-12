using System.Reflection;
using Godot;
using MegaCrit.Sts2.Core.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Modding;

namespace AjamaStickers.AjamaStickersCode.Utils;

public static class Interop
{
    private static void ReportNotFound(string type, string location, string name)
    {
        SfxCmd.Play(FmodSfx.merchantDisappointment, 5);
        MainFile.Logger.Warn($"{type} {name} was not found in {location} !!!");
    }

    public static bool IsLoaded(string modId)
    {
        return ModManager.GetLoadedMods()
            .Any(mod => mod.manifest?.id == modId);
    }

    public static Assembly? GetAssembly(string modId, string? subModIdentifier = null)
    {
        var assemblies = ModManager.GetLoadedMods()
            .FirstOrDefault(mod => mod.manifest?.id == modId)?
            .assemblies;

        if (assemblies == null)
        {
            ReportNotFound("Assembly", "assemblies", modId);
            return null;
        }
        
        if (subModIdentifier == null) return assemblies.FirstOrDefault();

        var subModAssembly = assemblies.FirstOrDefault(a =>
            a.FullName?.Contains(subModIdentifier, StringComparison.OrdinalIgnoreCase) ?? false);

        if (subModAssembly != null) return subModAssembly;

        ReportNotFound("Sub-Assembly", "assemblies", subModIdentifier);
        return null;
    }
    
    public static MethodInfo? GetMethod(Assembly? assembly, string location, string name)
    {
        var method = assembly?.GetType(location)?.GetMethod(name);
        
        if (method != null) return method;
        
        ReportNotFound("Method", location, name);
        return null;
    }

    public static object? GetField(Assembly? assembly, string location, string name, object? instance = null, string errorMsgName = "Field")
    {
        var field = assembly?
            .GetType(location)?
            .GetField(name)?
            .GetValue(instance);

        if (field != null) return field;
        
        ReportNotFound(errorMsgName, location, name);
        return null;
    }
    
    public static object? GetProperty(Assembly? assembly, string location, string name, object? instance = null, string errorMsgName = "Property")
    {
        var property = assembly?
            .GetType(location)?
            .GetProperty(name)?
            .GetValue(instance);

        if (property != null) return property;
        
        ReportNotFound(errorMsgName, location, name);
        return null;
    }

    public static CardKeyword GetKeyword(Assembly? assembly, string location, string name)
    {
        var keyword = GetField(assembly, location, name, errorMsgName: "Keyword");
        
        if (keyword != null) return (CardKeyword) keyword;
        
        return CardKeyword.None;
    }

    public static Type GetType(Assembly? assembly, string location, string name)
    {
        var type = assembly?.GetType($"{location}.{name}");

        if (type != null) return type;
        
        ReportNotFound("Type", location, name);
        return typeof(int);
    }
    
    public static Type GetType(Assembly? assembly, string locationAndName)
    {
        var type = assembly?.GetType(locationAndName);

        if (type != null) return type;

        int lastPeriod = locationAndName.RFind(".");
        ReportNotFound("Type", locationAndName.Left(lastPeriod), locationAndName.Right(lastPeriod + 1));
        return typeof(int);
    }
}
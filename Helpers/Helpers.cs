using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;

namespace FreeDayPlugin;

public partial class FreeDayPlugin : BasePlugin
{
    public void ResetSuts()
    {
        FreeDayPlayers.Clear();
    }

    public bool HasPermission(CCSPlayerController? player, List<string> permissions)
    {
        if (player == null || !player.IsValid) return false;
        foreach (var perm in permissions)
        {
            if (AdminManager.PlayerHasPermissions(player, perm))
            {
                return true;
            }
        }
        player.PrintToChat(Localizer["Prefix"] + Localizer["NoPermission"]);
        return false;
    }

    public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;
        if (FreeDayPlayers.ContainsKey(player))
        {
            FreeDayPlayers.Remove(player);
        }
        return HookResult.Continue;
    }

    public HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;
        if (FreeDayPlayers.ContainsKey(player))
        {
            FreeDayPlayers.Remove(player);
        }
        return HookResult.Continue;
    }

    public HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;
        if (FreeDayPlayers.ContainsKey(player))
        {
            FreeDayPlayers.Remove(player);
        }
        return HookResult.Continue;
    }

    public HookResult OnWeaponCanAcquire(DynamicHook hook)
    {
        CCSWeaponBaseVData vdata = VirtualFunctions.GetCSWeaponDataFromKeyFunc.Invoke(-1, hook.GetParam<CEconItemView>(1).ItemDefinitionIndex.ToString()) ?? throw new Exception("Failed to get CCSWeaponBaseVData");
        CCSPlayerController player = hook.GetParam<CCSPlayer_ItemServices>(0).Pawn.Value!.Controller.Value!.As<CCSPlayerController>();

        if (player == null || !player.IsValid || !player.PawnIsAlive)
            return HookResult.Continue;

        if (FreeDayPlayers.ContainsKey(player))
        {
            if (!FreeDayPlayers[player])
            {
                player.PrintToChat(Localizer["Prefix"] + Localizer["CantEquipWeapon"]);
                FreeDayPlayers[player] = true;
            }
            return HookResult.Stop;
        }

        return HookResult.Continue;
    }

    public HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        ResetSuts();
        return HookResult.Continue;
    }

}
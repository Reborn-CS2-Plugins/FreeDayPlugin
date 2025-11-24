using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace FreeDayPlugin;

public partial class FreeDayPlugin : BasePlugin
{
    public void SutCommand(CCSPlayerController? invoker, CommandInfo info)
    {
        if (invoker == null || !invoker.IsValid || invoker.PlayerPawn == null || invoker.PlayerPawn.Value == null) return;

        if (!invoker.PawnIsAlive || invoker.TeamNum != 2) return;

        if (!SutEnabled)
        {
            invoker.PrintToChat(Localizer["Prefix"] + Localizer["SutNotEnabled"]);
            return;
        }

        if (FreeDayPlayers.ContainsKey(invoker)) return;

        FreeDayPlayers.Add(invoker, false);
        Server.PrintToChatAll(Localizer["Prefix"] + Localizer["BecameSut", invoker.PlayerName]);
        invoker.RemoveWeapons();
        invoker.PlayerPawn.Value.SetModel(Config.SutModelPath);
    }

    public void DisableSutCommand(CCSPlayerController? invoker, CommandInfo info)
    {
        if (invoker == null || !invoker.IsValid || invoker.PlayerPawn == null || invoker.PlayerPawn.Value == null) return;

        if (!HasPermission(invoker, Config.CommandPerm)) return;

        if (!SutEnabled) return;

        foreach (var player in FreeDayPlayers.Keys.ToList())
        {
            if (player == null || !player.IsValid || player.PlayerPawn == null || player.PlayerPawn.Value == null || player.PlayerPawn.Value.AbsOrigin == null) continue;

            CCSPlayerPawn playerPawn = player.PlayerPawn.Value;
            Vector savedPosition = new Vector(
                player.PlayerPawn.Value.AbsOrigin.X,
                player.PlayerPawn.Value.AbsOrigin.Y,
                player.PlayerPawn.Value.AbsOrigin.Z
            );
            var eyeAngles = playerPawn.EyeAngles;

            player.Respawn();
            playerPawn = player.PlayerPawn.Value;
            playerPawn.Teleport(savedPosition, eyeAngles, Vector.Zero);
        }

        ResetSuts();
        SutEnabled = false;
        Server.PrintToChatAll(Localizer["Prefix"] + Localizer["SutDisabledBy", invoker.PlayerName]);
    }

    public void EnableSutCommand(CCSPlayerController? invoker, CommandInfo info)
    {
        if (invoker == null || !invoker.IsValid) return;

        if (!HasPermission(invoker, Config.CommandPerm)) return;

        if (SutEnabled) return;

        SutEnabled = true;
        Server.PrintToChatAll(Localizer["Prefix"] + Localizer["SutEnabledBy", invoker.PlayerName]);
    }

    public void ResetSutCommand(CCSPlayerController? invoker, CommandInfo info)
    {
        if (invoker == null || !invoker.IsValid) return;

        if (!HasPermission(invoker, Config.CommandPerm)) return;

        if (!SutEnabled) return;

        foreach (var player in FreeDayPlayers.Keys.ToList())
        {
            if (player == null || !player.IsValid || player.PlayerPawn == null || player.PlayerPawn.Value == null || player.PlayerPawn.Value.AbsOrigin == null) continue;

            CCSPlayerPawn playerPawn = player.PlayerPawn.Value;
            Vector savedPosition = new Vector(
                player.PlayerPawn.Value.AbsOrigin.X,
                player.PlayerPawn.Value.AbsOrigin.Y,
                player.PlayerPawn.Value.AbsOrigin.Z
            );

            if (savedPosition == null) continue;

            player.Respawn();
            playerPawn.Teleport(savedPosition, playerPawn.EyeAngles, Vector.Zero);
        }

        Server.PrintToChatAll(Localizer["Prefix"] + Localizer["SutsResettedBy", invoker.PlayerName]);
        ResetSuts();
    }
}
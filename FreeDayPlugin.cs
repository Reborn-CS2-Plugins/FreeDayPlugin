using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;

namespace FreeDayPlugin;

public partial class FreeDayPlugin : BasePlugin, IPluginConfig<FreeDayConfig>
{
    public override string ModuleName => "FreeDayPlugin";
    public override string ModuleAuthor => "belali yusuf";
    public override string ModuleVersion => "1.0.6";
    public FreeDayConfig Config { get; set; } = default!;
    public Dictionary<CCSPlayerController, bool> FreeDayPlayers { get; set; } = new Dictionary<CCSPlayerController, bool>();
    bool SutEnabled = true;

    public override void Load(bool hotReload)
    {
        RegisterCommands();
        RegisterHandlers();
        RegisterListener<Listeners.OnServerPrecacheResources>(OnServerPrecacheResources);
    }

    public void OnConfigParsed(FreeDayConfig config)
    {
        Config = config;
    }

    public void OnServerPrecacheResources(ResourceManifest resource)
    {
        Console.WriteLine("Sut Model: Precaching resource");
        resource.AddResource(Config.SutModelPath);
        Console.WriteLine($"Sut Model: Finished precaching {Config.SutModelPath}");
    }

    public void RegisterCommands()
    {
        foreach (var alias in Config.CommandAliasesSut)
        {
            AddCommand(alias, Localizer["SonaKalanDisableDescription"], SutCommand);
        }

        foreach (var alias in Config.CommandAliasesDisableSut)
        {
            AddCommand(alias, Localizer["SonaKalanDisableDescription"], DisableSutCommand);
        }

        foreach (var alias in Config.CommandAliasesEnableSut)
        {
            AddCommand(alias, Localizer["SonaKalanEnableDescription"], EnableSutCommand);
        }

        foreach (var alias in Config.CommandAliasesResetSut)
        {
            AddCommand(alias, Localizer["SonaKalanResetDescription"], ResetSutCommand);
        }
    }

    public void RegisterHandlers()
    {
        RegisterEventHandler<EventPlayerDisconnect>(OnPlayerDisconnect, HookMode.Pre);
        RegisterEventHandler<EventRoundStart>(OnRoundStart, HookMode.Post);
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn, HookMode.Pre);
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath, HookMode.Pre);
        VirtualFunctions.CCSPlayer_ItemServices_CanAcquireFunc.Hook(OnWeaponCanAcquire, HookMode.Pre);
    }
}

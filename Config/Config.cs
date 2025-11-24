using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace FreeDayPlugin;

public class FreeDayConfig : BasePluginConfig
{
    [JsonPropertyName("command_permission")]
    public List<string> CommandPerm { get; set; } = new List<string>
    {
    "@css/slay", "@jailbreak/warden", "@css/warden"
    };

    [JsonPropertyName("command_aliases_sut")]
    public List<string> CommandAliasesSut { get; set; } = new List<string> { "css_sutol", "css_sütol" };

    [JsonPropertyName("command_aliases_disable_sut")]
    public List<string> CommandAliasesDisableSut { get; set; } = new List<string> { "css_sut0", "css_süt0" };

    [JsonPropertyName("command_aliases_enable_sut")]
    public List<string> CommandAliasesEnableSut { get; set; } = new List<string> { "css_sut1", "css_süt1" };

    [JsonPropertyName("command_aliases_reset_sut")]
    public List<string> CommandAliasesResetSut { get; set; } = new List<string> { "css_resetsut", "css_resetsüt" };

    public string SutModelPath { get; set; } = "characters/models/ambrosian/reborn/sut/sut.vmdl";
}
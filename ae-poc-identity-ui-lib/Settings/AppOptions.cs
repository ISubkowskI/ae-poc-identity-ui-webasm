namespace Ae.Poc.Identity.Ui.Settings;

public sealed class AppOptions
{
    public const string App = "App";

    public string Title { get; set; } = String.Empty;
    public string Version { get; set; } = String.Empty;
    public string ClientId { get; set; } = String.Empty;

}

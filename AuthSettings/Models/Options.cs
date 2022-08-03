namespace AuthSettings.Models;

public class Options
{
    public string TenantUrl { get; set; } = string.Empty;
    public string DeployerClientId { get; set; } = string.Empty;
    public string DeployerClientSecret { get; set; } = string.Empty;
    public string DeployerAudience { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
}
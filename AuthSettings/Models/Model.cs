using System.Text.Json.Serialization;

namespace AuthSettings.Models;


public class Settings : BaseSettings
{
    [JsonPropertyName("client_id")] 
    public string ClientId { get; set; }
}


public class SettingsRequest : BaseSettings
{  
}


public abstract class BaseSettings
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("description")] 
    public string? Description { get; set; }
    
    [JsonPropertyName("logo_uri")] 
    public string? LogoUri { get; set; }

    [JsonPropertyName("callbacks")] 
    public List<string>? Callbacks { get; set; }

    [JsonPropertyName("allowed_origins")] 
    public List<string>? AllowedOrigins { get; set; }

    [JsonPropertyName("web_origins")] 
    public List<string>? WebOrigins { get; set; }

    [JsonPropertyName("grant_types")] 
    public List<string>? GrantTypes { get; set; }

    [JsonPropertyName("client_aliases")] 
    public List<string>? ClientAliases { get; set; }

    [JsonPropertyName("allowed_clients")] 
    public List<string>? AllowedClients { get; set; }

    [JsonPropertyName("allowed_logout_urls")]
    public List<string>? AllowedLogoutUrls { get; set; }

    [JsonPropertyName("jwt_configuration")]
    public JwtConfiguration? JwtConfiguration { get; set; }

    /*
    [JsonPropertyName("encryption_key")] 
    public EncryptionKey? EncryptionKey { get; set; }
    */

    [JsonPropertyName("oidc_conformant")] 
    public bool OidcConformant { get; set; }

    [JsonPropertyName("custom_login_page")]
    public string? CustomLoginPage { get; set; }

    [JsonPropertyName("custom_login_page_preview")]
    public string? CustomLoginPagePreview { get; set; }

    [JsonPropertyName("form_template")] 
    public string? FormTemplate { get; set; }

    [JsonPropertyName("initiate_login_uri")]
    public string? InitiateLoginUri { get; set; }

    [JsonPropertyName("organization_usage")]
    public string? OrganizationUsage { get; set; }

    [JsonPropertyName("organization_require_behavior")]
    public string? OrganizationRequireBehavior { get; set; }
    
}

public class EncryptionKey
{
    [JsonPropertyName("pub")] 
    public string? Pub { get; set; }

    [JsonPropertyName("cert")] 
    public string? Cert { get; set; }

    [JsonPropertyName("subject")] 
    public string? Subject { get; set; }
}

public class JwtConfiguration
{
    [JsonPropertyName("lifetime_in_seconds")]
    public int LifetimeInSeconds { get; set; }


    [JsonPropertyName("alg")] 
    public string? Alg { get; set; }
}

/*
public class OidcBackchannelLogout
{
    [JsonPropertyName("backchannel_logout_urls")]
    public List<string>? BackchannelLogoutUrls { get; set; }
}

public class RefreshToken
{
    [JsonPropertyName("rotation_type")] 
    public string? RotationType { get; set; }

    [JsonPropertyName("expiration_type")] 
    public string? ExpirationType { get; set; }

    [JsonPropertyName("leeway")] 
    public int Leeway { get; set; }

    [JsonPropertyName("token_lifetime")] 
    public int TokenLifetime { get; set; }

    [JsonPropertyName("infinite_token_lifetime")]
    public bool InfiniteTokenLifetime { get; set; }

    [JsonPropertyName("idle_token_lifetime")]
    public int IdleTokenLifetime { get; set; }

    [JsonPropertyName("infinite_idle_token_lifetime")]
    public bool InfiniteIdleTokenLifetime { get; set; }
}
*/
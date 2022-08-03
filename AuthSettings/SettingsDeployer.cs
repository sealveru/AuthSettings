using System.Text;
using System.Text.Json;
using AuthSettings.Models;
using Mapster;

namespace AuthSettings;

public interface ISettingsDeployer
{
    Task<bool> Deploy(Settings settings, Options options);
}

public class SettingsDeployer : ISettingsDeployer
{
    public async Task<bool> Deploy(Settings settings, Options options)
    {
        using var client = new HttpClient();

        client.BaseAddress = new Uri($"{options.TenantUrl}/api/v2/");
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {options.AuthToken}");

        var request = settings.Adapt<SettingsRequest>();
        var jsonRequest = JsonSerializer.Serialize(request);
        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");
        var response = await client.PatchAsync($"clients/{settings.ClientId}", content);

        Console.WriteLine(response);

        return response.IsSuccessStatusCode;
    }
}
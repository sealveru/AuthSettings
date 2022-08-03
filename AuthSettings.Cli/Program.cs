using System.Text.Json;
using AuthSettings;
using AuthSettings.Models;

try
{
    if (args.Length == 0)
    {
        Console.WriteLine("AuthSettings");
        Console.WriteLine("/validate verify the current configuration");
        Console.WriteLine("/deploy upload the current configuration to Auth0");
        return;
    }

    var reader = new FileReader();
    var runner = new ValidationRunner();
    var settings = reader.ReadAllFiles();

    switch (args[0])
    {
        case "/validate":
        {
            foreach (var setting in settings)
            {
                Console.WriteLine($"\nValidating {setting.ClientId}");

                var result = runner.Validate(setting).ToList();
                result.ForEach(r => Console.WriteLine(r.ToString()));

                if (result.All(e => e.IsValid))
                    Console.WriteLine("Ok");
            }

            break;
        }
        case "/deploy":
        {
            var deployer = new SettingsDeployer();
            var optionsString = File.ReadAllText("appsettings.json");
            var options = JsonSerializer.Deserialize<Options>(optionsString);

            foreach (var app in settings)
            {
                var result = runner.Validate(app).ToList();
                if (result.Any(e => !e.IsValid))
                {
                    Console.WriteLine($"Application with ClientId {app.ClientId} can not be deployed.");
                    result.ForEach(r => Console.WriteLine(r.ToString()));
                    continue;
                }

                var target = settings.First(s => s.ClientId == app.ClientId);

                await deployer.Deploy(target, options!);
            }

            break;
        }
        
        default:
            Console.WriteLine("Invalid parameter");
            break;
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unexpected error. Message: {e.Message}");
}

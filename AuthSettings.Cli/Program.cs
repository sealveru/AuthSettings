using AuthSettings;

try
{
    var reader = new FileReader();
    var runner = new ValidationRunner();

    var settings = reader.ReadAllFiles();

    foreach (var setting in settings)
    {
        Console.WriteLine($"\nValidating {setting.ClientId}");

        var result = runner.Validate(setting).ToList();
        result.ForEach(r => Console.WriteLine(r.ToString()));
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unexpected error. Message: {e.Message}");
}

Console.ReadKey();

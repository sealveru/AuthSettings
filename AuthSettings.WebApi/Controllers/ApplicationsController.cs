using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options = AuthSettings.Models.Options;

namespace AuthSettings.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IFileReader _reader;
    private readonly IValidationRunner _runner;
    private readonly ISettingsDeployer _deployer;
    private readonly IOptions<Options> _options;

    public ApplicationsController(IFileReader reader, IValidationRunner runner, ISettingsDeployer deployer, IOptions<Options> options)
    {
        _reader = reader;
        _runner = runner;
        _deployer = deployer;
        _options = options;
    }


    [HttpGet]
    public IEnumerable<Application> GetApplications()
    {
        var settings = _reader.ReadAllFiles();
        return settings.Select(s => new Application(s.ClientId, s.Name));
    }
    
    [HttpGet("{id}/validations")]
    public IEnumerable<ValidationResult> GetValidations(string id)
    {
        var setting = _reader.ReadAllFiles().FirstOrDefault(s => s.ClientId == id);
        return _runner.Validate(setting);
    }
    
    [HttpGet("{id}/deploy")]
    public async Task<bool> Deploy(string id)
    {
        var setting = _reader
            .ReadAllFiles()
            .FirstOrDefault(s => s.ClientId == id);

        if (setting is null)
            return false;

        return await _deployer.Deploy(setting, _options.Value);
    }
}

public record Application(string ClientId, string Name);

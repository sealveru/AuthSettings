using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AuthSettings.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApplicationsController : ControllerBase
{
    private readonly IFileReader _reader;
    private readonly IValidationRunner _runner;

    public ApplicationsController(IFileReader reader, IValidationRunner runner)
    {
        _reader = reader;
        _runner = runner;
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
}

public record Application(string ClientId, string Name);

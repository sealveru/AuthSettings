using System.Reflection;
using AuthSettings.Models;
using FluentValidation;
using FluentValidation.Results;

namespace AuthSettings;

public interface IValidationRunner
{
    IEnumerable<ValidationResult> Validate(SettingsResponse? settings);
}

public class ValidationRunner : IValidationRunner
{
    public IEnumerable<ValidationResult> Validate(SettingsResponse? settings)
    {
        if (settings is null)
            return GetNotFoundResponse();

        var validators = GetValidators();

        return validators
            .Select(validator => validator.Validate(settings))
            .Where(result => !result.IsValid)
            .ToList();
    }

    private static IEnumerable<AbstractValidator<SettingsResponse>> GetValidators()
    {
        var validatorType = typeof(AbstractValidator<SettingsResponse>);

        return Assembly
            .GetAssembly(typeof(ValidationRunner))!
            .GetTypes()
            .Where(type => validatorType.IsAssignableFrom(type))
            .Select(type => (AbstractValidator<SettingsResponse>) Activator.CreateInstance(type)!)
            .ToList();
    }

    private static IEnumerable<ValidationResult> GetNotFoundResponse()
    {
        var failures = new List<ValidationFailure>
        {
            new("ClientId", "ClientId Not Found")
        };

        return new List<ValidationResult> {new(failures)};
    }
}
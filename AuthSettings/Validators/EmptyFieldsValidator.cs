using AuthSettings.Models;
using FluentValidation;

namespace AuthSettings.Validators;

public class EmptyFieldsValidator : AbstractValidator<Settings>
{
    public EmptyFieldsValidator()
    {
        RuleFor(s => s.ClientId).NotEmpty();
        RuleFor(s => s.LogoUri).NotEmpty();
        RuleFor(s => s.AllowedOrigins).NotEmpty();
        RuleForEach(s => s.AllowedOrigins).NotEmpty();
    }
}
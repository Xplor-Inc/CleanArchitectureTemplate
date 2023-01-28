using CleanArchitectureTemplate.Core.Constants;
using CleanArchitectureTemplate.WebApp.Models.Dtos.Accounts;

namespace CleanArchitectureTemplate.WebApp.Validations.Accounts;

public class ValidateEmailLinkDtoValidator : AbstractValidator<ValidateEmailLinkDto>
{
    public ValidateEmailLinkDtoValidator()
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(StaticConfiguration.EMAIL_LENGTH)
            .WithMessage("Invalid emailAddress format");

        RuleFor(m => m.Resetlink)
            .NotEmpty()
            .WithMessage("Resetlink is required");
    }
}

using CleanArchitectureTemplate.Core.Constants;
using CleanArchitectureTemplate.WebApp.Models.Dtos.Accounts;

namespace CleanArchitectureTemplate.WebApp.Validations.Accounts;

public class ForgetPasswordDtoValidator : AbstractValidator<ForgetPasswordDto>
{
    public ForgetPasswordDtoValidator()
    {
        ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(m => m.EmailAddress)
            .NotEmpty()
            .WithMessage("EmailAddress is required")
            .EmailAddress()
            .WithMessage("Invalid email address")
            .MaximumLength(StaticConfiguration.EMAIL_LENGTH)
            .WithMessage("Invalid emailAddress format");
    }
}
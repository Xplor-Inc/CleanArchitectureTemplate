using CleanArchitectureTemplate.Core.Constants;
using CleanArchitectureTemplate.WebApp.Models.Dtos.Enquiries;

namespace CleanArchitectureTemplate.WebApp.Validations.Enquiries
{
    public class EnquiryResolutionDtoValidator : AbstractValidator<EnquiryResolutionDto>
    {
        public EnquiryResolutionDtoValidator()
        {
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(m => m.Resolution)
               .NotEmpty()
               .WithMessage("Resolution is required")
               .MaximumLength(StaticConfiguration.COMMAN_LENGTH)
               .WithMessage("Resolution is too long");
        }
    }
}
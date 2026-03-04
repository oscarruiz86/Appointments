
using Application.UseCases.Services.Commands;
using FluentValidation;

namespace Application.UseCases.Services.Validators
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0);
        }
    }
}

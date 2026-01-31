using Application.UseCases.Users.Commands;
using FluentValidation;

namespace Application.UseCases.Users.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(13)
                .Matches(@"^\+[1-9]\d{7,14}$")
                .WithMessage("El teléfono debe tener formato internacional válido, por ejemplo: +34600123456");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches("[A-Z]").WithMessage("Debe contener mayúscula")
                .Matches("[a-z]").WithMessage("Debe contener minúscula")
                .Matches("[0-9]").WithMessage("Debe contener número");

            RuleFor(x => x.RoleIds)
                .NotNull()
                .Must(r => r.Count > 0)
                .WithMessage("Debe asignar al menos un rol");
        }
    }
}

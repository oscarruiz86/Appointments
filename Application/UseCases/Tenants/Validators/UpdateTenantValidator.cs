using Application.UseCases.Tenants.Commands;
using FluentValidation;

namespace Application.UseCases.Tenants.Validators
{
   public class UpdateTenantValidator  : AbstractValidator<UpdateTenantCommand>
        {
        public UpdateTenantValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Timezone)
                .NotEmpty()
                .MaximumLength(6);

            RuleFor(x => x.Phone)
                .NotEmpty()
                .MaximumLength(13)
                .Matches(@"^\+[1-9]\d{7,14}$")
                .WithMessage("El teléfono debe tener formato internacional válido, por ejemplo: +34600123456");
        }
    }
}


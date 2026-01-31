using Application.UseCases.Users.Commands;
using FluentValidation;

namespace Application.UseCases.Users.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {

            RuleFor(x => x.UserId)
                .NotEmpty();


            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Phone)
                .NotEmpty()                
                .MaximumLength(13)
                .Matches(@"^\+[1-9]\d{7,14}$")
                .WithMessage("El teléfono debe tener formato internacional válido, por ejemplo: +34600123456");


        }
    }
}

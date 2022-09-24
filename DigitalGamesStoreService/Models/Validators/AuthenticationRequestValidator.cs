using DigitalGamesStoreService.Models.Requests;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .Length(6, 256)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .Length(5, 32);
    }
}
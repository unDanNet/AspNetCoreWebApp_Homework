using DigitalGamesStoreService.Models.Requests.Create;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
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
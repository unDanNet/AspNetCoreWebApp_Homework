using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
            
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .Length(6, 256)
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .Length(5, 32);

        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
    }
}
using DigitalGamesStoreService.Models.Requests.Create;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class UserPublicProfileCreateRequestValidator : AbstractValidator<UserPublicProfileCreateRequest>
{
    public UserPublicProfileCreateRequestValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);

        RuleFor(x => x.Nickname)
            .NotNull()
            .NotEmpty()
            .Length(1, 32);

        RuleFor(x => x.ProfileDescription).MaximumLength(1024);
    }
}
using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class UserPublicProfileUpdateRequestValidator : AbstractValidator<UserPublicProfileUpdateRequest>
{
    public UserPublicProfileUpdateRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        
        RuleFor(x => x.Nickname)
            .NotNull()
            .NotEmpty()
            .Length(1, 32);

        RuleFor(x => x.ProfileDescription).MaximumLength(1024);

        RuleFor(x => x.CurrentlyPlayedGameId).GreaterThan(0);
    }
}
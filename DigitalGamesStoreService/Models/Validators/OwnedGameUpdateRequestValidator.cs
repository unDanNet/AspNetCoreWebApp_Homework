using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class OwnedGameUpdateRequestValidator : AbstractValidator<OwnedGameUpdateRequest>
{
    public OwnedGameUpdateRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.HoursPlayed).GreaterThanOrEqualTo(0);
    }
}
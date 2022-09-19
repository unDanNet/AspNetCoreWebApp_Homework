using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class GameUpdateRequestValidator : AbstractValidator<GameUpdateRequest>
{
    public GameUpdateRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .Length(2, 256);

        RuleFor(x => x.DeveloperName)
            .NotNull()
            .NotEmpty()
            .Length(2, 256);

        RuleFor(x => x.Description).MaximumLength(1024);

        RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
    }
}
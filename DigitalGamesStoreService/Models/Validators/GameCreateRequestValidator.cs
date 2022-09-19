using DigitalGamesStoreService.Models.Requests.Create;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class GameCreateRequestValidator : AbstractValidator<GameCreateRequest>
{
    public GameCreateRequestValidator()
    {
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
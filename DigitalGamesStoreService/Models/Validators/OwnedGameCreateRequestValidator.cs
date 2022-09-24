using DigitalGamesStoreService.Models.Requests.Create;
using FluentValidation;

namespace DigitalGamesStoreService.Models.Validators;

public class OwnedGameCreateRequestValidator : AbstractValidator<OwnedGameCreateRequest>
{
    public OwnedGameCreateRequestValidator()
    {
        RuleFor(x => x.GameId).GreaterThan(0);
        RuleFor(x => x.OwnerId).GreaterThan(0);
    }
}
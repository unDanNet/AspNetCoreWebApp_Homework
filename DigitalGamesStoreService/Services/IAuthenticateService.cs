using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests;
using DigitalGamesStoreService.Models.Responses;

namespace DigitalGamesStoreService.Services;

public interface IAuthenticateService
{
    public AuthenticationResponse SignIn(AuthenticationRequest request);

    public UserSessionDto GetSession(string sessionToken);
}
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Models.Responses;

public class AuthenticationResponse
{
    public UserSessionDto Session { get; set; }
    public AuthenticationStatus Status { get; set; }
}

public enum AuthenticationStatus
{
    Success, 
    UserNotFound, 
    InvalidPassword
}
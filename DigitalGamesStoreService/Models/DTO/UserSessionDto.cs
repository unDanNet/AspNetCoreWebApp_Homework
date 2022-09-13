namespace DigitalGamesStoreService.Models.DTO;

public class UserSessionDto
{
    public int SessionId { get; set; }
    
    public string SessionToken { get; set; }
    
    public UserDto User { get; set; }
}
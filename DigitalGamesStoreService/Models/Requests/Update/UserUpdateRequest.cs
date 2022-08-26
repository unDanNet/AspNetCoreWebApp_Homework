namespace DigitalGamesStoreService.Models.Requests.Update;

public class UserUpdateRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Guid> OwnedGameIds { get; set; }
    public decimal Balance { get; set; }
}
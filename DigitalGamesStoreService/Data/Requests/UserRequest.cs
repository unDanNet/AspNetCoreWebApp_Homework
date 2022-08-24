namespace DigitalGamesStoreService.Data.Requests;

public class UserRequest
{
    public int Id { get; set; }
    public int PublicProfileId { get; set; }
    public IEnumerable<Guid> OwnedGameIds { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
}
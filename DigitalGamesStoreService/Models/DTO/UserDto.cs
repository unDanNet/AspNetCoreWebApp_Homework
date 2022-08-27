namespace DigitalGamesStoreService.Models.DTO;

public class UserDto
{
    public int Id { get; set; }
    public int PublicProfileId { get; set; }
    public IEnumerable<Guid> OwnedGameIds { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
}
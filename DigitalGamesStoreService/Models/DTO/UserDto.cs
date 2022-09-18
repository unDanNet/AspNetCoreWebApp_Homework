using DigitalGamesStoreService.Data;

namespace DigitalGamesStoreService.Models.DTO;

public class UserDto
{
    public int Id { get; set; }
    public UserPublicProfile PublicProfile { get; set; }
    public IEnumerable<OwnedGame> OwnedGames { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
}
using DigitalGamesStoreService.Data;

namespace DigitalGamesStoreService.Models.Requests.Update;

public class UserUpdateRequest
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public decimal Balance { get; set; }
}
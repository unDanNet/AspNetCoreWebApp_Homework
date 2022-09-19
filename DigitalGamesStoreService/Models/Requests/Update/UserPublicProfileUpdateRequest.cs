namespace DigitalGamesStoreService.Models.Requests.Update;

public class UserPublicProfileUpdateRequest
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string ProfileDescription { get; set; }
    public int? CurrentlyPlayedGameId { get; set; }
}
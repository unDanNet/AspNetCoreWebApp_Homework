namespace DigitalGamesStoreService.Models.Requests.Update;

public class UserPublicProfileUpdateRequest
{
    public string Nickname { get; set; }
    public string ProfileDescription { get; set; }
    public IEnumerable<int> FriendsProfileIds { get; set; }
    public int CurrentlyPlayedGameId { get; set; }
}
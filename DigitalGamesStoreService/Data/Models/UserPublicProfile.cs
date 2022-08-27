namespace DigitalGamesStoreService.Models;

public class UserPublicProfile
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string ProfileDescription { get; set; }
    public IEnumerable<int> FriendsProfileIds { get; set; }
    public int CurrentlyPlayedGameId { get; set; }
}
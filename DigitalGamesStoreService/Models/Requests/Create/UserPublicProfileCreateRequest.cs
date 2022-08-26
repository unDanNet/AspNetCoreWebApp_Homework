namespace DigitalGamesStoreService.Models.Requests.Create;

public class UserPublicProfileCreateRequest
{
    public int UserId { get; set; }
    public string Nickname { get; set; }
    public string ProfileDescription { get; set; }
}
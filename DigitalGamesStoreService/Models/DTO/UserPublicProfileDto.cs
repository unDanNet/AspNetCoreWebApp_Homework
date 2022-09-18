using DigitalGamesStoreService.Data;

namespace DigitalGamesStoreService.Models.DTO;

public class UserPublicProfileDto
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string ProfileDescription { get; set; }
    public int UserId { get; set; }
    public Game CurrentlyPlayedGame { get; set; }
}
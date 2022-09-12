namespace DigitalGamesStoreService.Models.Requests.Create;

public class OwnedGameCreateRequest
{
    public int GameId { get; set; }
    public int OwnerId { get; set; }
}
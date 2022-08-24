namespace DigitalGamesStoreService.Data.Requests;

public class OwnedGameRequest
{
    public Guid Id { get; set; }
    public int GameId { get; set; }
    public float HoursPlayed { get; set; }
    public bool IsFavourite { get; set; }
}
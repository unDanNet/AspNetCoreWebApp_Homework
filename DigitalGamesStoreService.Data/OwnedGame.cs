namespace DigitalGamesStoreService.Data;

public class OwnedGame
{
    public Guid Id { get; set; }
    public int GameId { get; set; }
    public float HoursPlayed { get; set; }
    public bool IsFavourite { get; set; }
}
namespace DigitalGamesStoreService.Models.DTO;

public class OwnedGameDto
{
    public Guid Id { get; set; }
    public int GameId { get; set; }
    public float HoursPlayed { get; set; }
    public bool IsFavourite { get; set; }
}
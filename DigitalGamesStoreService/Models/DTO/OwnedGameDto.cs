using DigitalGamesStoreService.Data;

namespace DigitalGamesStoreService.Models.DTO;

public class OwnedGameDto
{
    public Guid Id { get; set; }
    public Game Game { get; set; }
    public int OwnerId { get; set; }
    public float HoursPlayed { get; set; }
    public bool IsFavourite { get; set; }
}
namespace DigitalGamesStoreService.Models.Requests.Update;

public class OwnedGameUpdateRequest
{
    public Guid Id { get; set; }
    public float HoursPlayed { get; set; }
    public bool IsFavourite { get; set; }
}
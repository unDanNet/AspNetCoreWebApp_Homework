namespace DigitalGamesStoreService.Models.Requests.Update;

public class GameUpdateRequest
{
    public string Name { get; set; }
    public string DeveloperName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
}
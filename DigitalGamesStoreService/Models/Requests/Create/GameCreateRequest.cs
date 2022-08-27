namespace DigitalGamesStoreService.Models.Requests.Create;

public class GameCreateRequest
{
    public string Name { get; set; }
    public string DeveloperName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
}
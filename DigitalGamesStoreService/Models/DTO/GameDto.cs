namespace DigitalGamesStoreService.Models.DTO;

public class GameDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DeveloperName { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
}
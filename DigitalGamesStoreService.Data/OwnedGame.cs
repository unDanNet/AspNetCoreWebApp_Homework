using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalGamesStoreService.Data;

[Table(nameof(OwnedGame))]
public class OwnedGame
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(Data.Game))]
    public int GameId { get; set; }
    public Game Game { get; set; }
    
    [ForeignKey(nameof(Data.User))]
    public int UserId { get; set; }

    [Column]
    public float HoursPlayed { get; set; }
    
    [Column]
    public bool IsFavourite { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalGamesStoreService.Data;

public class UserPublicProfile
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Data.User))]
    public int UserId { get; set; }

    [Column]
    [StringLength(32)]
    public string Nickname { get; set; }
    
    [Column]
    [StringLength(1024)]
    public string ProfileDescription { get; set; }

    [ForeignKey(nameof(Game))]
    public int? CurrentlyPlayedGameId { get; set; }
    public Game CurrentlyPlayedGame { get; set; }
    
}
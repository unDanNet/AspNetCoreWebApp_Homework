using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalGamesStoreService.Data;

[Table(nameof(User))]
public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public UserPublicProfile UserPublicProfile { get; set; }

    [Column]
    [Required]
    [StringLength(255)]
    public string Email { get; set; }
    
    [Column]
    [Required]
    [StringLength(31)]
    public string Password { get; set; }
    
    [Column(TypeName = "money")]
    public decimal Balance { get; set; }
    
    public virtual IEnumerable<OwnedGame> OwnedGames { get; set; } = new HashSet<OwnedGame>();
}
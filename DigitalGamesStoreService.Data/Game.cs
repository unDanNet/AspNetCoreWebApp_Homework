using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalGamesStoreService.Data;

[Table(nameof(Game))]
public class Game
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column]
    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string Name { get; set; }
    
    [Column]
    [Required]
    [StringLength(255, MinimumLength = 2)]
    public string DeveloperName { get; set; }
    
    [Column]
    [StringLength(1023)]
    public string Description { get; set; }
    
    [Column(TypeName = "money")]
    public decimal Cost { get; set; }
}
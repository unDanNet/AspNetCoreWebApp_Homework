using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalGamesStoreService.Data;

[Table(nameof(UserSession))]
public class UserSession
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column]
    [Required]
    [StringLength(512)]
    public string SessionToken { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt { get; set; }
    
    [Column(TypeName = "timestamptz")]
    public DateTime LastRequestedAt { get; set; }
    
    [Column(TypeName = "timestamptz")]
    public DateTime? ExpiredAt { get; set; }
    
    [Column]
    public bool IsExpired { get; set; }
}
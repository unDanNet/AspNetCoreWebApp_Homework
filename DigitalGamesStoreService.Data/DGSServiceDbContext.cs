using Microsoft.EntityFrameworkCore;

namespace DigitalGamesStoreService.Data;

public sealed class DGSServiceDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserPublicProfile> Profiles { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<OwnedGame> OwnedGames { get; set; }
    public DbSet<UserSession> UserSessions { get; set; }

    public DGSServiceDbContext()
    {
        Database.EnsureCreated();
    }

    public DGSServiceDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasPostgresExtension("uuid-ossp");    

        modelBuilder.Entity<OwnedGame>()
            .Property(e => e.Id)
            .HasDefaultValueSql("uuid_generate_v4()");
    }
}
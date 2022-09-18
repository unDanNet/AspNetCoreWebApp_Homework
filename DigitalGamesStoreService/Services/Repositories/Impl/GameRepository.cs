using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class GameRepository : IGameRepository
{
    private readonly DGSServiceDbContext db;

    public GameRepository(DGSServiceDbContext db)
    {
        this.db = db;
    }
    
    public IEnumerable<Game> GetAll(bool loadInnerData = false)
    {
        return db.Games.ToList();
    }

    public Game GetById(int id, bool loadInnerData = false)
    {
        return db.Games.FirstOrDefault(g => g.Id == id);
    }

    public int Create(Game data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        
        db.Games.Add(data);
        db.SaveChanges();
        
        return data.Id;
    }

    public void Update(Game data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var game = GetById(data.Id, false);

        if (game is null)
        {
            throw new ArgumentException($"Could not find entity with given id ({data.Id}).");
        }

        game.Name = data.Name;
        game.Description = data.Description;
        game.DeveloperName = data.DeveloperName;
        game.Cost = data.Cost;

        db.SaveChanges();
    }

    public bool Delete(int id)
    {
        var game = GetById(id, false);

        if (game is null)
        {
            return false;
        }

        db.Games.Remove(game);
        db.SaveChanges();

        return true;
    }
}
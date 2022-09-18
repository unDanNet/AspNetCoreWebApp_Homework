using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class OwnedGameRepository : IOwnedGameRepository
{
    private readonly DGSServiceDbContext db;

    private void LoadData(OwnedGame game)
    {
        db.Entry(game).Reference(g => g.Game).Load();
    }
    
    public OwnedGameRepository(DGSServiceDbContext db)
    {
        this.db = db;
    }
    
    public IEnumerable<OwnedGame> GetAll(bool loadInnerData = false)
    {
        var ownedGames = db.OwnedGames.ToList();

        if (loadInnerData)
        {
            foreach (var ownedGame in ownedGames)
            {
                LoadData(ownedGame);
            }
        }

        return ownedGames;
    }

    public OwnedGame GetById(Guid id, bool loadInnerData = false)
    {
        var ownedGame = db.OwnedGames.FirstOrDefault(og => og.Id == id);

        if (ownedGame != null && loadInnerData)
        {
            LoadData(ownedGame);
        }

        return ownedGame;
    }

    public Guid Create(OwnedGame data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        db.OwnedGames.Add(data);
        db.SaveChanges();

        return data.Id;
    }

    public void Update(OwnedGame data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var ownedGame = GetById(data.Id, false);

        if (ownedGame is null)
        {
            throw new ArgumentException($"Could not find entity with given id ({data.Id}).");
        }

        ownedGame.HoursPlayed = data.HoursPlayed;
        ownedGame.IsFavourite = data.IsFavourite;

        db.SaveChanges();
    }

    public bool Delete(Guid id)
    {
        var ownedGame = GetById(id, false);

        if (ownedGame is null)
        {
            return false;
        }

        db.OwnedGames.Remove(ownedGame);
        
        return true;
    }
}
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class OwnedGameRepository : IOwnedGameRepository
{
    public IEnumerable<OwnedGame> GetAll()
    {
        return new List<OwnedGame>();
    }

    public OwnedGame GetById(Guid id)
    {
        return new OwnedGame();
    }

    public Guid Create(OwnedGame data)
    {
        return new Guid();
    }

    public void Update(OwnedGame data)
    {
        
    }

    public void Delete(Guid id)
    {
        
    }
}
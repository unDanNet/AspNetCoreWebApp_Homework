using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class GameRepository : IGameRepository
{
    public IEnumerable<Game> GetAll()
    {
        return new List<Game>();
    }

    public Game GetById(int id)
    {
        return new Game();
    }

    public int Create(Game data)
    {
        return 0;
    }

    public void Update(Game data)
    {

    }

    public void Delete(int id)
    {
        
    }
}
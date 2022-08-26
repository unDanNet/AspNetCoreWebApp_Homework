using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class UserRepository : IUserRepository
{
    public IEnumerable<User> GetAll()
    {
        return new List<User>();
    }

    public User GetById(int id)
    {
        return new User();
    }

    public int Create(User data)
    {
        return 0;
    }

    public void Update(User data)
    {
        
    }

    public void Delete(int id)
    {
        
    }
}
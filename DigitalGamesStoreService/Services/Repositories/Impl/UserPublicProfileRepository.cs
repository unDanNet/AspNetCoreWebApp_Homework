using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class UserPublicProfileRepository : IUserPublicProfileRepository
{
    public IEnumerable<UserPublicProfile> GetAll()
    {
        return new List<UserPublicProfile>();
    }

    public UserPublicProfile GetById(int id)
    {
        return new UserPublicProfile();
    }

    public int Create(UserPublicProfile data)
    {
        return 0;
    }

    public void Update(UserPublicProfile data)
    {
        
    }

    public void Delete(int id)
    {
        
    }
}
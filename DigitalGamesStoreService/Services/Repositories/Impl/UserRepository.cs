using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class UserRepository : IUserRepository
{
    private readonly DGSServiceDbContext db;

    public UserRepository(DGSServiceDbContext db)
    {
        this.db = db;
    }

    private void LoadData(User user)
    {
        db.Entry(user).Reference(u => u.UserPublicProfile).Load();
        db.Entry(user).Collection(u => u.OwnedGames).Load();
    }
    
    public IEnumerable<User> GetAll(bool loadInnerData = false)
    {
        var users = db.Users.ToList();

        if (loadInnerData)
        {
            foreach (var user in users)
            {
                LoadData(user);
            }
        }

        return users;
    }

    public User GetById(int id, bool loadInnerData = false)
    {
        var user = db.Users.FirstOrDefault(u => u.Id == id);

        if (user != null && loadInnerData)
        {
            LoadData(user);
        }

        return user;
    }

    public int Create(User data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }
        
        db.Users.Add(data);
        db.SaveChanges();

        return data.Id;
    }

    public void Update(User data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var user = GetById(data.Id, false);

        if (user is null)
        {
            throw new ArgumentException($"Could not find entity with given id ({data.Id}).");
        }

        user.Balance = data.Balance;
        user.Email = data.Email;
        user.Password = data.Password;

        db.SaveChanges();
    }

    public bool Delete(int id)
    {
        var user = GetById(id, false);

        if (user is null)
        {
            return false;
        }

        db.Users.Remove(user);
        db.SaveChanges();
        
        return true;
    }
}
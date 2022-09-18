using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.DTO;

namespace DigitalGamesStoreService.Services.Repositories.Impl;

public class UserPublicProfileRepository : IUserPublicProfileRepository
{
    private readonly DGSServiceDbContext db;

    public UserPublicProfileRepository(DGSServiceDbContext db)
    {
        this.db = db;
    }

    private void LoadData(UserPublicProfile profile)
    {
        db.Entry(profile).Reference(p => p.CurrentlyPlayedGame).Load();
    }
    
    public IEnumerable<UserPublicProfile> GetAll(bool loadInnerData = false)
    {
        var profiles = db.Profiles.ToList();

        if (loadInnerData)
        {
            foreach (var profile in profiles)
            {
                LoadData(profile);
            }
        }

        return profiles;
    }

    public UserPublicProfile GetById(int id, bool loadInnerData = false)
    {
        var profile = db.Profiles.FirstOrDefault(p => p.Id == id);

        if (profile != null && loadInnerData)
        {
            LoadData(profile);
        }

        return profile;
    }

    public int Create(UserPublicProfile data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        db.Profiles.Add(data);
        db.SaveChanges();

        return data.Id;
    }

    public void Update(UserPublicProfile data)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        var profile = GetById(data.Id, false);

        if (profile is null)
        {
            throw new ArgumentException($"Could not find entity with given id ({data.Id}).");
        }

        profile.Nickname = data.Nickname;
        profile.ProfileDescription = data.ProfileDescription;
        profile.CurrentlyPlayedGameId = data.CurrentlyPlayedGameId;

        db.SaveChanges();
    }

    public bool Delete(int id)
    {
        var profile = GetById(id, false);

        if (profile is null)
        {
            return false;
        }

        db.Profiles.Remove(profile);
        db.SaveChanges();
        
        return true;
    }
}
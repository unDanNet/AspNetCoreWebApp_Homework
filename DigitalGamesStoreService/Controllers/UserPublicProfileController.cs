using DigitalGamesStoreService.Data.Repositories;
using DigitalGamesStoreService.Data.Requests;
using DigitalGamesStoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserPublicProfileController
{
    #region Services

    private readonly IUserPublicProfileRepository userPublicProfileRepository;

    #endregion

    public UserPublicProfileController(IUserPublicProfileRepository userPublicProfileRepository)
    {
        this.userPublicProfileRepository = userPublicProfileRepository;
    }
    
    [HttpGet("get/all")]
    public IActionResult GetAllUsers()
    {
        return new OkObjectResult(userPublicProfileRepository.GetAll());
    }

    [HttpGet("get/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return new OkObjectResult(userPublicProfileRepository.GetById(id));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] UserPublicProfileRequest request)
    {
        var id = userPublicProfileRepository.Create(new UserPublicProfile {
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
            CurrentlyPlayedGameId = request.CurrentlyPlayedGameId,
            FriendsProfileIds = request.FriendsProfileIds
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] UserPublicProfile request)
    {
        userPublicProfileRepository.Update(new UserPublicProfile {
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
            CurrentlyPlayedGameId = request.CurrentlyPlayedGameId,
            FriendsProfileIds = request.FriendsProfileIds
        });

        return new OkResult();
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        userPublicProfileRepository.Delete(id);

        return new OkResult();
    }
}
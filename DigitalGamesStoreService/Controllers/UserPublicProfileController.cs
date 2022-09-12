using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserPublicProfileController
{
    #region Services

    private readonly ILogger<UserPublicProfileController> logger;
    private readonly IUserPublicProfileRepository userPublicProfileRepository;

    #endregion

    public UserPublicProfileController(ILogger<UserPublicProfileController> logger, IUserPublicProfileRepository userPublicProfileRepository)
    {
        this.logger = logger;
        this.userPublicProfileRepository = userPublicProfileRepository;
    }
    
    [HttpGet("get/all")]
    public ActionResult<List<UserPublicProfileDto>> GetAll()
    {
        logger.LogInformation("Got all user public profiles.");

        var result = userPublicProfileRepository.GetAll(true).Select(profile => new UserPublicProfileDto {
            Id = profile.Id,
            Nickname = profile.Nickname,
            ProfileDescription = profile.ProfileDescription,
            CurrentlyPlayedGame = profile.CurrentlyPlayedGame,
            UserId = profile.UserId
        }).ToList();
        
        return new OkObjectResult(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<UserPublicProfileDto> GetById([FromRoute] int id)
    {
        logger.LogInformation($"Got user public profile with id {id}.");

        var profile = userPublicProfileRepository.GetById(id, true);
        var result = new UserPublicProfileDto {
            Id = profile.Id,
            Nickname = profile.Nickname,
            ProfileDescription = profile.ProfileDescription,
            CurrentlyPlayedGame = profile.CurrentlyPlayedGame,
            UserId = profile.UserId
        };
        
        return new OkObjectResult(result);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] UserPublicProfileCreateRequest request)
    {
        logger.LogInformation("Created user public profile.");
        
        var id = userPublicProfileRepository.Create(new UserPublicProfile {
            UserId = request.UserId,
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] UserPublicProfileUpdateRequest request)
    {
        logger.LogInformation("Updated user public profile.");
        
        userPublicProfileRepository.Update(new UserPublicProfile {
            Id = request.Id,
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
            CurrentlyPlayedGameId = request.CurrentlyPlayedGameId,
        });

        return new OkResult();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted user public profile with id {id}");
        
        userPublicProfileRepository.Delete(id);

        return new OkResult();
    }
}
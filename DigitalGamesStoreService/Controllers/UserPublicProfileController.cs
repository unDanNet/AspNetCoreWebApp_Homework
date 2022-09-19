using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserPublicProfileController : ControllerBase
{
    #region Services

    private readonly ILogger<UserPublicProfileController> logger;
    private readonly IUserPublicProfileRepository userPublicProfileRepository;
    private readonly IValidator<UserPublicProfileCreateRequest> createRequestValidator;
    private readonly IValidator<UserPublicProfileUpdateRequest> updateRequestValidator;

    #endregion

    public UserPublicProfileController(ILogger<UserPublicProfileController> logger, 
        IUserPublicProfileRepository userPublicProfileRepository, 
        IValidator<UserPublicProfileCreateRequest> createRequestValidator,
        IValidator<UserPublicProfileUpdateRequest> updateRequestValidator)
    {
        this.logger = logger;
        this.userPublicProfileRepository = userPublicProfileRepository;
        this.createRequestValidator = createRequestValidator;
        this.updateRequestValidator = updateRequestValidator;
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
        
        return Ok(result);
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
        
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] UserPublicProfileCreateRequest request)
    {
        logger.LogInformation("Created user public profile.");

        var validationResult = createRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var id = userPublicProfileRepository.Create(new UserPublicProfile {
            UserId = request.UserId,
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
        });

        return Ok(id);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] UserPublicProfileUpdateRequest request)
    {
        logger.LogInformation("Updated user public profile.");

        var validationResult = updateRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        userPublicProfileRepository.Update(new UserPublicProfile {
            Id = request.Id,
            Nickname = request.Nickname,
            ProfileDescription = request.ProfileDescription,
            CurrentlyPlayedGameId = request.CurrentlyPlayedGameId,
        });

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted user public profile with id {id}");
        
        userPublicProfileRepository.Delete(id);

        return Ok();
    }
}
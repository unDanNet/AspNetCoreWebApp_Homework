using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController
{
    #region Services

    private readonly ILogger<UserController> logger;
    private readonly IUserRepository userRepository;

    #endregion

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        this.logger = logger;
        this.userRepository = userRepository;
    }

    [HttpGet("get/all")]
    public ActionResult<List<UserDto>> GetAll()
    {
        logger.LogInformation("Got all users.");

        var result = userRepository.GetAll().Select(user => new UserDto {
            Id = user.Id,
            PublicProfileId = user.PublicProfileId,
            OwnedGameIds = user.OwnedGameIds,
            Email = user.Email,
            Balance = user.Balance
        }).ToList();

        return new OkObjectResult(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<UserDto> GetById([FromRoute] int id)
    {
        logger.LogInformation($"Got user with id {id}.");

        var user = userRepository.GetById(id);
        var result = new UserDto {
            Id = user.Id,
            PublicProfileId = user.PublicProfileId,
            OwnedGameIds = user.OwnedGameIds,
            Email = user.Email,
            Balance = user.Balance
        };
        
        return new OkObjectResult(result);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] UserCreateRequest request)
    {
        logger.LogInformation("Created user.");
        
        var id = userRepository.Create(new User {
            Email = request.Email,
            Password = request.Password
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] UserUpdateRequest request)
    {
        logger.LogInformation("Updated user.");
        
        userRepository.Update(new User {
            Email = request.Email,
            Password = request.Password,
            Balance = request.Balance,
            OwnedGameIds = request.OwnedGameIds
        });

        return new OkResult();
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted user with id {id}.");
        
        userRepository.Delete(id);

        return new OkResult();
    }
}
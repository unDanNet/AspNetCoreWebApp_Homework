using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    #region Services

    private readonly ILogger<UserController> logger;
    private readonly IUserRepository userRepository;
    private readonly IValidator<UserCreateRequest> createRequestValidator;
    private readonly IValidator<UserUpdateRequest> updateRequestValidator;

    #endregion

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, 
        IValidator<UserCreateRequest> createRequestValidator, IValidator<UserUpdateRequest> updateRequestValidator)
    {
        this.logger = logger;
        this.userRepository = userRepository;
        this.createRequestValidator = createRequestValidator;
        this.updateRequestValidator = updateRequestValidator;
    }

    [HttpGet("get/all")]
    public ActionResult<List<UserDto>> GetAll()
    {
        logger.LogInformation("Got all users.");

        var users = userRepository.GetAll(true);

        var result = users.Select(user => new UserDto {
            Id = user.Id,
            PublicProfile = user.UserPublicProfile,
            OwnedGames = user.OwnedGames,
            Email = user.Email,
            Balance = user.Balance
        }).ToList();

        return Ok(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<UserDto> GetById([FromRoute] int id)
    {
        logger.LogInformation($"Got user with id {id}.");

        var user = userRepository.GetById(id, true);
        var result = new UserDto {
            Id = user.Id,
            PublicProfile = user.UserPublicProfile,
            OwnedGames = user.OwnedGames,
            Email = user.Email,
            Balance = user.Balance
        };
        
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] UserCreateRequest request)
    {
        logger.LogInformation("Created user.");

        var validationResult = createRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }

        var passwordComponents = PasswordUtils.CreatePasswordSecuredComponents(request.Password);
        
        var id = userRepository.Create(new User {
            Email = request.Email,
            PasswordSalt = passwordComponents.salt,
            Password = passwordComponents.hashedPassword
        });

        return Ok(id);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] UserUpdateRequest request)
    {
        logger.LogInformation("Updated user.");

        var validationResult = updateRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        userRepository.Update(new User {
            Id = request.Id,
            Email = request.Email,
            Password = request.Password,
            Balance = request.Balance,
        });

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted user with id {id}.");
        
        userRepository.Delete(id);

        return Ok();
    }
}
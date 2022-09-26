using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Services.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GameController : ControllerBase
{
    #region Services

    private readonly ILogger<GameController> logger;
    private readonly IGameRepository gameRepository;
    private readonly IValidator<GameCreateRequest> createRequestValidator;
    private readonly IValidator<GameUpdateRequest> updateRequestValidator;

    #endregion

    public GameController(ILogger<GameController> logger, IGameRepository gameRepository, 
        IValidator<GameCreateRequest> createRequestValidator, IValidator<GameUpdateRequest> updateRequestValidator)
    {
        this.logger = logger;
        this.gameRepository = gameRepository;
        this.createRequestValidator = createRequestValidator;
        this.updateRequestValidator = updateRequestValidator;
    }
    
    [HttpGet("get/all")]
    public ActionResult<List<GameDto>> GetAll()
    {
        logger.LogInformation("Got all games.");

        var result = gameRepository.GetAll().Select(game => new GameDto {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Cost = game.Cost,
            DeveloperName = game.DeveloperName
        }).ToList();
        
        return Ok(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<GameDto> GetById([FromRoute] int id)
    {
        logger.LogInformation($"Got game with id {id}.");

        var game = gameRepository.GetById(id);

        if (game is null)
        {
            return BadRequest();
        }
        
        var result = new GameDto {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Cost = game.Cost,
            DeveloperName = game.DeveloperName
        };
        
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public ActionResult<int> Create([FromBody] GameCreateRequest request)
    {
        logger.LogInformation("Created game.");

        var validationResult = createRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var id = gameRepository.Create(new Game {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = request.Cost
        });

        return Ok(id);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult Update([FromBody] GameUpdateRequest request)
    {
        logger.LogInformation("Updated game.");

        var validationResult = updateRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        gameRepository.Update(new Game {
            Id = request.Id,
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = request.Cost
        });

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted game with id {id}.");
        
        gameRepository.Delete(id);

        return Ok();
    }
}
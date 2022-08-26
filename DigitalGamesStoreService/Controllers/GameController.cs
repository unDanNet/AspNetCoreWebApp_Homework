using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController
{
    #region Services

    private readonly ILogger<GameController> logger;
    private readonly IGameRepository gameRepository;

    #endregion

    public GameController(ILogger<GameController> logger, IGameRepository gameRepository)
    {
        this.logger = logger;
        this.gameRepository = gameRepository;
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
        
        return new OkObjectResult(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<GameDto> GetById([FromRoute] int id)
    {
        logger.LogInformation($"Got game with id {id}.");

        var game = gameRepository.GetById(id);
        var result = new GameDto {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Cost = game.Cost,
            DeveloperName = game.DeveloperName
        };
        
        return new OkObjectResult(result);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] GameCreateRequest request)
    {
        logger.LogInformation("Created game.");
        
        var id = gameRepository.Create(new Game {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = request.Cost
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] GameUpdateRequest request)
    {
        logger.LogInformation("Updated game.");
        
        gameRepository.Update(new Game {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = request.Cost
        });

        return new OkResult();
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        logger.LogInformation($"Deleted game with id {id}.");
        
        gameRepository.Delete(id);

        return new OkResult();
    }
}
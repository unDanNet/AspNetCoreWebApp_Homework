using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnedGameController
{
    #region Services

    private readonly ILogger<OwnedGameController> logger;
    private readonly IOwnedGameRepository ownedGameRepository;

    #endregion

    public OwnedGameController(ILogger<OwnedGameController> logger, IOwnedGameRepository ownedGameRepository)
    {
        this.logger = logger;
        this.ownedGameRepository = ownedGameRepository;
    }

    [HttpGet("get/all")]
    public ActionResult<List<OwnedGameDto>> GetAll()
    {
        logger.LogInformation("Got all owned games");

        var result = ownedGameRepository.GetAll(true).Select(og => new OwnedGameDto {
            Game = og.Game,
            OwnerId = og.UserId,
            HoursPlayed = og.HoursPlayed,
            Id = og.Id,
            IsFavourite = og.IsFavourite
        }).ToList();

        return new OkObjectResult(result);
    }

    [HttpGet("get/{id}")]
    public ActionResult<OwnedGameDto> GetById([FromRoute] Guid id)
    {
        logger.LogInformation($"Got owned game with guid {id}.");

        var ownedGame = ownedGameRepository.GetById(id, false);
        var result = new OwnedGameDto {
            Game = ownedGame.Game,
            OwnerId = ownedGame.UserId,
            Id = ownedGame.Id,
            HoursPlayed = ownedGame.HoursPlayed,
            IsFavourite = ownedGame.IsFavourite
        };
        
        return new OkObjectResult(result);
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] OwnedGameCreateRequest request)
    {
        logger.LogInformation("Created owned game.");
        
        var id = ownedGameRepository.Create(new OwnedGame {
            GameId = request.GameId,
            UserId = request.OwnerId
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] OwnedGameUpdateRequest request)
    {
        logger.LogInformation("Updated owned game.");
        
        ownedGameRepository.Update(new OwnedGame {
            Id = request.Id,
            HoursPlayed = request.HoursPlayed,
            IsFavourite = request.IsFavourite
        });

        return new OkResult();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        logger.LogInformation($"Deleted game with guid {id}.");
        
        ownedGameRepository.Delete(id);

        return new OkResult();
    }
}
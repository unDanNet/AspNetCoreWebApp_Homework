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
    
    //TODO: Decide whether GET ALL method is needed for owned games
    // [HttpGet("get/all")]
    // public IActionResult GetAll()
    // {
    //     logger.LogInformation("Got all owned games.");
    //     return new OkObjectResult(ownedGameRepository.GetAll());
    // }

    [HttpGet("get/{id}")]
    public ActionResult<OwnedGameDto> GetById([FromRoute] Guid id)
    {
        logger.LogInformation($"Got owned game with guid {id}.");

        var ownedGame = ownedGameRepository.GetById(id);
        var result = new OwnedGameDto {
            GameId = ownedGame.GameId,
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
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] OwnedGameUpdateRequest request)
    {
        logger.LogInformation("Updated owned game.");
        
        ownedGameRepository.Update(new OwnedGame {
            HoursPlayed = request.HoursPlayed,
            IsFavourite = request.IsFavourite
        });

        return new OkResult();
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        logger.LogInformation($"Deleted game with guid {id}.");
        
        ownedGameRepository.Delete(id);

        return new OkResult();
    }
}
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
public class OwnedGameController : ControllerBase
{
    #region Services

    private readonly ILogger<OwnedGameController> logger;
    private readonly IOwnedGameRepository ownedGameRepository;
    private readonly IValidator<OwnedGameCreateRequest> createRequestValidator;
    private readonly IValidator<OwnedGameUpdateRequest> updateRequestValidator;

    #endregion

    public OwnedGameController(ILogger<OwnedGameController> logger, 
        IOwnedGameRepository ownedGameRepository, 
        IValidator<OwnedGameCreateRequest> createRequestValidator, 
        IValidator<OwnedGameUpdateRequest> updateRequestValidator)
    {
        this.logger = logger;
        this.ownedGameRepository = ownedGameRepository;
        this.createRequestValidator = createRequestValidator;
        this.updateRequestValidator = updateRequestValidator;
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

        return Ok(result);
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
        
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] OwnedGameCreateRequest request)
    {
        logger.LogInformation("Created owned game.");

        var validationResult = createRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var id = ownedGameRepository.Create(new OwnedGame {
            GameId = request.GameId,
            UserId = request.OwnerId
        });

        return Ok(id);
    }

    [HttpPost("update")]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] OwnedGameUpdateRequest request)
    {
        logger.LogInformation("Updated owned game.");

        var validationResult = updateRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        ownedGameRepository.Update(new OwnedGame {
            Id = request.Id,
            HoursPlayed = request.HoursPlayed,
            IsFavourite = request.IsFavourite
        });

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        logger.LogInformation($"Deleted game with guid {id}.");
        
        ownedGameRepository.Delete(id);

        return Ok();
    }
}
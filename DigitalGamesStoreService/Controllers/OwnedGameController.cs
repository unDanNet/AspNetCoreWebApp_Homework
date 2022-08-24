using DigitalGamesStoreService.Data.Repositories;
using DigitalGamesStoreService.Data.Requests;
using DigitalGamesStoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnedGameController
{
    #region Services

    private readonly IOwnedGameRepository ownedGameRepository;

    #endregion

    public OwnedGameController(IOwnedGameRepository ownedGameRepository)
    {
        this.ownedGameRepository = ownedGameRepository;
    }
    
    [HttpGet("get/all")]
    public IActionResult GetAllUsers()
    {
        return new OkObjectResult(ownedGameRepository.GetAll());
    }

    [HttpGet("get/{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        return new OkObjectResult(ownedGameRepository.GetById(id));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] OwnedGameRequest request)
    {
        var id = ownedGameRepository.Create(new OwnedGame {
            GameId = request.GameId,
            HoursPlayed = request.HoursPlayed,
            IsFavourite = request.IsFavourite
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] OwnedGameRequest request)
    {
        ownedGameRepository.Update(new OwnedGame {
            GameId = request.GameId,
            HoursPlayed = request.HoursPlayed,
            IsFavourite = request.IsFavourite
        });

        return new OkResult();
    }

    [HttpGet("delete/{id}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        ownedGameRepository.Delete(id);

        return new OkResult();
    }
}
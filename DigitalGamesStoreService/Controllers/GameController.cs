using DigitalGamesStoreService.Data.Repositories;
using DigitalGamesStoreService.Data.Requests;
using DigitalGamesStoreService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GameController
{
    #region Services

    private readonly IGameRepository gameRepository;

    #endregion

    public GameController(IGameRepository gameRepository)
    {
        this.gameRepository = gameRepository;
    }
    
    [HttpGet("get/all")]
    public IActionResult GetAllUsers()
    {
        return new OkObjectResult(gameRepository.GetAll());
    }

    [HttpGet("get/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return new OkObjectResult(gameRepository.GetById(id));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] GameRequest request)
    {
        var id = gameRepository.Create(new Game {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = request.Cost
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] Game request)
    {
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
        gameRepository.Delete(id);

        return new OkResult();
    }
}
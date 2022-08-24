using DigitalGamesStoreService.Data.Repositories;
using DigitalGamesStoreService.Data.Requests;
using DigitalGamesStoreService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DigitalGamesStoreService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController
{
    #region Services

    private readonly IUserRepository userRepository;

    #endregion

    public UserController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpGet("get/all")]
    public IActionResult GetAllUsers()
    {
        return new OkObjectResult(userRepository.GetAll());
    }

    [HttpGet("get/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        return new OkObjectResult(userRepository.GetById(id));
    }

    [HttpPost("create")]
    public IActionResult Create([FromBody] UserRequest request)
    {
        var id = userRepository.Create(new User {
            Email = request.Email,
            Password = request.Password,
            Balance = request.Balance,
            PublicProfileId = request.PublicProfileId,
            OwnedGameIds = request.OwnedGameIds
        });

        return new OkObjectResult(id);
    }

    [HttpPost("update")]
    public IActionResult Update([FromBody] UserRequest request)
    {
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
        userRepository.Delete(id);

        return new OkResult();
    }
}
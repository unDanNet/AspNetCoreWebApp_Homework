using System.ComponentModel.DataAnnotations;
using DigitalGamesStoreService.Controllers;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Models.Validators;
using DigitalGamesStoreService.Services.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DigitalGamesStoreServiceTests;

public class GameControllerTests
{
    private readonly GameController controller;

    private readonly Mock<IGameRepository> repositoryMock;
    private readonly Mock<ILogger<GameController>> loggerMock;
    private readonly IValidator<GameCreateRequest> createRequestValidator;
    private readonly IValidator<GameUpdateRequest> updateRequestValidator;
    
    public GameControllerTests()
    {
        repositoryMock = new Mock<IGameRepository>();
        loggerMock = new Mock<ILogger<GameController>>();
        createRequestValidator = new GameCreateRequestValidator();
        updateRequestValidator = new GameUpdateRequestValidator();

        controller = new GameController(
            loggerMock.Object,
            repositoryMock.Object,
            createRequestValidator,
            updateRequestValidator
        );
    }

    [Fact]
    public void GetAllTest()
    {
        //
        repositoryMock.Setup(r => r.GetAll(false)).Returns(new List<Game>());

        //
        var result = controller.GetAll();

        //
        repositoryMock.Verify(r => r.GetAll(false), Times.Once);

        Assert.NotNull(result);
        Assert.NotNull(result.Result);
        
        Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        Assert.IsAssignableFrom<ActionResult<List<GameDto>>>(result);
        
        Assert.Empty(((result.Result as OkObjectResult)!.Value as List<GameDto>)!);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    public void GetByIdTest(int id)
    {
        //
        repositoryMock.Setup(r => r.GetById(It.IsAny<int>(), false)).Returns(id == 1 ? It.IsAny<Game>() : null);

        //
        var result = controller.GetById(id);
        
        //
        repositoryMock.Verify(r => r.GetById(id, false), Times.Once);

        Assert.NotNull(result);
        Assert.IsAssignableFrom<ActionResult<GameDto>>(result);
        
        if (id == 1)
        {
            Assert.NotNull(result.Result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        else
        {
            Assert.IsAssignableFrom<BadRequestResult>(result.Result);
        }
    }

    [Theory]
    [InlineData("Among Us", "Innersloth", "A funny game of betrayal and teamwork", 133)]
    [InlineData("Among Us", "Innersloth", "A funny game of betrayal and teamwork", -10)]
    public void CreateTest(string name, string developerName, string description, decimal cost)
    {
        var game = new GameCreateRequest {
            Name = name,
            Description = description,
            DeveloperName = developerName,
            Cost = cost
        };

        repositoryMock.Setup(r => r.Create(It.IsAny<Game>())).Returns(It.IsAny<int>());

        var result = controller.Create(game);

        if (cost == 133)
        {
            repositoryMock.Verify(r => r.Create(It.IsAny<Game>()), Times.Once);
        }
        
        Assert.NotNull(result);

        if (cost == 133)
        {
            Assert.NotNull(result.Result);
            Assert.IsAssignableFrom<ActionResult<int>>(result);
            Assert.IsAssignableFrom<OkObjectResult>(result.Result);
        }
        else
        {
            Assert.IsAssignableFrom<BadRequestObjectResult>(result.Result);
        }
    }

    [Theory]
    [InlineData(-1, "Among Us", "Innersloth", "A funny game of betrayal and teamwork", 133)]
    [InlineData(1, "Among Us", "Innersloth", "A funny game of betrayal and teamwork", 133)]
    public void UpdateTest(int id, string name, string developerName, string description, decimal cost)
    {
        var game = new GameUpdateRequest {
            Id = id,
            Name = name,
            Description = description,
            DeveloperName = developerName,
            Cost = cost
        };

        repositoryMock.Setup(r => r.Update(It.IsAny<Game>()));

        var result = controller.Update(game);

        if (id == 1)
        {
            repositoryMock.Verify(r => r.Update(It.IsAny<Game>()), Times.Once);
        }
        
        Assert.NotNull(result);

        if (id == 1)
        {
            Assert.NotNull(result);
            Assert.IsAssignableFrom<OkResult>(result);
        }
        else
        {
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }
    }

    [Theory]
    [InlineData(1)]
    public void DeleteTest(int id)
    {
        repositoryMock.Setup(r => r.Delete(It.IsAny<int>()));

        var result = controller.Delete(1);
        
        repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        
        Assert.NotNull(result);
        Assert.IsAssignableFrom<OkResult>(result);
    }
}
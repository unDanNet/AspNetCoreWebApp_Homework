using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreServiceRpc;
using FluentValidation;
using Grpc.Core;
using static DigitalGamesStoreServiceRpc.GameService;

namespace DigitalGamesStoreService.RpcServices;

public class GameService : GameServiceBase
{
    #region Services
    
    private readonly IGameRepository gameRepository;
    private readonly IValidator<GameCreateRequest> createRequestValidator;
    private readonly IValidator<GameUpdateRequest> updateRequestValidator;

    #endregion
    
    public GameService(IGameRepository gameRepository, IValidator<GameCreateRequest> createRequestValidator, 
        IValidator<GameUpdateRequest> updateRequestValidator)
    {
        this.gameRepository = gameRepository;
        this.createRequestValidator = createRequestValidator;
        this.updateRequestValidator = updateRequestValidator;
    }

    public override Task<GetAllGamesResponse> GetAllGames(GetAllGamesRequest request, ServerCallContext context)
    {
        var result = gameRepository.GetAll().Select(game => new GameDto {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Cost = Convert.ToDouble(game.Cost),
            DeveloperName = game.DeveloperName
        }).ToList();

        var response = new GetAllGamesResponse();
        response.Games.AddRange(result);

        return Task.FromResult(response);
    }

    public override Task<GetGameByIdResponse> GetGameById(GetGameByIdRequest request, ServerCallContext context)
    {
        var game = gameRepository.GetById(request.Id);
        var result = new GameDto {
            Id = game.Id,
            Name = game.Name,
            Description = game.Description,
            Cost = Convert.ToDouble(game.Cost),
            DeveloperName = game.DeveloperName
        };

        var response = new GetGameByIdResponse {
            Game = result
        };

        return Task.FromResult(response);
    }

    public override Task<CreateGameResponse> CreateGame(CreateGameRequest request, ServerCallContext context)
    {
        var validationResult = createRequestValidator.Validate(new GameCreateRequest {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = Convert.ToDecimal(request.Cost)
        });
        
        if (!validationResult.IsValid)
        {
            return null;
        }
        
        var id = gameRepository.Create(new Game {
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = Convert.ToDecimal(request.Cost)
        });

        var response = new CreateGameResponse {
            Id = id
        };

        return Task.FromResult(response);
    }

    public override Task<UpdateGameResponse> UpdateGame(UpdateGameRequest request, ServerCallContext context)
    {
        var validationResult = updateRequestValidator.Validate(new GameUpdateRequest {
            Id = request.Id,
            Name = request.Name,
            Description = request.Description,
            DeveloperName = request.DeveloperName,
            Cost = Convert.ToDecimal(request.Cost)
        });
        
        if (!validationResult.IsValid)
        {
            return null;
        }
        
        gameRepository.Update(new Game {
            Id = request.Id,
            Name = request.Name,
            DeveloperName = request.DeveloperName,
            Description = request.Description,
            Cost = Convert.ToDecimal(request.Cost)
        });

        return Task.FromResult(new UpdateGameResponse());
    }

    public override Task<DeleteGameResponse> DeleteGame(DeleteGameRequest request, ServerCallContext context)
    {
        gameRepository.Delete(request.Id);
        return Task.FromResult(new DeleteGameResponse());
    }
}
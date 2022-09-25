// See https://aka.ms/new-console-template for more information

using DigitalGamesStoreServiceRpc;
using Grpc.Net.Client;

namespace DGSServiceClient
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress(
                "https://localhost:7157",
                new GrpcChannelOptions {
                    HttpHandler = new HttpClientHandler {
                        ServerCertificateCustomValidationCallback = 
                            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    }
                }
            );

            var client = new GameService.GameServiceClient(channel);

            Console.WriteLine("Specify information about the game to add:");
            
            Console.Write("Name: ");
            var gameName = Console.ReadLine() ?? "";
            
            Console.Write("Developer Name: ");
            var gameDeveloperName = Console.ReadLine() ?? "";
            
            Console.Write("Description: ");
            var gameDescription = Console.ReadLine() ?? "";
            
            Console.Write("Cost: ");
            var gameCost = Convert.ToDouble(Console.ReadLine());

            var createGameResponse = client.CreateGame(new CreateGameRequest {
                Name = gameName,
                DeveloperName = gameDeveloperName,
                Description = gameDescription,
                Cost = gameCost
            });

            if (createGameResponse is not null)
            {
                Console.WriteLine($"Game has been added successfully: #{createGameResponse.Id}");
            }

            var getAllGamesResponse = client.GetAllGames(new GetAllGamesRequest());
            
            Console.WriteLine("All games presented in the database:");
            foreach (var game in getAllGamesResponse.Games)
            {
                Console.WriteLine($"[{game.Id}, {game.Name}, {game.DeveloperName}, {game.Description}, {game.Cost}]");
            }
        }
    }
}
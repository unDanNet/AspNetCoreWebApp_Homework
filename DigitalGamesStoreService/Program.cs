using DigitalGamesStoreService.Data.Repositories;
using DigitalGamesStoreService.Data.Repositories.Impl;
using DigitalGamesStoreService.Models;

namespace DigitalGamesStoreService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserPublicProfileRepository, UserPublicProfileRepository>();
            builder.Services.AddScoped<IOwnedGameRepository, OwnedGameRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


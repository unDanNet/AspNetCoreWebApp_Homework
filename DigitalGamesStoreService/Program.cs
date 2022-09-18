using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Services.Repositories.Impl;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Services;
using DigitalGamesStoreService.Settings;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using NLog.Web;

namespace DigitalGamesStoreService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region App Settings Configuration

            builder.Services.Configure<NLogSettings>(settings =>
                builder.Configuration.GetSection("NLog").Bind(settings)
            );

            builder.Services.Configure<DatabaseSettings>(settings =>
                builder.Configuration.GetSection("Database").Bind(settings)
            );

            #endregion

            #region Logging Services Configuration

            builder.Services.AddHttpLogging(logging => {
                logging.LoggingFields = HttpLoggingFields.All | HttpLoggingFields.RequestQuery;
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
                logging.RequestHeaders.Add("Authorization");
                logging.RequestHeaders.Add("X-Real-IP");
                logging.RequestHeaders.Add("X-Forwarded-For");
            });

            builder.Host.ConfigureLogging(logging => {
                logging.ClearProviders();
                logging.AddConsole();
            }).UseNLog(new NLogAspNetCoreOptions {
                RemoveLoggerFactoryFilter = true
            });

            #endregion

            #region Configuring Database

            builder.Services.AddDbContext<DGSServiceDbContext>(options => {
                options.UseNpgsql(builder.Configuration["Database:ConnectionString"]);
            });

            #endregion

            #region Adding Repositories

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserPublicProfileRepository, UserPublicProfileRepository>();
            builder.Services.AddScoped<IOwnedGameRepository, OwnedGameRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            
            #endregion

            #region Adding Other Services

            builder.Services.AddSingleton<IAuthenticateService, AuthenticateService>();
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #endregion
            
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}


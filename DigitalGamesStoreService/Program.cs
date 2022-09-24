using System.Text;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Services.Repositories;
using DigitalGamesStoreService.Services.Repositories.Impl;
using DigitalGamesStoreService.Models;
using DigitalGamesStoreService.Models.Requests;
using DigitalGamesStoreService.Models.Requests.Create;
using DigitalGamesStoreService.Models.Requests.Update;
using DigitalGamesStoreService.Models.Validators;
using DigitalGamesStoreService.Services;
using DigitalGamesStoreService.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

            #region Configuring Authentication

            builder.Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthenticationService.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            #endregion

            #region Configuring Swagger

            builder.Services.AddSwaggerGen(options => {
                options.SwaggerDoc(
                    "v1", 
                    new OpenApiInfo {
                        Title = "Digital Games Store Service",
                        Version = "v1"
                    }
                );
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme {
                        Description = "JWT Authorization header using the Bearer Scheme. For example: 'Bearer a34DfdS'",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    }
                );
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            #endregion

            #region Configure Validation

            builder.Services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            
            builder.Services.AddScoped<IValidator<GameCreateRequest>, GameCreateRequestValidator>();
            builder.Services.AddScoped<IValidator<GameUpdateRequest>, GameUpdateRequestValidator>();
            
            builder.Services.AddScoped<IValidator<OwnedGameCreateRequest>, OwnedGameCreateRequestValidator>();
            builder.Services.AddScoped<IValidator<OwnedGameUpdateRequest>, OwnedGameUpdateRequestValidator>();
            
            builder.Services.AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            builder.Services.AddScoped<IValidator<UserUpdateRequest>, UserUpdateRequestValidator>();
            
            builder.Services
                .AddScoped<IValidator<UserPublicProfileCreateRequest>, UserPublicProfileCreateRequestValidator>();
            builder.Services
                .AddScoped<IValidator<UserPublicProfileUpdateRequest>, UserPublicProfileUpdateRequestValidator>();

            #endregion
            
            #region Adding Other Services

            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            #endregion
            
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpLogging();

            app.MapControllers();

            app.Run();
        }
    }
}


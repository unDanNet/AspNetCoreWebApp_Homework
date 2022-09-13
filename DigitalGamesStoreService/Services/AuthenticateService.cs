using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitalGamesStoreService.Data;
using DigitalGamesStoreService.Models.DTO;
using DigitalGamesStoreService.Models.Requests;
using DigitalGamesStoreService.Models.Responses;
using Microsoft.IdentityModel.Tokens;

namespace DigitalGamesStoreService.Services;

public class AuthenticateService : IAuthenticateService
{
    public const string SecretKey = "dad17d9411f71955b500685f63f6b9e2628fab50";

    private readonly Dictionary<string, UserSessionDto> sessions = new Dictionary<string, UserSessionDto>();

    private readonly IServiceScopeFactory serviceScopeFactory;

    public AuthenticateService(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public AuthenticationResponse SignIn(AuthenticationRequest request)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<DGSServiceDbContext>();
        var user = !string.IsNullOrWhiteSpace(request.Email) ? GetUserByEmail(db, request.Email) : null;

        if (user is null)
        {
            return new AuthenticationResponse { Status = AuthenticationStatus.UserNotFound };
        }

        var correctPassword = PasswordUtils.VerifyPassword(request.Password, user.PasswordSalt, user.Password);

        if (correctPassword)
        {
            return new AuthenticationResponse { Status = AuthenticationStatus.InvalidPassword };
        }

        var session = new UserSession {
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            LastRequestedAt = DateTime.UtcNow,
            IsExpired = false,
            SessionToken = CreateSessionToken(user)
        };

        db.UserSessions.Add(session);
        db.SaveChanges();

        var sessionDto = GetSessionInfo(user, session);

        lock (sessions)
        {
            sessions[session.SessionToken] = sessionDto;
        }

        return new AuthenticationResponse {
            Status = AuthenticationStatus.Success,
            Session = sessionDto
        };
    }

    public UserSessionDto GetSession(string sessionToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<DGSServiceDbContext>();

        return new UserSessionDto();
    }

    private UserSessionDto GetSessionInfo(User user, UserSession session)
    {
        return new UserSessionDto {
            SessionId = session.Id,
            SessionToken = session.SessionToken,
            User = new UserDto {
                Id = user.Id,
                Email = user.Email,
                OwnedGames = user.OwnedGames,
                PublicProfile = user.UserPublicProfile,
                Balance = user.Balance
            }
        };
    }

    private User GetUserByEmail(DGSServiceDbContext db, string email)
    {
        return db.Users.FirstOrDefault(u => u.Email == email);
    }

    private string CreateSessionToken(User user)
    {
        var keyBytes = Encoding.ASCII.GetBytes(SecretKey);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(
                new[] {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }
            ),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}
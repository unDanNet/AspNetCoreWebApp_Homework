using System.Net.Http.Headers;
using DigitalGamesStoreService.Models.Requests;
using DigitalGamesStoreService.Models.Responses;
using DigitalGamesStoreService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace DigitalGamesStoreService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    #region Services

    private readonly IAuthenticationService authenticationService;
    private readonly IValidator<AuthenticationRequest> authRequestValidator;

    #endregion

    public AuthenticationController(IAuthenticationService authenticationService, 
        IValidator<AuthenticationRequest> authRequestValidator)
    {
        this.authenticationService = authenticationService;
        this.authRequestValidator = authRequestValidator;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
    public IActionResult SignIn([FromBody] AuthenticationRequest request)
    {
        var validationResult = authRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDictionary());
        }
        
        var response = authenticationService.SignIn(request);

        if (response.Status == Models.Responses.AuthenticationStatus.Success)
        {
            Response.Headers.Add("X-Session-Token", response.Session.SessionToken);
        }

        return new OkObjectResult(response);
    }

    [HttpGet("session")]
    public IActionResult GetSession()
    {
        var isHeaderCorrect = AuthenticationHeaderValue.TryParse(
            Request.Headers[HeaderNames.Authorization],
            out var authHeader
        );

        if (!isHeaderCorrect)
        {
            return Unauthorized();
        }

        var scheme = authHeader.Scheme;
        var sessionToken = authHeader.Parameter;

        if (string.IsNullOrWhiteSpace(sessionToken))
        {
            return Unauthorized();
        }

        var sessionDto = authenticationService.GetSession(sessionToken);
        if (sessionDto is null)
        {
            return Unauthorized();
        }

        return new OkObjectResult(sessionDto);
    }
}
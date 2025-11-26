using ISM.Application.Commands.Auth.ChangePassword;
using ISM.Application.Commands.Auth.CreateJudgeUser;
using ISM.Application.Commands.Auth.Logout;
using ISM.Application.Commands.Auth.RefreshToken;
using ISM.Application.Commands.Auth.RegisterParticipant;
using ISM.Application.DTOs.Auth;
using ISM.Application.Queries.Auth.Login;
using ISM.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("register/participant")]
    [AllowAnonymous]
    public async Task<ActionResult<ParticipantRegistrationResultDto>> RegisterParticipant([FromBody] RegisterParticipantCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<RefreshTokenResponseDto>> Refresh([FromBody] RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<ActionResult<ChangePasswordResultDto>> ChangePassword([FromBody] ChangePasswordCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("judge")]
    [Authorize(Policy = AuthorizationPolicies.AdminOnly)]
    public async Task<ActionResult<JudgeCreatedDto>> CreateJudge([FromBody] CreateJudgeUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}

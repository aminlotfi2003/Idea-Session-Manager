using Asp.Versioning;
using ISM.Application.Features.Auth.Commands.ChangePassword;
using ISM.Application.Features.Auth.Commands.CreateJudgeUser;
using ISM.Application.Features.Auth.Commands.Logout;
using ISM.Application.Features.Auth.Commands.RefreshToken;
using ISM.Application.Features.Auth.Commands.RegisterParticipant;
using ISM.Application.Features.Auth.Dtos;
using ISM.Application.Features.Auth.Queries.Login;
using ISM.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/evaluations")]
[ApiVersion("1.0")]
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

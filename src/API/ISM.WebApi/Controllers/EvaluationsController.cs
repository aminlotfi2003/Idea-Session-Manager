using Asp.Versioning;
using ISM.Application.Commands.Evaluations.SubmitIdeaEvaluation;
using ISM.Application.DTOs.Evaluations;
using ISM.Application.Queries.Evaluations.GetAssignedIdeasForJudge;
using ISM.Application.Queries.Evaluations.GetIdeaForEvaluation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ISM.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/evaluations")]
[ApiVersion("1.0")]
public class EvaluationsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private Guid GetCurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("events/{eventId}/assigned")]
    [Authorize(Roles = "Judge")]
    public async Task<ActionResult<IReadOnlyCollection<JudgeAssignedIdeaDto>>> Assigned(Guid eventId)
    {
        var result = await _mediator.Send(new GetAssignedIdeasForJudgeQuery(GetCurrentUserId(), eventId));
        return Ok(result);
    }

    [HttpGet("ideas/{ideaId}")]
    [Authorize(Roles = "Judge")]
    public async Task<ActionResult<IdeaEvaluationDetailDto>> ForIdea(Guid ideaId)
    {
        var result = await _mediator.Send(new GetIdeaForEvaluationQuery(ideaId, GetCurrentUserId()));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Judge")]
    public async Task<IActionResult> Submit([FromBody] SubmitIdeaEvaluationDto dto)
    {
        await _mediator.Send(new SubmitIdeaEvaluationCommand(GetCurrentUserId(), dto));
        return NoContent();
    }
}

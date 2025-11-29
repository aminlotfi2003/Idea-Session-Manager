using Asp.Versioning;
using ISM.Application.Features.Evaluations.Commands.SubmitIdeaEvaluation;
using ISM.Application.Features.Evaluations.Dtos;
using ISM.Application.Features.Evaluations.Queries.GetAssignedIdeasForJudge;
using ISM.Application.Features.Evaluations.Queries.GetIdeaForEvaluation;
using ISM.SharedKernel.Common.Pagination;
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
    public async Task<ActionResult<PaginatedResult<JudgeAssignedIdeaDto>>> Assigned(Guid eventId, [FromQuery] PaginationParams pagination)
    {
        pagination ??= new PaginationParams();
        var result = await _mediator.Send(new GetAssignedIdeasForJudgeQuery(GetCurrentUserId(), eventId, pagination));
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

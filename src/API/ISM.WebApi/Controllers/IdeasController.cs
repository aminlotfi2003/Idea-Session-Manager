using Asp.Versioning;
using ISM.Application.Features.Ideas.Commands.AssignIdeasToJudges;
using ISM.Application.Features.Ideas.Commands.CalculateFinalScoresAndRanking;
using ISM.Application.Features.Ideas.Commands.SubmitIdea;
using ISM.Application.Features.Ideas.Dtos;
using ISM.Application.Features.Ideas.Queries.GetEventResultsForAdmin;
using ISM.Application.Features.Ideas.Queries.GetMyIdeaDetails;
using ISM.Application.Features.Ideas.Queries.GetMyIdeaResult;
using ISM.Application.Features.Ideas.Queries.GetMyIdeasForEvent;
using ISM.SharedKernel.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ISM.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/ideas")]
[ApiVersion("1.0")]
public class IdeasController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private Guid GetCurrentUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    [Authorize(Roles = "Participant")]
    public async Task<ActionResult<IdeaDetailDto>> Submit([FromBody] SubmitIdeaDto dto)
    {
        var result = await _mediator.Send(new SubmitIdeaCommand(dto, GetCurrentUserId()));
        return Ok(result);
    }

    [HttpPost("{eventId}/assignments")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Assign(Guid eventId, [FromBody] IEnumerable<IdeaAssignmentDto> assignments)
    {
        await _mediator.Send(new AssignIdeasToJudgesCommand(eventId, assignments));
        return NoContent();
    }

    [HttpPost("{eventId}/scores")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Calculate(Guid eventId)
    {
        await _mediator.Send(new CalculateFinalScoresAndRankingCommand(eventId));
        return NoContent();
    }

    [HttpGet("{eventId}/mine")]
    [Authorize(Roles = "Participant")]
    public async Task<ActionResult<PaginatedResult<IdeaListItemDto>>> MyIdeas(Guid eventId, [FromQuery] PaginationParams pagination)
    {
        pagination ??= new PaginationParams();
        var result = await _mediator.Send(new GetMyIdeasForEventQuery(eventId, GetCurrentUserId(), pagination));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Participant")]
    public async Task<ActionResult<IdeaDetailDto>> MyIdea(Guid id)
    {
        var result = await _mediator.Send(new GetMyIdeaDetailsQuery(id, GetCurrentUserId()));
        return Ok(result);
    }

    [HttpGet("{eventId}/results")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaginatedResult<IdeaResultDto>>> Results(Guid eventId, [FromQuery] PaginationParams pagination)
    {
        pagination ??= new PaginationParams();
        var result = await _mediator.Send(new GetEventResultsForAdminQuery(eventId, pagination));
        return Ok(result);
    }

    [HttpGet("{id}/my-result")]
    [Authorize(Roles = "Participant")]
    public async Task<ActionResult<IdeaResultDto>> MyResult(Guid id)
    {
        var result = await _mediator.Send(new GetMyIdeaResultQuery(id, GetCurrentUserId()));
        return Ok(result);
    }
}

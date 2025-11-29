using Asp.Versioning;
using ISM.Application.Features.Events.Commands.ArchiveEvent;
using ISM.Application.Features.Events.Commands.AssignJudgesToEvent;
using ISM.Application.Features.Events.Commands.CloseIdeaSubmission;
using ISM.Application.Features.Events.Commands.CreateInnovationEvent;
using ISM.Application.Features.Events.Commands.DefineOrUpdateEvaluationCriteria;
using ISM.Application.Features.Events.Commands.FinalizeEventEvaluation;
using ISM.Application.Features.Events.Commands.OpenIdeaSubmission;
using ISM.Application.Features.Events.Commands.PublishEvent;
using ISM.Application.Features.Events.Commands.PublishEventResults;
using ISM.Application.Features.Events.Commands.UpdateInnovationEvent;
using ISM.Application.Features.Events.Dtos;
using ISM.Application.Features.Events.Queries.GetEventDetails;
using ISM.Application.Features.Events.Queries.GetEventList;
using ISM.Application.Features.Events.Queries.GetEventSummaryReport;
using ISM.Application.Features.Events.Queries.GetOpenEventsForParticipant;
using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/events")]
[ApiVersion("1.0")]
public class EventsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<InnovationEventDetailDto>> Create([FromBody] CreateInnovationEventDto dto)
    {
        var result = await _mediator.Send(new CreateInnovationEventCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id, version = "1.0" }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<InnovationEventDetailDto>> Update(Guid id, [FromBody] UpdateInnovationEventDto dto)
    {
        if (id != dto.Id) return BadRequest();
        var result = await _mediator.Send(new UpdateInnovationEventCommand(dto));
        return Ok(result);
    }

    [HttpPost("{id}/publish")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<InnovationEventDetailDto>> Publish(Guid id)
    {
        var result = await _mediator.Send(new PublishEventCommand(id));
        return Ok(result);
    }

    [HttpPost("{id}/open-submission")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> OpenSubmission(Guid id)
    {
        await _mediator.Send(new OpenIdeaSubmissionCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/close-submission")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CloseSubmission(Guid id)
    {
        await _mediator.Send(new CloseIdeaSubmissionCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/criteria")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DefineCriteria(Guid id, [FromBody] IEnumerable<EvaluationCriteriaDto> criteria)
    {
        await _mediator.Send(new DefineOrUpdateEvaluationCriteriaCommand(new DefineEvaluationCriteriaDto(id, criteria)));
        return NoContent();
    }

    [HttpPost("{id}/judges")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignJudges(Guid id, [FromBody] IEnumerable<Guid> judgeIds)
    {
        await _mediator.Send(new AssignJudgesToEventCommand(new AssignJudgesDto(id, judgeIds)));
        return NoContent();
    }

    [HttpPost("{id}/finalize")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> FinalizeEvaluation(Guid id)
    {
        await _mediator.Send(new FinalizeEventEvaluationCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/publish-results")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PublishResults(Guid id)
    {
        await _mediator.Send(new PublishEventResultsCommand(id));
        return NoContent();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaginatedResult<InnovationEventListItemDto>>> GetList([FromQuery] EventStatus? status, [FromQuery] PaginationParams pagination)
    {
        pagination ??= new PaginationParams();
        var result = await _mediator.Send(new GetEventListQuery(status, pagination));
        return Ok(result);
    }

    [HttpGet("open")]
    [Authorize(Roles = "Participant")]
    public async Task<ActionResult<PaginatedResult<InnovationEventListItemDto>>> GetOpen([FromQuery] AllowedParticipantGroup group, [FromQuery] PaginationParams pagination)
    {
        pagination ??= new PaginationParams();
        var result = await _mediator.Send(new GetOpenEventsForParticipantQuery(group, pagination));
        return Ok(result);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Participant,Judge")]
    public async Task<ActionResult<InnovationEventDetailDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEventDetailsQuery(id));
        return Ok(result);
    }

    [HttpPost("{id}/archive")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Archive(Guid id)
    {
        await _mediator.Send(new ArchiveEventCommand(id));
        return NoContent();
    }

    [HttpGet("{id}/summary")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<EventSummaryDto>> Summary(Guid id)
    {
        var result = await _mediator.Send(new GetEventSummaryReportQuery(id));
        return Ok(result);
    }
}

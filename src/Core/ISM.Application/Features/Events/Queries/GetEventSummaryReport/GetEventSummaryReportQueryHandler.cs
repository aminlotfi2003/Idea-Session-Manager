using ISM.Application.Common.Abstractions.Persistence;
using ISM.Application.Features.Events.Dtos;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventSummaryReport;

internal class GetEventSummaryReportQueryHandler : IRequestHandler<GetEventSummaryReportQuery, EventSummaryDto>
{
    private readonly IUnitOfWork _uow;

    public GetEventSummaryReportQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<EventSummaryDto> Handle(GetEventSummaryReportQuery request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        return new EventSummaryDto
        {
            EventId = entity.Id,
            Title = entity.Title,
            Status = entity.Status,
            IdeaCount = entity.Ideas.Count,
            JudgeCount = entity.EventJudges.Count
        };
    }
}

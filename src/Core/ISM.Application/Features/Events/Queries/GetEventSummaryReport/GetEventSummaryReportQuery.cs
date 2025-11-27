using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.GetEventSummaryReport;

public record GetEventSummaryReportQuery(Guid EventId) : IRequest<EventSummaryDto>;

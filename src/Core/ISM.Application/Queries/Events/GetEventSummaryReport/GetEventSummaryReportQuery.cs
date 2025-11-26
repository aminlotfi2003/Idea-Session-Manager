using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.GetEventSummaryReport;

public record GetEventSummaryReportQuery(Guid EventId) : IRequest<EventSummaryDto>;

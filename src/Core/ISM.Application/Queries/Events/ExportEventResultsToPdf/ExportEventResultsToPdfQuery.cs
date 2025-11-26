using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.ExportEventResultsToPdf;

public record ExportEventResultsToPdfQuery(Guid EventId) : IRequest<EventReportFileDto>;

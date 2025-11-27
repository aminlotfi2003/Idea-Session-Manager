using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.ExportEventResultsToPdf;

public record ExportEventResultsToPdfQuery(Guid EventId) : IRequest<EventReportFileDto>;

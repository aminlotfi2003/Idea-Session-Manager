using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.ExportEventResultsToExcel;

public record ExportEventResultsToExcelQuery(Guid EventId) : IRequest<EventReportFileDto>;

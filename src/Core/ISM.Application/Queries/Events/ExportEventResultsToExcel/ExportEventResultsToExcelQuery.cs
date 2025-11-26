using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.ExportEventResultsToExcel;

public record ExportEventResultsToExcelQuery(Guid EventId) : IRequest<EventReportFileDto>;

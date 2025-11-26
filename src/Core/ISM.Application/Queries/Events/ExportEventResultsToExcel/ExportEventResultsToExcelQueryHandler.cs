using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.ExportEventResultsToExcel;

internal class ExportEventResultsToExcelQueryHandler : IRequestHandler<ExportEventResultsToExcelQuery, EventReportFileDto>
{
    public Task<EventReportFileDto> Handle(ExportEventResultsToExcelQuery request, CancellationToken cancellationToken)
    {
        var dummy = new EventReportFileDto("results.xlsx", Array.Empty<byte>(), "application/vnd.ms-excel");
        return Task.FromResult(dummy);
    }
}

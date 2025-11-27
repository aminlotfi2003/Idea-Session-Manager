using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.ExportEventResultsToExcel;

internal class ExportEventResultsToExcelQueryHandler : IRequestHandler<ExportEventResultsToExcelQuery, EventReportFileDto>
{
    public Task<EventReportFileDto> Handle(ExportEventResultsToExcelQuery request, CancellationToken cancellationToken)
    {
        var dummy = new EventReportFileDto("results.xlsx", Array.Empty<byte>(), "application/vnd.ms-excel");
        return Task.FromResult(dummy);
    }
}

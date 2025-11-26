using ISM.Application.DTOs.Events;
using MediatR;

namespace ISM.Application.Queries.Events.ExportEventResultsToPdf;

internal class ExportEventResultsToPdfQueryHandler : IRequestHandler<ExportEventResultsToPdfQuery, EventReportFileDto>
{
    public Task<EventReportFileDto> Handle(ExportEventResultsToPdfQuery request, CancellationToken cancellationToken)
    {
        var dummy = new EventReportFileDto("results.pdf", Array.Empty<byte>(), "application/pdf");
        return Task.FromResult(dummy);
    }
}

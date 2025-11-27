using ISM.Application.Features.Events.Dtos;
using MediatR;

namespace ISM.Application.Features.Events.Queries.ExportEventResultsToPdf;

internal class ExportEventResultsToPdfQueryHandler : IRequestHandler<ExportEventResultsToPdfQuery, EventReportFileDto>
{
    public Task<EventReportFileDto> Handle(ExportEventResultsToPdfQuery request, CancellationToken cancellationToken)
    {
        var dummy = new EventReportFileDto("results.pdf", Array.Empty<byte>(), "application/pdf");
        return Task.FromResult(dummy);
    }
}

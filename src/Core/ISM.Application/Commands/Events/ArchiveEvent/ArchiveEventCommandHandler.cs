using ISM.Application.Abstractions.Repositories;
using MediatR;

namespace ISM.Application.Commands.Events.ArchiveEvent;

internal class ArchiveEventCommandHandler : IRequestHandler<ArchiveEventCommand>
{
    private readonly IUnitOfWork _uow;

    public ArchiveEventCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(ArchiveEventCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        entity.Archive();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

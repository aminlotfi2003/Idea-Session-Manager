using ISM.Application.Abstractions.Repositories;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.PublishEventResults;

internal class PublishEventResultsCommandHandler : IRequestHandler<PublishEventResultsCommand>
{
    private readonly IUnitOfWork _uow;

    public PublishEventResultsCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(PublishEventResultsCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        entity.PublishResults();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

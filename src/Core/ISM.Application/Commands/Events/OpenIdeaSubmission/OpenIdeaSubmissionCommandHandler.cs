using ISM.Application.Abstractions.Repositories;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.OpenIdeaSubmission;

internal class OpenIdeaSubmissionCommandHandler : IRequestHandler<OpenIdeaSubmissionCommand>
{
    private readonly IUnitOfWork _uow;

    public OpenIdeaSubmissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(OpenIdeaSubmissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        entity.OpenIdeaSubmission();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

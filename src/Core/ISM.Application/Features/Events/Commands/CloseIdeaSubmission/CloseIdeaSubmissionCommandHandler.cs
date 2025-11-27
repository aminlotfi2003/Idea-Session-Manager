using ISM.Application.Common.Abstractions.Persistence;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Events.Commands.CloseIdeaSubmission;

internal class CloseIdeaSubmissionCommandHandler : IRequestHandler<CloseIdeaSubmissionCommand>
{
    private readonly IUnitOfWork _uow;

    public CloseIdeaSubmissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CloseIdeaSubmissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        entity.CloseIdeaSubmission();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

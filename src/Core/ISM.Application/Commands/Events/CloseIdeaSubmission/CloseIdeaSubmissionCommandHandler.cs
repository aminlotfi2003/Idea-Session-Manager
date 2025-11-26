using ISM.Application.Abstractions.Repositories;
using MediatR;

namespace ISM.Application.Commands.Events.CloseIdeaSubmission;

internal class CloseIdeaSubmissionCommandHandler : IRequestHandler<CloseIdeaSubmissionCommand>
{
    private readonly IUnitOfWork _uow;

    public CloseIdeaSubmissionCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CloseIdeaSubmissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _uow.InnovationEvents.GetByIdAsync(request.EventId, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        entity.CloseIdeaSubmission();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

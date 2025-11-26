using ISM.Application.Abstractions.Repositories;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Commands.Events.AssignJudgesToEvent;

internal class AssignJudgesToEventCommandHandler : IRequestHandler<AssignJudgesToEventCommand>
{
    private readonly IUnitOfWork _uow;

    public AssignJudgesToEventCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AssignJudgesToEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.Payload.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        foreach (var judgeId in request.Payload.JudgeIds)
        {
            var judge = await _uow.Judges.GetByIdAsync(judgeId, cancellationToken) ?? throw new NotFoundException("Judge not found");
            eventEntity.AssignJudge(judge);
        }

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Entities;
using MediatR;

namespace ISM.Application.Commands.Ideas.AssignIdeasToJudges;

internal class AssignIdeasToJudgesCommandHandler : IRequestHandler<AssignIdeasToJudgesCommand>
{
    private readonly IUnitOfWork _uow;

    public AssignIdeasToJudgesCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(AssignIdeasToJudgesCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        foreach (var assignment in request.Assignments)
        {
            var idea = eventEntity.Ideas.FirstOrDefault(i => i.Id == assignment.IdeaId) ?? throw new KeyNotFoundException("Idea not found");
            var judge = await _uow.Judges.GetByIdAsync(assignment.JudgeId, cancellationToken) ?? throw new KeyNotFoundException("Judge not found");
            var evaluation = IdeaEvaluation.Create(idea.Id, judge.Id);
            idea.AddEvaluation(evaluation);
            await _uow.IdeaEvaluations.AddAsync(evaluation, cancellationToken);
        }

        await _uow.SaveChangesAsync(cancellationToken);
    }
}

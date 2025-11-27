using ISM.Application.Common.Abstractions.Persistence;
using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Exceptions;
using MediatR;

namespace ISM.Application.Features.Events.Commands.FinalizeEventEvaluation;

internal class FinalizeEventEvaluationCommandHandler : IRequestHandler<FinalizeEventEvaluationCommand>
{
    private readonly IUnitOfWork _uow;

    public FinalizeEventEvaluationCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(FinalizeEventEvaluationCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new NotFoundException("Event not found");
        foreach (var idea in eventEntity.Ideas)
        {
            var weightedScores = idea.Evaluations.Where(e => e.WeightedScore.HasValue).Select(e => e.WeightedScore!.Value).ToList();
            if (weightedScores.Any())
            {
                var finalScore = weightedScores.Average();
                idea.MarkEvaluated(finalScore, finalScore >= 3 ? FinalDecision.Approved : FinalDecision.Rejected);
            }
        }

        eventEntity.MarkEvaluationCompleted();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

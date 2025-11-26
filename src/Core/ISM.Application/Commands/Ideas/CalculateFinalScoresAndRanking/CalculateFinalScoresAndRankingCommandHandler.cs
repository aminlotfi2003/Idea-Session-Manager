using ISM.Application.Abstractions.Repositories;
using ISM.Domain.Enums;
using MediatR;

namespace ISM.Application.Commands.Ideas.CalculateFinalScoresAndRanking;

internal class CalculateFinalScoresAndRankingCommandHandler : IRequestHandler<CalculateFinalScoresAndRankingCommand>
{
    private readonly IUnitOfWork _uow;

    public CalculateFinalScoresAndRankingCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task Handle(CalculateFinalScoresAndRankingCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = await _uow.InnovationEvents.GetWithDetailsAsync(request.EventId, cancellationToken) ?? throw new KeyNotFoundException("Event not found");
        var scoredIdeas = eventEntity.Ideas
            .Select(i => new
            {
                Idea = i,
                Final = i.Evaluations.Where(e => e.WeightedScore.HasValue).Select(e => e.WeightedScore!.Value).DefaultIfEmpty(0).Average()
            })
            .OrderByDescending(x => x.Final)
            .ToList();

        int rank = 1;
        foreach (var item in scoredIdeas)
        {
            item.Idea.MarkEvaluated(item.Final, item.Final >= 3 ? FinalDecision.Approved : FinalDecision.Rejected);
            item.Idea.SetRank(rank++);
        }

        eventEntity.MarkEvaluationCompleted();
        await _uow.SaveChangesAsync(cancellationToken);
    }
}

namespace ISM.Application.Abstractions.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    IInnovationEventRepository InnovationEvents { get; }
    IIdeaRepository Ideas { get; }
    IParticipantProfileRepository ParticipantProfiles { get; }
    IJudgeRepository Judges { get; }
    IEvaluationCriteriaRepository EvaluationCriteria { get; }
    IIdeaEvaluationRepository IdeaEvaluations { get; }
    IEvaluationScoreRepository EvaluationScores { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

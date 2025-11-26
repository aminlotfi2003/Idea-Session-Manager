using ISM.Application.Abstractions.Repositories;
using ISM.Infrastructure.Persistence.Contexts;

namespace ISM.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context,
        IInnovationEventRepository innovationEventRepository,
        IIdeaRepository ideaRepository,
        IParticipantProfileRepository participantProfileRepository,
        IJudgeRepository judgeRepository,
        IEvaluationCriteriaRepository evaluationCriteriaRepository,
        IIdeaEvaluationRepository ideaEvaluationRepository,
        IEvaluationScoreRepository evaluationScoreRepository)
    {
        _context = context;
        InnovationEvents = innovationEventRepository;
        Ideas = ideaRepository;
        ParticipantProfiles = participantProfileRepository;
        Judges = judgeRepository;
        EvaluationCriteria = evaluationCriteriaRepository;
        IdeaEvaluations = ideaEvaluationRepository;
        EvaluationScores = evaluationScoreRepository;
    }

    public IInnovationEventRepository InnovationEvents { get; }

    public IIdeaRepository Ideas { get; }

    public IParticipantProfileRepository ParticipantProfiles { get; }

    public IJudgeRepository Judges { get; }

    public IEvaluationCriteriaRepository EvaluationCriteria { get; }

    public IIdeaEvaluationRepository IdeaEvaluations { get; }

    public IEvaluationScoreRepository EvaluationScores { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}

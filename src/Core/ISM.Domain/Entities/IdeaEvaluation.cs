using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class IdeaEvaluation : Entity
{
    private readonly List<EvaluationScore> _scores = new();

    private IdeaEvaluation() { } // for EF

    public Guid IdeaId { get; private set; }
    public Guid JudgeId { get; private set; }
    public DateTimeOffset EvaluationDate { get; private set; }
    public OverallDecision OverallDecision { get; private set; } = OverallDecision.NotSet;
    public string? Comments { get; private set; }

    public IReadOnlyCollection<EvaluationScore> Scores => _scores.AsReadOnly();

    public static IdeaEvaluation Create(Guid ideaId, Guid judgeId)
    {
        return new IdeaEvaluation
        {
            IdeaId = ideaId,
            JudgeId = judgeId,
            EvaluationDate = DateTimeOffset.UtcNow
        };
    }

    public void SetDecision(OverallDecision decision, string? comments)
    {
        OverallDecision = decision;
        Comments = comments;
        EvaluationDate = DateTimeOffset.UtcNow;
    }

    public void AddScore(EvaluationScore score)
    {
        _scores.Add(score);
    }
}

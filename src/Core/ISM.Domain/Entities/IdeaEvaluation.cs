using ISM.Domain.Enums;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class IdeaEvaluation : Entity
{
    private IdeaEvaluation() { }

    public Guid IdeaId { get; private set; }
    public Idea Idea { get; private set; } = null!;

    public Guid JudgeId { get; private set; }
    public Judge Judge { get; private set; } = null!;

    public DateTimeOffset EvaluationDate { get; private set; }
    public OverallDecision Decision { get; private set; } = OverallDecision.NotSet;
    public string? Comments { get; private set; }
    public double? WeightedScore { get; private set; }

    public ICollection<EvaluationScore> Scores { get; private set; } = new HashSet<EvaluationScore>();

    public static IdeaEvaluation Create(Guid ideaId, Guid judgeId)
    {
        return new IdeaEvaluation
        {
            IdeaId = ideaId,
            JudgeId = judgeId,
            EvaluationDate = DateTimeOffset.UtcNow
        };
    }

    public void SetDecision(OverallDecision decision, string? comments, double? weightedScore = null)
    {
        Decision = decision;
        Comments = comments;
        WeightedScore = weightedScore;
        EvaluationDate = DateTimeOffset.UtcNow;
    }

    public void AddScore(EvaluationScore score)
    {
        Scores.Add(score);
    }
}

using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class EvaluationScore : Entity
{
    private EvaluationScore() { } // for EF

    public Guid IdeaEvaluationId { get; private set; }
    public IdeaEvaluation IdeaEvaluation { get; private set; } = null!;

    public Guid EvaluationCriteriaId { get; private set; }
    public EvaluationCriteria EvaluationCriteria { get; private set; } = null!;
    
    public int Score { get; private set; }
    public string? Comment { get; private set; }

    public static EvaluationScore Create(Guid ideaEvaluationId, Guid evaluationCriteriaId, int score, string? comment)
    {
        return new EvaluationScore
        {
            IdeaEvaluationId = ideaEvaluationId,
            EvaluationCriteriaId = evaluationCriteriaId,
            Score = score,
            Comment = comment
        };
    }
}

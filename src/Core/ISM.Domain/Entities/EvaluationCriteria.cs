using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class EvaluationCriteria : Entity
{
    private EvaluationCriteria() { } // for EF

    public Guid InnovationEventId { get; private set; }
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int MinScore { get; private set; }
    public int MaxScore { get; private set; }
    public double Weight { get; private set; }
    public int Order { get; private set; }

    public static EvaluationCriteria Create(
        Guid innovationEventId,
        string title,
        string description,
        int minScore,
        int maxScore,
        double weight,
        int order)
    {
        if (maxScore <= minScore)
            throw new ArgumentException("MaxScore must be greater than MinScore.");

        if (weight <= 0)
            throw new ArgumentException("Weight must be greater than zero.");

        return new EvaluationCriteria
        {
            InnovationEventId = innovationEventId,
            Title = title,
            Description = description,
            MinScore = minScore,
            MaxScore = maxScore,
            Weight = weight,
            Order = order
        };
    }
}

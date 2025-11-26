using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class EventJudge : Entity
{
    private EventJudge() { }

    public Guid InnovationEventId { get; private set; }
    public InnovationEvent InnovationEvent { get; private set; } = null!;

    public Guid JudgeId { get; private set; }
    public Judge Judge { get; private set; } = null!;

    public static EventJudge Create(Guid innovationEventId, Guid judgeId)
    {
        return new EventJudge
        {
            InnovationEventId = innovationEventId,
            JudgeId = judgeId
        };
    }
}

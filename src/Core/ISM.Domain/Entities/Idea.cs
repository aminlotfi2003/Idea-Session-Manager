using ISM.Domain.Enums;
using ISM.Domain.DomainEvents;
using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class Idea : Entity, IAggregateRoot
{
    private Idea() { }

    public Guid InnovationEventId { get; private set; }
    public InnovationEvent InnovationEvent { get; private set; } = null!;

    public IdeaCode IdeaCode { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Requirements { get; private set; } = default!;
    public string ProposedImplementation { get; private set; } = default!;
    public string ValueProposition { get; private set; } = default!;
    public DateTimeOffset SubmittedAt { get; private set; }
    public IdeaStatus Status { get; private set; } = IdeaStatus.Submitted;

    public double? FinalScore { get; private set; }
    public FinalDecision FinalDecision { get; private set; } = FinalDecision.None;
    public int? Rank { get; private set; }

    public IdeaParticipantLink ConfidentialLink { get; private set; } = null!;

    public ICollection<IdeaEvaluation> Evaluations { get; private set; } = new HashSet<IdeaEvaluation>();

    public static Idea Submit(
        Guid innovationEventId,
        IdeaCode ideaCode,
        string title,
        string description,
        string requirements,
        string proposedImplementation,
        string valueProposition,
        Guid participantProfileId,
        string encryptedParticipantPayload)
    {
        var idea = new Idea
        {
            InnovationEventId = innovationEventId,
            IdeaCode = ideaCode,
            Title = title,
            Description = description,
            Requirements = requirements,
            ProposedImplementation = proposedImplementation,
            ValueProposition = valueProposition,
            SubmittedAt = DateTimeOffset.UtcNow,
            Status = IdeaStatus.Submitted
        };

        idea.ConfidentialLink = IdeaParticipantLink.Create(idea.Id, participantProfileId, encryptedParticipantPayload);

        idea.AddDomainEvent(new IdeaSubmittedDomainEvent(idea.Id, innovationEventId));
        return idea;
    }

    public void MarkAnonymized()
    {
        Status = IdeaStatus.Anonymized;
    }

    public void MarkAssignedToJudges()
    {
        Status = IdeaStatus.AssignedToJudges;
    }

    public void MarkUnderReview()
    {
        Status = IdeaStatus.UnderReview;
    }

    public void MarkEvaluated(double finalScore, FinalDecision decision)
    {
        FinalScore = finalScore;
        FinalDecision = decision;
        Status = IdeaStatus.Evaluated;

        AddDomainEvent(new IdeaEvaluatedDomainEvent(Id, InnovationEventId, finalScore, MapToOverallDecision(decision)));
    }

    public void SetRank(int rank)
    {
        Rank = rank;
        Status = IdeaStatus.Finalized;
    }

    public void AddEvaluation(IdeaEvaluation evaluation)
    {
        Evaluations.Add(evaluation);
    }

    private static OverallDecision MapToOverallDecision(FinalDecision finalDecision)
    {
        return finalDecision switch
        {
            FinalDecision.Approved => OverallDecision.Approved,
            FinalDecision.Rejected => OverallDecision.Rejected,
            FinalDecision.NeedsRevision => OverallDecision.NeedsRevision,
            _ => OverallDecision.NotSet
        };
    }
}

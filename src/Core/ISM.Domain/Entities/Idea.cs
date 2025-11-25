using ISM.Domain.Enums;
using ISM.Domain.Events;
using ISM.Domain.ValueObjects;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class Idea : Entity, IAggregateRoot
{
    private readonly List<IdeaEvaluation> _evaluations = new();

    private Idea() { } // for EF

    public Guid InnovationEventId { get; private set; }
    public IdeaCode IdeaCode { get; private set; } = default!;
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Requirements { get; private set; } = default!;
    public string ProposedImplementationMethod { get; private set; } = default!;
    public string ValueProposition { get; private set; } = default!;
    public DateTimeOffset SubmissionDate { get; private set; }
    public IdeaStatus Status { get; private set; } = IdeaStatus.Submitted;

    public double? FinalScore { get; private set; }
    public OverallDecision OverallDecision { get; private set; } = OverallDecision.NotSet;
    public int? Rank { get; private set; }

    public Guid EncryptedParticipantReferenceId { get; private set; }

    public IReadOnlyCollection<IdeaEvaluation> Evaluations => _evaluations.AsReadOnly();

    public static Idea Submit(
        Guid innovationEventId,
        IdeaCode ideaCode,
        string title,
        string description,
        string requirements,
        string proposedImplementationMethod,
        string valueProposition,
        Guid encryptedParticipantReferenceId)
    {
        var idea = new Idea
        {
            InnovationEventId = innovationEventId,
            IdeaCode = ideaCode,
            Title = title,
            Description = description,
            Requirements = requirements,
            ProposedImplementationMethod = proposedImplementationMethod,
            ValueProposition = valueProposition,
            SubmissionDate = DateTimeOffset.UtcNow,
            Status = IdeaStatus.Submitted,
            EncryptedParticipantReferenceId = encryptedParticipantReferenceId
        };

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

    public void MarkEvaluated(double finalScore, OverallDecision decision)
    {
        FinalScore = finalScore;
        OverallDecision = decision;
        Status = IdeaStatus.Evaluated;

        AddDomainEvent(new IdeaEvaluatedDomainEvent(Id, InnovationEventId, finalScore, decision));
    }

    public void SetRank(int rank)
    {
        Rank = rank;
        Status = IdeaStatus.Finalized;
    }

    public void AddEvaluation(IdeaEvaluation evaluation)
    {
        _evaluations.Add(evaluation);
    }
}

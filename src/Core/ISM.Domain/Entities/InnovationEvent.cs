using ISM.Domain.Enums;
using ISM.Domain.DomainEvents;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class InnovationEvent : Entity, IAggregateRoot, IAuditableEntity
{
    private InnovationEvent() { }

    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Goals { get; private set; } = default!;
    public AllowedParticipantGroup AllowedParticipantGroup { get; private set; }
    public DateTimeOffset IdeaSubmissionStart { get; private set; }
    public DateTimeOffset IdeaSubmissionEnd { get; private set; }
    public EventStatus Status { get; private set; } = EventStatus.Draft;
    public string? RulesDocumentPath { get; private set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid? CreatedBy { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public bool IsArchived { get; private set; }

    public ICollection<EvaluationCriteria> Criteria { get; private set; } = new HashSet<EvaluationCriteria>();
    public ICollection<Idea> Ideas { get; private set; } = new HashSet<Idea>();
    public ICollection<EventJudge> EventJudges { get; private set; } = new HashSet<EventJudge>();

    public static InnovationEvent Create(
        string title,
        string description,
        string goals,
        AllowedParticipantGroup allowedParticipantGroup,
        DateTimeOffset ideaSubmissionStart,
        DateTimeOffset ideaSubmissionEnd,
        Guid createdBy,
        string? rulesDocumentPath = null)
    {
        if (ideaSubmissionEnd <= ideaSubmissionStart)
        {
            throw new ArgumentException("Idea submission end must be greater than start.");
        }

        var ev = new InnovationEvent
        {
            Title = title,
            Description = description,
            Goals = goals,
            AllowedParticipantGroup = allowedParticipantGroup,
            IdeaSubmissionStart = ideaSubmissionStart,
            IdeaSubmissionEnd = ideaSubmissionEnd,
            CreatedAt = DateTimeOffset.UtcNow,
            CreatedBy = createdBy,
            Status = EventStatus.Draft,
            RulesDocumentPath = rulesDocumentPath
        };

        ev.AddDomainEvent(new InnovationEventCreatedDomainEvent(ev.Id));

        return ev;
    }

    public void Publish()
    {
        EnsureStatus(EventStatus.Draft);
        Status = EventStatus.Published;
        TouchUpdatedAt();
        AddDomainEvent(new InnovationEventPublishedDomainEvent(Id));
    }

    public void OpenIdeaSubmission()
    {
        EnsureStatus(EventStatus.Published);
        Status = EventStatus.IdeaSubmissionOpen;
        TouchUpdatedAt();
    }

    public void CloseIdeaSubmission()
    {
        EnsureStatus(EventStatus.IdeaSubmissionOpen);
        Status = EventStatus.IdeaSubmissionClosed;
        TouchUpdatedAt();
    }

    public void AddEvaluationCriteria(EvaluationCriteria criteria)
    {
        Criteria.Add(criteria);
        TouchUpdatedAt();
    }

    public void AssignJudge(Judge judge)
    {
        var linkExists = EventJudges.Any(x => x.JudgeId == judge.Id);
        if (!linkExists)
        {
            EventJudges.Add(EventJudge.Create(Id, judge.Id));
            TouchUpdatedAt();
        }
    }

    internal void AddIdea(Idea idea)
    {
        Ideas.Add(idea);
        TouchUpdatedAt();
    }

    public void MarkEvaluationInProgress()
    {
        Status = EventStatus.EvaluationInProgress;
        TouchUpdatedAt();
    }

    public void MarkEvaluationCompleted()
    {
        Status = EventStatus.EvaluationCompleted;
        TouchUpdatedAt();
    }

    public void PublishResults()
    {
        Status = EventStatus.ResultsPublished;
        TouchUpdatedAt();
        AddDomainEvent(new InnovationEventResultsFinalizedDomainEvent(Id));
    }

    public void Archive()
    {
        Status = EventStatus.Archived;
        IsArchived = true;
        TouchUpdatedAt();
    }

    private void EnsureStatus(EventStatus expected)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException($"Event must be in {expected} status.");
        }
    }

    private void TouchUpdatedAt() => ModifiedAt = DateTimeOffset.UtcNow;
}

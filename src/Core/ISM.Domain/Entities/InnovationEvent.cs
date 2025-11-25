using ISM.Domain.Enums;
using ISM.Domain.Events;
using ISM.SharedKernel.Common.Domain;

namespace ISM.Domain.Entities;

public class InnovationEvent : Entity, IAggregateRoot
{
    private readonly List<EvaluationCriteria> _evaluationCriteria = new();
    private readonly List<Idea> _ideas = new();
    private readonly List<Guid> _judgeIds = new(); // UserIds

    private InnovationEvent() { } // for EF

    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Goals { get; private set; } = default!;
    public string AllowedParticipantGroups { get; private set; } = default!;
    public DateTimeOffset StartDateForIdeaSubmission { get; private set; }
    public DateTimeOffset EndDateForIdeaSubmission { get; private set; }
    public EventStatus Status { get; private set; } = EventStatus.Draft;
    public string? RulesDocumentPath { get; private set; }

    public IReadOnlyCollection<EvaluationCriteria> EvaluationCriteria => _evaluationCriteria.AsReadOnly();
    public IReadOnlyCollection<Idea> Ideas => _ideas.AsReadOnly();
    public IReadOnlyCollection<Guid> JudgeIds => _judgeIds.AsReadOnly();

    public static InnovationEvent Create(
        string title,
        string description,
        string goals,
        string allowedParticipantGroups,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        string? rulesDocumentPath = null)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be greater than start date.");

        var ev = new InnovationEvent
        {
            Title = title,
            Description = description,
            Goals = goals,
            AllowedParticipantGroups = allowedParticipantGroups,
            StartDateForIdeaSubmission = startDate,
            EndDateForIdeaSubmission = endDate,
            Status = EventStatus.Draft,
            RulesDocumentPath = rulesDocumentPath
        };

        ev.AddDomainEvent(new InnovationEventCreatedDomainEvent(ev.Id));

        return ev;
    }

    public void Publish()
    {
        if (Status != EventStatus.Draft)
            throw new InvalidOperationException("Only draft events can be published.");

        Status = EventStatus.Published;
        AddDomainEvent(new InnovationEventPublishedDomainEvent(Id));
    }

    public void OpenIdeaSubmission()
    {
        if (Status != EventStatus.Published)
            throw new InvalidOperationException("Event must be published before opening idea submission.");

        Status = EventStatus.IdeaSubmissionOpen;
    }

    public void CloseIdeaSubmission()
    {
        if (Status != EventStatus.IdeaSubmissionOpen)
            throw new InvalidOperationException("Idea submission is not open.");

        Status = EventStatus.IdeaSubmissionClosed;
    }

    public void AddEvaluationCriteria(EvaluationCriteria criteria)
    {
        _evaluationCriteria.Add(criteria);
    }

    public void AssignJudge(Guid judgeUserId)
    {
        if (!_judgeIds.Contains(judgeUserId))
            _judgeIds.Add(judgeUserId);
    }

    internal void AddIdea(Idea idea)
    {
        _ideas.Add(idea);
    }

    public void MarkEvaluationInProgress()
    {
        Status = EventStatus.EvaluationInProgress;
    }

    public void MarkEvaluationCompleted()
    {
        Status = EventStatus.EvaluationCompleted;
    }

    public void PublishResults()
    {
        Status = EventStatus.ResultsPublished;
        AddDomainEvent(new InnovationEventResultsFinalizedDomainEvent(Id));
    }

    public void Archive()
    {
        Status = EventStatus.Archived;
    }
}

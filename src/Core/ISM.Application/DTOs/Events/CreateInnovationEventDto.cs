using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Events;

public record CreateInnovationEventDto(
    string Title,
    string Description,
    string Goals,
    AllowedParticipantGroup AllowedParticipantGroup,
    DateTimeOffset IdeaSubmissionStart,
    DateTimeOffset IdeaSubmissionEnd,
    string? RulesDocumentPath
);

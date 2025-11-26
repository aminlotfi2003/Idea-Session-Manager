using ISM.Domain.Enums;

namespace ISM.Application.DTOs.Events;

public record UpdateInnovationEventDto(
    Guid Id,
    string Title,
    string Description,
    string Goals,
    AllowedParticipantGroup AllowedParticipantGroup,
    DateTimeOffset IdeaSubmissionStart,
    DateTimeOffset IdeaSubmissionEnd,
    string? RulesDocumentPath
);

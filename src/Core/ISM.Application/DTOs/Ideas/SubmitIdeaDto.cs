namespace ISM.Application.DTOs.Ideas;

public record SubmitIdeaDto(
    Guid EventId,
    string Title,
    string Description,
    string Requirements,
    string ProposedImplementation,
    string ValueProposition,
    string ParticipantName,
    string ParticipantEmail,
    string ParticipantDepartment
);

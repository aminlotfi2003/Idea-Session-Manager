namespace ISM.Application.Features.Ideas.Dtos;

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

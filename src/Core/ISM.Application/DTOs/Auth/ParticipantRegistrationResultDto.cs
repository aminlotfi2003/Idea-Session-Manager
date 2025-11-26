namespace ISM.Application.DTOs.Auth;

public sealed record ParticipantRegistrationResultDto(Guid UserId, Guid ParticipantId, string Email, string FullName, string ParticipantType);

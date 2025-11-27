namespace ISM.Application.Features.Auth.Dtos;

public sealed record ParticipantRegistrationResultDto(Guid UserId, Guid ParticipantId, string Email, string FullName, string ParticipantType);

namespace ISM.Application.DTOs.Auth;

public sealed record JudgeCreatedDto(Guid UserId, string Email, string TemporaryPassword);
